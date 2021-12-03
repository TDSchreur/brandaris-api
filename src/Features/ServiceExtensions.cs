using Features.GetPerson;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Features;

public static class ServiceExtensions
{
    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddMediatR(typeof(GetPersonsHandler).Assembly);

        return services;
    }
}
