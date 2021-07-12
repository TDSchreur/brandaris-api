using MediatR;

namespace Features.GetProduct
{
    public class GetProductQuery : IRequest<GetProductResponse>
    {
        public int Id { get; init; }
    }
}
