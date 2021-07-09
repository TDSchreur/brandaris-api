using System;
using System.Collections.Generic;
using MediatR;

namespace Features.GetProduct
{
    public class GetProductQuery : IRequest<GetProductResponse>
    {
        public bool HasName => !string.IsNullOrWhiteSpace(Name);

        public bool HasProductIds => ProductIds.Count > 0;

        public string Name { get; init; }

        public IReadOnlyCollection<int> ProductIds { get; init; } = Array.Empty<int>();
    }
}
