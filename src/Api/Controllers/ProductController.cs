using System.Threading.Tasks;
using Features.GetPerson;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Brandaris.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator) => _mediator = mediator;

        [HttpGet("")]
        public async Task<ActionResult<GetPersonsResponse>> Get([FromQuery] GetPersonsQuery query) => await _mediator.Send(query);
    }
}
