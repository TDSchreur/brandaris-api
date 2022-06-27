using System;
using BoDi;
using IntegrationTests.Drivers;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Hooks;

[Binding]
public class Hooks
{
    private static IConfiguration config;

    [BeforeScenario(Order = 0)]
    public static void CreateConfig(IObjectContainer container)
    {
        _ = container ?? throw new ArgumentNullException(nameof(container));

        if (config == null)
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "local";

            config = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.{env}.json", false)
                    .Build();
        }

        Driver driver = new();

        container.RegisterInstanceAs(config);
        container.RegisterFactoryAs(objectContainer => driver.WebDriver);
        container.RegisterFactoryAs(objectContainer => driver.DefaultWait);
    }
}
