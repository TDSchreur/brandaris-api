using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Brandaris.Api.Features
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected BaseApiController(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected IMediator Mediator { get; }
    }
}
