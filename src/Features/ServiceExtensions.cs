using Data.Entities;
using Features.GetById;
using Features.GetPerson;
using Features.GetProduct;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Features
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddFeatures(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetPersonsHandler).Assembly);

            services.AddScoped(typeof(IRequestHandler<GetByIdRequest<Person, PersonModel>, GetByIdResponse<PersonModel>>),
                               typeof(GetByIdHandler<Person, PersonModel>));

            services.AddScoped(typeof(IRequestHandler<GetByIdRequest<Product, ProductModel>, GetByIdResponse<ProductModel>>),
                               typeof(GetByIdHandler<Product, ProductModel>));

            return services;
        }
    }
}
