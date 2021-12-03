using Features.Models;

namespace Features.AddProduct;

public class AddProductResponse : ResponseBase<ProductModel>
{
    public AddProductResponse(ProductModel value, bool success = true) : base(value, success) { }
}
