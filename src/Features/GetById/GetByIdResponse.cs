namespace Features.GetById
{
    public class GetByIdResponse<TResponseModel> : ResponseBase<TResponseModel> where TResponseModel : IResponseModel
    {
        public GetByIdResponse(TResponseModel value, bool success = true) : base(value, success) { }
    }
}
