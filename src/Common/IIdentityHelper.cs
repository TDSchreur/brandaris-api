using System;
using Microsoft.Extensions.DependencyInjection;

namespace Brandaris.Common;

public interface IIdentityHelper
{
    string GetName();

    Guid GetOid();
}

public static class ServiceProviderExtensions
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services.AddScoped<IIdentityHelper, IdentityHelper>();

        return services;
    }
}
