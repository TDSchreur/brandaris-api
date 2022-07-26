using System.Threading.Tasks;
using Brandaris.Features.AddPerson;
using Brandaris.Features.ApprovePerson;
using Brandaris.Features.GetPerson;
using Brandaris.Features.GetPersons;
using Brandaris.Features.UpdatePerson;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Brandaris.Api.Controllers;

[ApiController]
[Authorize("GetPersonPolicy")]
[Route("api/[controller]")]
public class PersonController : Controller
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(AddPersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AddPersonResponse>> AddPerson([FromBody] AddPersonCommand command)
    {
        return (await _mediator.Send(command)).FormatResponse();
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(GetPersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetPersonResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApprovePersonsResponse>> ApprovePerson([FromRoute] ApprovePersonCommand command)
    {
        return (await _mediator.Send(command)).FormatResponse();
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GetPersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetPersonResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetPersonResponse>> GetPerson([FromRoute] GetPersonQuery query)
    {
        return (await _mediator.Send(query)).FormatResponse();
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(GetPersonsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetPersonsResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetPersonsResponse>> GetPersons([FromQuery] GetPersonsQuery query)
    {
        return (await _mediator.Send(query)).FormatResponse();
    }

    [HttpPut("")]
    [ProducesResponseType(typeof(GetPersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetPersonResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdatePersonResponse>> UpdatePerson([FromBody] UpdatePersonCommand query)
    {
        return (await _mediator.Send(query)).FormatResponse();
    }
}