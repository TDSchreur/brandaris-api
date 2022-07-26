using Brandaris.Features.Models;

namespace Brandaris.Features.GetProduct;

public record GetProductResponse(ProductModel Value, bool Success = true) : ResponseBase<ProductModel>(Value, Success);