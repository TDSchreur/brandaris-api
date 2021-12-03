using System.Linq.Expressions;

namespace DataAccess;

public interface ICommand<TEntity>
    where TEntity : IEntity
{
    void Add(params TEntity[] entity);

    Task<int> SaveChangesAsync(CancellationToken token = default);

    void Update<TProperty>(TEntity entity, params Expression<Func<TEntity, TProperty>>[] propertyExpressions);
}
