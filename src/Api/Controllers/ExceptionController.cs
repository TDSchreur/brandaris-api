using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Brandaris.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class ExceptionController : ControllerBase
{
    private readonly ILogger<ExceptionController> _logger;

    public ExceptionController(ILogger<ExceptionController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public void GenerateException()
    {
        try
        {
            throw new Exception("Exception for testing!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error in {controller}", nameof(ExceptionController));

            throw;
        }
    }
}