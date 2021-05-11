using System.Collections.Generic;
using System.Threading.Tasks;
using Brandaris.Api.Features.GetConfig;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Brandaris.Api.Features
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigController : BaseApiController
    {
        private readonly ILogger<ConfigController> _logger;

        public ConfigController(IMediator mediator,
                                ILogger<ConfigController> logger) : base(mediator)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<ActionResult<GetConfigResponse>> Get()
        {
            using var logscope = _logger.BeginScope(new Dictionary<string, object>
                                                    {
                                                        ["User"] = "T.D.Schreur",
                                                        ["BIC"] = "ABC123"
                                                    });

            return await Mediator.Send(new GetConfigQuery());
        }
    }
}
