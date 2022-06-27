using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Brandaris.Common;
using Brandaris.Data;
using Brandaris.DataAccess;
using Brandaris.Features;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Serilog;

namespace Brandaris.Api;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        LoggerConfiguration loggerBuilder = new LoggerConfiguration()
                                           .WriteTo.Console();

        Log.Logger = loggerBuilder.CreateBootstrapLogger();
        Log.Information("Starting up");

        Activity.DefaultIdFormat = ActivityIdFormat.W3C;

        try
        {
            IHost host = CreateHostBuilder(args).Build();

            await host.RunAsync()
                      .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Unhandled exception");
            return -1;
        }
        finally
        {
            Log.Information("Shut down complete");
            Log.CloseAndFlush();
            await Task.Delay(1000);
        }

        return 0;
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        new HostBuilder()
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
                services.AddDataAccess<DataContext>(() => hostContext.Configuration.GetConnectionString("Default"));
                services.AddFeatures();
                services.AddCommon();
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
