using Features.Models;

namespace Features.AddProduct;

public record AddProductResponse(ProductModel Value, bool Success = true) : ResponseBase<ProductModel>(Value, Success);
