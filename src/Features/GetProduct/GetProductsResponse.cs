using System.Collections.Generic;
using Brandaris.Features.Models;

namespace Brandaris.Features.GetProduct;

public record GetProductsResponse(ICollection<ProductModel> Value, bool Success = true) : ResponseBase<ICollection<ProductModel>>(Value, Success);