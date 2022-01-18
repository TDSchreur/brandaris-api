using Features.Models;

namespace Features.UpdateProduct;

public record UpdateProductResponse(ProductModel Value, bool Success = true) : ResponseBase<ProductModel>(Value, Success);
