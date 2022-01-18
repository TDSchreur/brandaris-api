using MediatR;

namespace Features.GetProduct;

public record GetProductQuery(int Id) : IRequest<GetProductResponse>;
