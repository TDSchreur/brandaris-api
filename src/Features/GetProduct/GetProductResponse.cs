using Features.Models;

namespace Features.GetProduct;

public record GetProductResponse(ProductModel Value, bool Success = true) : ResponseBase<ProductModel>(Value, Success);
