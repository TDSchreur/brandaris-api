using System;
using Brandaris.Api.Features;
using MediatR;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health/readiness");

                endpoints.MapHealthChecks("/health/liveness", new HealthCheckOptions {Predicate = _ => false});
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.Run(async context => { await context.Response.WriteAsync($"{Environment.MachineName}: Hello world! Request path: {context.Request.Path}"); });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(BaseApiController).Assembly);

            services.AddControllers();

            services.AddHealthChecks();

            services.AddApplicationInsightsTelemetry();
        }
    }
}
