using System.Threading.Tasks;
using Data.Entities;
using Features.GetById;
using Features.GetPerson;
using MediatR;
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

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GetByIdResponse<PersonModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetByIdResponse<PersonModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetByIdResponse<PersonModel>>> GetPerson([FromRoute] int id) =>
            (await _mediator.Send(new GetByIdRequest<Person, PersonModel>
                                  {
                                      Id = id,
                                      Selector = x => new PersonModel
                                                      {
                                                          Id = x.Id,
                                                          FirstName = x.FirstName,
                                                          LastName = x.LastName
                                                      }
                                  })).FormatResponse();

        [HttpGet("")]
        [ProducesResponseType(typeof(GetByIdResponse<PersonModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetByIdResponse<PersonModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetPersonsResponse>> GetPersons([FromQuery] GetPersonsQuery query) => (await _mediator.Send(query)).FormatResponse();
    }
}
