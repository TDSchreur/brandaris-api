using System.Threading.Tasks;
using Data.Entities;
using Features.GetById;
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
        public async Task<ActionResult<GetProductResponse>> Get([FromQuery] GetProductQuery query) => await _mediator.Send(query);

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GetByIdResponse<ProductModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetByIdResponse<ProductModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetByIdResponse<ProductModel>>> GetProduct([FromRoute] int id) =>
            (await _mediator.Send(new GetByIdRequest<Product, ProductModel>
                                  {
                                      Id = id,
                                      Selector = x => new ProductModel
                                                      {
                                                          Id = x.Id, Name = x.Name
                                                      }
                                  })).FormatResponse();
    }
}
