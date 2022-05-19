using System;
using Brandaris.Features.AddPerson;
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
using Serilog;

namespace Brandaris.Api;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSerilogRequestLogging();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler(appBuilder => appBuilder.Run(async context =>
            {
                context.Response.StatusCode = 500;
                await context
                     .Response
                     .WriteAsync("An unexpected error on server happened, please try again later.");
            }));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseOpenApi();

        app.UseSwaggerUi3();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health/readiness");

            endpoints.MapHealthChecks("/health/liveness", new HealthCheckOptions
            {
                Predicate = _ => false
            });

            endpoints.MapControllers().RequireAuthorization();
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

            opt.AddPolicy("GetConfigPolicy", policy =>
            {
                policy.Combine(defaultPolicy);
                policy.RequireClaim("roles", "get-config");
            });

            opt.AddPolicy("GetPersonPolicy", policy =>
            {
                policy.Combine(defaultPolicy);
                policy.RequireClaim("scp", "manage-data");
                policy.RequireClaim("roles", "manage-master-data");
            });
        });

        services.AddAuthentication(opt =>
                 {
                     opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                     opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                 })
                .AddJwtBearer("AAD", options =>
                 {
                     options.MapInboundClaims = false;
                     options.RequireHttpsMetadata = true;
                     options.Authority = $"https://login.microsoftonline.com/{Configuration["Authentication:TenantId"]}";
                     options.TokenValidationParameters.ValidateTokenReplay = true;
                     options.TokenValidationParameters.ValidIssuer = $@"https://sts.windows.net/{Configuration["Authentication:TenantId"]}/";
                     options.TokenValidationParameters.ValidateAudience = true;
                     options.TokenValidationParameters.ValidateLifetime = true;
                     options.TokenValidationParameters.ValidateIssuerSigningKey = true;
                     options.TokenValidationParameters.ValidAudiences = new[]
                     {
                         Configuration["Authentication:AppIdUri"], Configuration["Authentication:ClientId"]
                     };
                 });

        services.AddOpenApiDocument(opt => { opt.Title = "Brandaris"; });

        services.AddControllers()
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<AddPersonCommandValidator>(null, ServiceLifetime.Transient));

        services.AddHealthChecks();

        services.AddApplicationInsightsTelemetry();
    }
}
