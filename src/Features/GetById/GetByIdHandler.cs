using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using MediatR;

namespace Features.GetById
{
    public class GetByIdHandler<TEntity, TResponseModel> :
        IRequestHandler<GetByIdRequest<TEntity, TResponseModel>, GetByIdResponse<TResponseModel>>
        where TResponseModel : IResponseModel
        where TEntity : IEntity
    {
        private readonly IQuery<TEntity> _query;

        public GetByIdHandler(IQuery<TEntity> query) => _query = query;

        public async Task<GetByIdResponse<TResponseModel>> Handle(
            GetByIdRequest<TEntity, TResponseModel> request,
            CancellationToken cancellationToken)
        {
            TResponseModel result = await _query.Where(x => x.Id == request.Id).Select(request.Selector).FirstOrDefaultAsync(cancellationToken);

            return new GetByIdResponse<TResponseModel>(result);
        }
    }
}
