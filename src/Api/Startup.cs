using System;
using System.Collections.Generic;
using Features.GetPerson;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            app.UseAuthentication();

            app.UseAuthorization();

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
            services.AddMediatR(typeof(GetPersonsHandler).Assembly);

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
                opt.RequireHttpsMetadata = false;
                opt.Authority = $"https://login.microsoftonline.com/{Configuration["Authentication:TenantId"]}";
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    // Both App ID URI and client id are valid audiences in the access token
                    ValidAudiences = new List<string>
                    {
                        Configuration["Authentication:AppIdUri"],
                        Configuration["Authentication:ClientId"]
                    }
                };
            });

            services.AddControllers();

            services.AddHealthChecks();

            services.AddApplicationInsightsTelemetry();
        }
    }
}
