using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data;

namespace Brandaris.DataAccess;

public interface ICommand<TEntity>
    where TEntity : IEntity
{
    void Add(params TEntity[] entity);

    void Attach(TEntity entity);

    void Remove(TEntity entity);

    Task<int> SaveChangesAsync(CancellationToken token = default);

    void Update<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyExpression);
}
