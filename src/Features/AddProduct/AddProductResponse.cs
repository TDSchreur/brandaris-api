using Brandaris.Features.Models;

namespace Brandaris.Features.AddProduct;

public record AddProductResponse(ProductModel Value, bool Success = true) : ResponseBase<ProductModel>(Value, Success);