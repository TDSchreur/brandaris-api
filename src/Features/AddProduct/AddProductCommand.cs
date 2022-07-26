using MediatR;

namespace Brandaris.Features.AddProduct;

public record AddProductCommand(string Name) : IRequest<AddProductResponse>;