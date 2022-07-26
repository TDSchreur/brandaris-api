using System;
using Microsoft.Extensions.DependencyInjection;

namespace IPFiltering;

public static class ServiceCollectionExtensions
{
    public static void AddIpFilter(this IServiceCollection services, Func<IpSafeList> safelistFunc)
    {
        IpSafeList ipSafeList = safelistFunc.Invoke() ?? throw new ArgumentException("IpSafeList is null, check configuration");

        services.AddSingleton(ipSafeList);
    }
}