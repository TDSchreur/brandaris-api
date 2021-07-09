using System.Collections.Generic;

namespace Features.GetProduct
{
    public class GetProductResponse : ResponseBase<ICollection<ProductModel>>
    {
        public GetProductResponse(ICollection<ProductModel> value, bool success = true) : base(value, success) { }
    }
}
