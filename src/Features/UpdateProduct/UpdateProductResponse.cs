using Brandaris.Features.Models;

namespace Brandaris.Features.UpdateProduct;

public record UpdateProductResponse(ProductModel Value, bool Success = true) : ResponseBase<ProductModel>(Value, Success);
