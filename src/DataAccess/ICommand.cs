using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess;

public interface ICommand<TEntity>
    where TEntity : IEntity
{
    void Add(params TEntity[] entity);

    Task<int> SaveChangesAsync(CancellationToken token = default);

    void Update<TProperty>(TEntity entity, params Expression<Func<TEntity, TProperty>>[] propertyExpressions);
}
