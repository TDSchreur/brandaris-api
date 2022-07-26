using System;
using MediatR;

namespace Brandaris.Features.GetProduct;

public record GetProductsQuery : IRequest<GetProductsResponse>
{
    internal bool HasName => !string.IsNullOrWhiteSpace(Name);

    internal bool HasProductIds => ProductIds.Length > 0;

    public string Name { get; init; }

    public int[] ProductIds { get; init; } = Array.Empty<int>();
}