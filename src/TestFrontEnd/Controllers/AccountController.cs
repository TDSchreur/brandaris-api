using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestFrontEnd.ServiceAgents;

namespace TestFrontEnd.Controllers;

[Route("[controller]")]
public class AccountController : ControllerBase
{
    [HttpGet("claims")]
    public ActionResult<IEnumerable<KeyValuePair<string, string>>> GetClaims()
    {
        IEnumerable<KeyValuePair<string, string>> claims = HttpContext.User.Claims
                                                                      .OrderBy(x => x.Type)
                                                                      .Select(kv => new KeyValuePair<string, string>(kv.Type, kv.Value));

        return Ok(claims);
    }

    [HttpGet("claims_remote")]
    public async Task<ActionResult<IEnumerable<KeyValuePair<string, string>>>> GetClaimsRemoteAsync([FromServices] IBrandarisApiServiceAgent brandarisApiServiceAgent)
    {
        IEnumerable<KeyValuePair<string, string>> claims = await brandarisApiServiceAgent.GetRemoteClaimsAsync();

        return Ok(claims);
    }

    [HttpGet("Login")]
    [AllowAnonymous]
    public IActionResult SignIn([FromQuery] string returnUrl)
    {
        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            returnUrl = "/";
        }

        return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, OpenIdConnectDefaults.AuthenticationScheme);
    }

    [HttpGet("Logout")]
    public new SignOutResult SignOut()
    {
        return SignOut(new AuthenticationProperties { RedirectUri = "/" }, CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
    }
}