using MediatR;

namespace Features.AddProduct
{
    public class AddProductCommand : IRequest<AddProductResponse>
    {
        public string Name { get; init; }
    }
}
