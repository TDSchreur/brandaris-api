using MediatR;

namespace Features.UpdateProduct
{
    public class UpdateProductCommand : IRequest<UpdateProductResponse>
    {
        public int Id { get; init; }

        public string Name { get; init; }
    }
}
