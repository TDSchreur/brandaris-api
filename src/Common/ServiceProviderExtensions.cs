using Microsoft.Extensions.DependencyInjection;

namespace Brandaris.Common;

public static class ServiceProviderExtensions
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services.AddScoped<IIdentityHelper, IdentityHelper>();

        return services;
    }
}