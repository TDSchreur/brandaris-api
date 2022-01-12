using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class ServiceProviderExtensions
{
    public static IServiceCollection AddDataAccess<TContext>(this IServiceCollection services, Func<string> getConnectionString)
        where TContext : DbContext
    {
        string connectionstring = getConnectionString.Invoke();

        services.AddDbContextPool<TContext>(o => o.UseSqlServer(connectionstring));
        services.AddTransient<DbContext, TContext>();
        services.AddTransient(typeof(IQuery<>), typeof(Query<>));
        services.AddTransient(typeof(ICommand<>), typeof(Command<>));

        return services;
    }
}
