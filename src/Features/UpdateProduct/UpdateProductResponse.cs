using Features.Models;

namespace Features.UpdateProduct;

public class UpdateProductResponse : ResponseBase<ProductModel>
{
    public UpdateProductResponse(ProductModel value, bool success = true) : base(value, success) { }
}
