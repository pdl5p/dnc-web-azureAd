using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace ConsoleApplication
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                //builder.AddApplicationInsightsSettings(developerMode: true);
            }

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddOptions();

            //services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));

            services.AddMvc();

            services.AddAuthentication(options =>
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme
            );
            //services.AddApplicationInsightsTelemetry(Configuration);

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            //app.UseApplicationInsightsRequestTelemetry();

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseApplicationInsightsExceptionTelemetry();

            //app.UseMiddleware<CookieMiddleware>();

            app.UseStaticFiles();

            app.UseCookieAuthentication();

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions{

                ClientId = "1c5b9d21-5a08-46e0-a7f2-3e445a0ab672",
                ClientSecret = "nAf5+Acn7csm53TJH1QkHvkogC0mVcBTvonHk77yv3E=",
                Authority = "https://login.microsoftonline.com/fb86ec9d-f0a6-4792-8451-ab10a18bbbbc",
                CallbackPath = "/signin-oidc",
                ResponseType = OpenIdConnectResponseType.CodeIdToken
            });
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}"
                );
            });
        }
    }
}