using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data;
using Microsoft.EntityFrameworkCore;

namespace Brandaris.DataAccess;

public class Query<TEntity> : IQuery<TEntity>
    where TEntity : class, IEntity
{
    private IQueryable<TEntity> _query;

    public Query(DataContext context)
    {
        _query = context.Set<TEntity>().AsNoTracking();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Query{TEntity}" /> class.
    ///     Only for unit testing.
    /// </summary>
    /// <param name="query">The mock query.</param>
    public Query(IQueryable<TEntity> query)
    {
        _query = query;
    }

    public bool Any(Expression<Func<TEntity, bool>> predicate)
    {
        return _query.Any(predicate);
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _query.AnyAsync(predicate, cancellationToken);
    }

    public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _query.FirstOrDefaultAsync(predicate);
    }

    public IQueryable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector)
    {
        return _query.Select(selector);
    }

    public Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken = default)
    {
        return _query.ToListAsync(cancellationToken);
    }

    public IQuery<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
    {
        _query = _query.Where(predicate);

        return this;
    }
}