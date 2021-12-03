using Microsoft.AspNetCore.Mvc;

namespace Brandaris.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet("")]
    public ActionResult<IEnumerable<KeyValuePair<string, string>>> Get()
    {
        IEnumerable<KeyValuePair<string, string>> claims = HttpContext.User.Claims
                                                                      .OrderBy(x => x.Type)
                                                                      .Select(kv => new KeyValuePair<string, string>(kv.Type, kv.Value));

        return Ok(claims);
    }
}
