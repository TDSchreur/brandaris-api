using MediatR;

namespace Features.AddProduct;

public record AddProductCommand(string Name) : IRequest<AddProductResponse>;
