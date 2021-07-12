﻿using System.Collections.Generic;

namespace Features.GetProduct
{
    public class GetProductsResponse : ResponseBase<ICollection<ProductModel>>
    {
        public GetProductsResponse(ICollection<ProductModel> value, bool success = true) : base(value, success) { }
    }
}
