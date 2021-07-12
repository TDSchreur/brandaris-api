using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class Query<TEntity> : IQuery<TEntity>
        where TEntity : class, IEntity
    {
        private IQueryable<TEntity> _query;

        public Query(DbContext context) => _query = context.Set<TEntity>().AsNoTracking();

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => _query.AnyAsync(predicate, cancellationToken);

        public IQueryable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector) => _query.Select(selector);

        public Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken = default) => _query.ToListAsync(cancellationToken);

        public IQuery<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            _query = _query.Where(predicate);

            return this;
        }
    }
}
