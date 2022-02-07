using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data.Entities;
using Brandaris.DataAccess;
using Brandaris.Features.Models;
using MediatR;

namespace Brandaris.Features.GetProduct;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, GetProductsResponse>
{
    private readonly IQuery<Product> _query;

    public GetProductsHandler(IQuery<Product> query) => _query = query;

    public async Task<GetProductsResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        List<ProductModel> products = await _query.Where(x => !request.HasProductIds || request.ProductIds.Contains(x.Id))
                                                  .Where(x => !request.HasName || x.Name == request.Name)
                                                  .Select(x => new ProductModel(x.Id, x.Name))
                                                  .ToListAsync(cancellationToken);

        return new GetProductsResponse(products);
    }
}
