using System.Linq.Expressions;

namespace DataAccess;

public interface IQuery<TEntity>
    where TEntity : IEntity
{
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    IQueryable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector);

    Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken = default);

    IQuery<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
}
