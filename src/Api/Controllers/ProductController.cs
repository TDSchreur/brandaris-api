using System.Threading.Tasks;
using Features.GetProduct;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(typeof(GetProductsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetProductsResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetProductsResponse>> Get([FromQuery] GetProductsQuery query) => await _mediator.Send(query);

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetProductResponse>> GetProduct([FromRoute] GetProductQuery query) => (await _mediator.Send(query)).FormatResponse();
    }
}
