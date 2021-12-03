﻿using Features.AddProduct;
using Features.GetProduct;
using Features.UpdateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Brandaris.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator) => _mediator = mediator;

    [HttpPost("")]
    [ProducesResponseType(typeof(AddProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AddProductResponse>> AddProduct([FromBody] AddProductCommand command) => (await _mediator.Send(command)).FormatResponse();

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetProductResponse>> GetProduct([FromRoute] GetProductQuery query) => (await _mediator.Send(query)).FormatResponse();

    [HttpGet("")]
    [ProducesResponseType(typeof(GetProductsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetProductsResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetProductsResponse>> GetProducts([FromQuery] GetProductsQuery query) => await _mediator.Send(query);

    [HttpPatch("")]
    [ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateProductResponse>> UpdateProduct([FromBody] UpdateProductCommand query) => (await _mediator.Send(query)).FormatResponse();
}
