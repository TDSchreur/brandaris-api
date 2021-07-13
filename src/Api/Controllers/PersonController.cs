using System.Threading.Tasks;
using Features.AddPerson;
using Features.GetPerson;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Brandaris.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator) => _mediator = mediator;

        [HttpPost("")]
        [ProducesResponseType(typeof(AddPersonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AddPersonResponse>> AddPerson([FromBody] AddPersonCommand command) => (await _mediator.Send(command)).FormatResponse();

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GetPersonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetPersonResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetPersonResponse>> GetPerson([FromRoute] GetPersonQuery query)
        {
            var claims = User.Claims;

            return (await _mediator.Send(query)).FormatResponse();
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(GetPersonsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetPersonsResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetPersonsResponse>> GetPersons([FromQuery] GetPersonsQuery query) => (await _mediator.Send(query)).FormatResponse();
    }
}
