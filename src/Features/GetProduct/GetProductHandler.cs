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
            ProductModel product = await _query.Where(x => request.Id == x.Id)
                                               .Select(x => new ProductModel
                                                            {
                                                                Id = x.Id, Name = x.Name
                                                            })
                                               .FirstOrDefaultAsync(cancellationToken);

            return new GetProductResponse(product);
        }
    }
}
