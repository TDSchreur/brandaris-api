using Brandaris.Features.GetPerson;
using Brandaris.Features.UpdateProduct;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Brandaris.Features;

public static class ServiceExtensions
{
    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddMediatR(typeof(GetPersonHandler).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<UpdateProductCommandValidator>();

        return services;
    }
}
