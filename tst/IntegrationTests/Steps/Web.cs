using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace IntegrationTests.Steps;

[Binding]
public class Web
{
    private readonly IWebDriver _driver;
    private readonly IWait<IWebDriver> _wait;
    private readonly IConfiguration _config;

    public Web(IWebDriver driver, IWait<IWebDriver> wait, IConfiguration config)
    {
        _driver = driver;
        _wait = wait;
        _config = config;
    }

    [Given(@"we go the the website")]
    public void GivenWeGoTheTheWebsite()
    {
        _driver.Navigate().GoToUrl(_config["appUrl"]);
    }

    [Given(@"we log in")]
    public async Task GivenWeLogIn()
    {
        IWebElement userNameElement = _wait.Until(x => x.FindElement(By.Name("loginfmt")));
        userNameElement.SendKeys("dnb@azure.tdschreur.nl");
        userNameElement.SendKeys(Keys.Enter);

        await Task.Delay(1000); // animatie afwachten...

        IWebElement passwordElement = _wait.Until(x => x.FindElement(By.Name("passwd")));
        passwordElement.SendKeys("F26qYfUQqYXMep");
        passwordElement.SendKeys(Keys.Enter);

        await Task.Delay(1000); // animatie afwachten...

        IWebElement button = _wait.Until(x => x.FindElement(By.Id("idSIButton9")));
        button.Click();
    }

    [Then(@"we should we back in the application")]
    public void ThenWeShouldWeBackInTheApplication()
    {
        Assert.StartsWith(_config["appUrl"], _driver.Url);
    }
}
