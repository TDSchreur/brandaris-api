using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;

namespace TestFrontEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                    .AddOpenIdConnect(opt =>
                    {
                        opt.ClientId = Configuration["Authentication:ClientId"];
                        opt.Authority = $"https://login.microsoftonline.com/{Configuration["Authentication:TenantId"]}";
                        opt.UseTokenLifetime = true;
                        opt.TokenValidationParameters = new TokenValidationParameters { NameClaimType = "name" };

                        opt.Events.OnTokenValidated = async context =>
                        {
                            var tokenAcquisition = context.HttpContext.RequestServices.GetRequiredService<ITokenAcquisition>();
                            context.Success();

                            // Adds the token to the cache, and also handles the incremental consent and claim challenges
                            tokenAcquisition.AddAccountToCacheFromJwt(context, scopes);
                            await Task.FromResult(0);
                        };

                        ////opt.Events = new OpenIdConnectEvents
                        ////{
                        ////    OnRedirectToIdentityProvider = context => OnRedirectToIdentityProvider(context, authOptions),
                        ////    OnAuthorizationCodeReceived = context => OnAuthorizationCodeReceived(context, authOptions, ApplicationContainer)
                        ////};
                    })
                    .AddCookie(opt =>
                    {
                        opt.LoginPath = "/account/signin";
                        opt.LogoutPath = "/account/signout";
                    });

            services.AddAuthorization(options =>
            {
                var defaultPolicy = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme).RequireAuthenticatedUser()
                                                                                                                     .Build();

                options.AddPolicy(CookieAuthenticationDefaults.AuthenticationScheme, defaultPolicy);
            });

            services.AddHealthChecks();

            services.AddApplicationInsightsTelemetry();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.Use(
                async (context, next) =>
                {
                    if (context.Request.Path.Value == "/")
                    {
                        context.Request.Path = new PathString("/index.html");
                    }

                    if (context.Request.Path.Value == "/index.html" &&
                        !context.User.Identity.IsAuthenticated)
                    {
                        await context.ChallengeAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new AuthenticationProperties { RedirectUri = "/" });

                        return;
                    }

                    await next.Invoke();
                });

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health/readiness");

                endpoints.MapHealthChecks("/health/liveness", new HealthCheckOptions
                {
                    Predicate = _ => false
                });

                endpoints.MapControllers().RequireAuthorization();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    ////spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
