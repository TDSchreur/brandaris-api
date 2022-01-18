using Features.Models;

namespace Features.GetProduct;

public record GetProductsResponse(ICollection<ProductModel> Value, bool Success = true) : ResponseBase<ICollection<ProductModel>>(Value, Success);
