using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Features.GetConfig;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Brandaris.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly ILogger<ConfigController> _logger;
        private readonly IMediator _mediator;

        public ConfigController(IMediator mediator,
                                ILogger<ConfigController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<ActionResult<GetConfigResponse>> Get()
        {
            using IDisposable scope = _logger.BeginScope(new Dictionary<string, object>
                                                         {
                                                             ["User"] = "T.D.Schreur", ["BIC"] = "ABC123"
                                                         });

            return await _mediator.Send(new GetConfigQuery());
        }
    }
}
