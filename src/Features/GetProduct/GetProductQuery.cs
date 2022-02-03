using MediatR;

namespace Brandaris.Features.GetProduct;

public record GetProductQuery(int Id) : IRequest<GetProductResponse>;
