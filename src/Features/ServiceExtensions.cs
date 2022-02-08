using Brandaris.Features.GetPerson;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Brandaris.Features;

public static class ServiceExtensions
{
    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddMediatR(typeof(GetPersonHandler).Assembly);

        return services;
    }
}
