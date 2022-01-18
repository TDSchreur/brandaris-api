using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public static class QueryExtensions
{
    public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default) => EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(source, cancellationToken);

    public static Task<List<TSource>> ToListAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default) => EntityFrameworkQueryableExtensions.ToListAsync(source, cancellationToken);
}
