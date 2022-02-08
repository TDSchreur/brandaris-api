using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Brandaris.DataAccess;

public static class ServiceProviderExtensions
{
    public static IServiceCollection AddDataAccess<TContext>(this IServiceCollection services, Func<string> getConnectionString)
        where TContext : DbContext
    {
        string connectionstring = getConnectionString.Invoke();

        services.AddDbContext<TContext>(o =>
        {
            o.UseSqlServer(connectionstring);
            o.EnableSensitiveDataLogging(true);
        });
        services.AddTransient<DbContext, TContext>();
        services.AddTransient(typeof(IQuery<>), typeof(Query<>));
        services.AddTransient(typeof(ICommand<>), typeof(Command<>));

        return services;
    }
}
