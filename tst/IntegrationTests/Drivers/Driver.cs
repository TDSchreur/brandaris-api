using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace IntegrationTests.Drivers;

public class Driver : IDisposable
{
    public Driver()
    {
        EdgeOptions options = new();
        options.AddArgument("--inprivate");

        WebDriver = new EdgeDriver(options);
        DefaultWait = new DefaultWait<IWebDriver>(WebDriver)
        {
            Timeout = TimeSpan.FromSeconds(5), PollingInterval = TimeSpan.FromSeconds(1)
        };
        DefaultWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
    }

    public IWebDriver WebDriver { get; init; }

    public IWait<IWebDriver> DefaultWait { get; init; }

    public void Dispose()
    {
        WebDriver?.Close();
        WebDriver?.Quit();
        WebDriver?.Dispose();
    }
}
