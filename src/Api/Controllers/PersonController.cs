using System.Threading.Tasks;
using Features.GetPerson;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Brandaris.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator) => _mediator = mediator;

        [HttpGet("")]
        public async Task<ActionResult<GetPersonResponse>> Get([FromQuery] GetPersonQuery query) => await _mediator.Send(query);
    }
}
