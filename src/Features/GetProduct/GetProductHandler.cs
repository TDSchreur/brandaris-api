using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Entities;
using DataAccess;
using MediatR;

namespace Features.GetProduct
{
    public class GetProductHandler : IRequestHandler<GetProductQuery, GetProductResponse>
    {
        private readonly IQuery<Product> _query;

        public GetProductHandler(IQuery<Product> query) => _query = query;

        public async Task<GetProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            List<ProductModel> products = await _query.Where(x => !request.HasProductIds || request.ProductIds.Contains(x.Id))
                                                      .Where(x => !request.HasName || x.Name == request.Name)
                                                      .Select(x => new ProductModel
                                                                   {
                                                                       Id = x.Id, Name = x.Name
                                                                   })
                                                      .ToListAsync(cancellationToken);

            return new GetProductResponse(products);
        }
    }
}
