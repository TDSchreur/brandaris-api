using MediatR;

namespace Brandaris.Features.UpdateProduct;

public record UpdateProductCommand : IRequest<UpdateProductResponse>
{
    public int Id { get; init; }

    public string Name { get; init; }
}
