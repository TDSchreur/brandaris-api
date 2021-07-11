using System;
using System.Collections.Generic;
using MediatR;

namespace Features.GetProduct
{
    public class GetProductQuery : IRequest<GetProductResponse>
    {
        public string Name { get; init; }

        public int[] ProductIds { get; init; } = Array.Empty<int>();

        internal bool HasName => !string.IsNullOrWhiteSpace(Name);

        internal bool HasProductIds => ProductIds.Length > 0;
    }
}
