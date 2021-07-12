using System;
using System.Collections.Generic;
using Features.GetPerson;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Brandaris.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "Brandaris V1"); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health/readiness");

                endpoints.MapHealthChecks("/health/liveness", new HealthCheckOptions
                                                              {
                                                                  Predicate = _ => false
                                                              });

                endpoints.MapControllers(); //// .RequireAuthorization();
            });

            app.Run(async context => { await context.Response.WriteAsync($"{Environment.MachineName}: Hello world! Request path: {context.Request.Path}"); });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(opt =>
            {
                AuthorizationPolicy defaultPolicy = new AuthorizationPolicyBuilder("AAD")
                                                   .RequireAuthenticatedUser()
                                                   .Build();

                opt.DefaultPolicy = defaultPolicy;
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("AAD", opt =>
            {
                opt.RequireHttpsMetadata = true;
                opt.Authority = $"https://login.microsoftonline.com/{Configuration["Authentication:TenantId"]}";
                opt.TokenValidationParameters = new TokenValidationParameters
                                                {
                                                    // Both App ID URI and client id are valid audiences in the access token
                                                    ValidAudiences = new List<string>
                                                                     {
                                                                         Configuration["Authentication:AppIdUri"], Configuration["Authentication:ClientId"]
                                                                     }
                                                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                                   {
                                       Title = "Brandaris", Version = "v1"
                                   });
                c.UseAllOfForInheritance();
            });

            services.AddControllers()
                    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<AddPersonCommandValidator>(null, ServiceLifetime.Transient));

            services.AddHealthChecks();

            services.AddApplicationInsightsTelemetry();
        }
    }
}
