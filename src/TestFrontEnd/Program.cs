using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using TestFrontEnd.ServiceAgents;

namespace TestFrontEnd;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        LoggerConfiguration loggerBuilder = new LoggerConfiguration()
                                           .Enrich.FromLogContext()
                                           .MinimumLevel.Debug()
                                           .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                           .MinimumLevel.Override("Microsoft.Identity.Web.TokenAcquisition", LogEventLevel.Fatal)
                                           .MinimumLevel.Override("System", LogEventLevel.Warning)
                                           .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                                                            theme: AnsiConsoleTheme.Literate);

        Log.Logger = loggerBuilder.CreateLogger();

        Activity.DefaultIdFormat = ActivityIdFormat.W3C;

        try
        {
            IHost host = CreateHostBuilder(args).Build();

            await host.RunAsync()
                      .ContinueWith(_ => { })
                      .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Log.Logger.Error(e, e.Message);
            Log.CloseAndFlush();
            await Task.Delay(1000);
            return -1;
        }

        return 0;
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return new HostBuilder()
              .UseContentRoot(Directory.GetCurrentDirectory())
              .UseSerilog((ctx, lc) =>
               {
                   if (ctx.HostingEnvironment.IsDevelopment())
                   {
                       lc.WriteTo.Console()
                         .ReadFrom.Configuration(ctx.Configuration);
                   }
               })
              .ConfigureAppConfiguration((context, builder) =>
               {
                   IHostEnvironment env = context.HostingEnvironment;

                   builder.AddJsonFile("appsettings.json", false, false)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                          .AddEnvironmentVariables()
                          .AddCommandLine(args);

                   if (env.IsDevelopment())
                   {
                       builder.AddUserSecrets<Startup>();
                   }

                   context.Configuration = builder.Build();
               })
              .ConfigureLogging((_, builder) =>
               {
                   builder.AddApplicationInsights(options =>
                   {
                       options.IncludeScopes = true;
                       options.FlushOnDispose = true;
                   });
               })
              .UseDefaultServiceProvider((context, options) =>
               {
                   bool isDevelopment = context.HostingEnvironment.IsDevelopment();
                   options.ValidateScopes = isDevelopment;
                   options.ValidateOnBuild = isDevelopment;
               })
              .ConfigureServices((hostContext, services) =>
               {
                   services.AddOptions();
                   services.AddHttpClient<IBrandarisApiServiceAgent, BrandarisApiServiceAgent>(client =>
                   {
                       client.BaseAddress = new Uri("https://brandaris-api.azurewebsites.net");
                   });
               })
              .ConfigureWebHost(builder =>
               {
                   builder.ConfigureAppConfiguration((ctx, cb) =>
                   {
                       if (ctx.HostingEnvironment.IsDevelopment())
                       {
                           StaticWebAssetsLoader.UseStaticWebAssets(ctx.HostingEnvironment, ctx.Configuration);
                       }
                   });
                   builder.UseContentRoot(Directory.GetCurrentDirectory());
                   builder.UseKestrel((context, options) => { options.Configure(context.Configuration.GetSection("Kestrel")); });
                   builder.UseIIS();
                   builder.UseStartup<Startup>();
               });
    }
}