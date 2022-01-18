using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Data;
using DataAccess;
using Features;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Brandaris.Api;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        LoggerConfiguration loggerBuilder = new LoggerConfiguration()
                                           .Enrich.FromLogContext()
                                           .MinimumLevel.Debug()
                                           .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                           .MinimumLevel.Override("System", LogEventLevel.Warning)
                                           .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                                                            theme: AnsiConsoleTheme.Literate);

        Log.Logger = loggerBuilder.CreateLogger();

        Activity.DefaultIdFormat = ActivityIdFormat.W3C;

        try
        {
            IHost host = CreateHostBuilder(args).Build();

            await host.RunAsync()
                      .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Log.Logger.Error(e, e.Message);
            Log.CloseAndFlush();
            Console.WriteLine(e.Message);

            await Task.Delay(1000);
            return -1;
        }

        return 0;
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        new HostBuilder()
           .UseContentRoot(Directory.GetCurrentDirectory())
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
                builder.ClearProviders();

#if DEBUG
                builder.AddSerilog(Log.Logger);
#endif

                builder.AddApplicationInsights(options =>
                {
                    options.IncludeScopes = true;
                    options.FlushOnDispose = true;
                });

                builder.AddFilter<ApplicationInsightsLoggerProvider>(string.Empty, LogLevel.Debug);
                builder.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Warning);
                builder.AddFilter<ApplicationInsightsLoggerProvider>("System", LogLevel.Warning);
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
                services.AddDataAccess<DataContext>(() => hostContext.Configuration.GetConnectionString("Default"));
                services.AddFeatures();
                services.AddHostedService<MigratorHostedService>();
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
