using Microsoft.AspNetCore.Mvc;

namespace TestFrontEnd.Controllers;

[Route("[controller]")]
public class ErrorController : ControllerBase
{
    [HttpGet]
    public ActionResult<ProblemDetails> Error() => Problem();
}
