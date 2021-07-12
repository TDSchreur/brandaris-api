using Features.Models;

namespace Features.GetProduct
{
    public class GetProductResponse : ResponseBase<ProductModel>
    {
        public GetProductResponse(ProductModel value, bool success = true) : base(value, success) { }
    }
}
