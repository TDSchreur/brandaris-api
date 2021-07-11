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
        public async Task<ActionResult<GetPersonsResponse>> GetPersons([FromQuery] GetPersonsQuery query) => (await _mediator.Send(query)).FormatResponse();

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetPersonResponse>> GetPerson([FromRoute] GetPersonQuery query) => (await _mediator.Send(query)).FormatResponse();
    }
}
