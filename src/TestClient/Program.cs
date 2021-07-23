using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace TestClient
{
    public static class Program
    {
        public static async Task<int> Main()
        {
            LoggerConfiguration loggerBuilder = new LoggerConfiguration()
                                               .Enrich.FromLogContext()
                                               .MinimumLevel.Debug()
                                               ////.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                               ////.MinimumLevel.Override("System", LogEventLevel.Warning)
                                               .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                                                                theme: AnsiConsoleTheme.Literate);

            Log.Logger = loggerBuilder.CreateLogger();

            try
            {
                IHost host = CreateHostBuilder().Build();

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

        private static IHostBuilder CreateHostBuilder() =>
            new HostBuilder()
               .ConfigureAppConfiguration((context, builder) =>
                {
                    IHostEnvironment env = context.HostingEnvironment;

                    builder.AddJsonFile("appsettings.json", false, false)
                           .AddUserSecrets<ServiceWorker>()
                           .AddEnvironmentVariables();

                    context.Configuration = builder.Build();
                })
               .ConfigureLogging((_, builder) =>
                {
                    builder.ClearProviders();
                    builder.AddSerilog(Log.Logger);
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
                    services.AddHostedService<ServiceWorker>();
                    services.AddSingleton<TokenHelper>();
                    services.AddSingleton<IMsalHttpClientFactory, MsalHttpClientFactory>();
                });
    }
}
