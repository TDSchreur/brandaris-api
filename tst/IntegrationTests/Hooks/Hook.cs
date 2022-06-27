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

        container.RegisterInstanceAs(config);
        container.RegisterFactoryAs(objectContainer => new Driver());
        container.RegisterFactoryAs(objectContainer => ((Driver)objectContainer.Resolve(typeof(Driver))).WebDriver);
        container.RegisterFactoryAs(objectContainer => ((Driver)objectContainer.Resolve(typeof(Driver))).DefaultWait);
    }
}
