using System;
using System.Linq.Expressions;
using DataAccess;
using MediatR;

namespace Features.GetById
{
    public class GetByIdRequest<TEntity, TResponseModel> : IRequest<GetByIdResponse<TResponseModel>>
        where TResponseModel : IResponseModel
        where TEntity : IEntity
    {
        public int Id { get; init; }

        public Expression<Func<TEntity, TResponseModel>> Selector { get; init; }
    }
}
