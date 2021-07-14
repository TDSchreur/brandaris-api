using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestFrontEnd.Controllers
{
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
#pragma warning disable CA1054

        [HttpGet("Login")]
        [AllowAnonymous]
        public IActionResult SignIn([FromQuery] string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = "/";
            }

            return Challenge(
                             new AuthenticationProperties
                             {
                                 RedirectUri = returnUrl
                             },
                             OpenIdConnectDefaults.AuthenticationScheme);
        }

        [HttpGet("Logout")]
#pragma warning disable CS0114
        public SignOutResult SignOut() => SignOut(
                                                  new AuthenticationProperties
                                                  {
                                                      RedirectUri = "/"
                                                  },
                                                  CookieAuthenticationDefaults.AuthenticationScheme,
                                                  OpenIdConnectDefaults.AuthenticationScheme);
    }
}
