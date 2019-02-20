using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RestSharp;
using Serilog;

namespace Lemon_WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Seq("http://localhost:5341/")
                .CreateLogger();

            Log.Information("Starting Up");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                //options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddCors();
            services.AddTransient<IRestClient, RestClient>();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // Adds Internationalization
            services.AddLocalization(option => option.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(opt =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("sv"),
                    new CultureInfo("fi"),
                    new CultureInfo("hu"),
                    new CultureInfo("sr"),
                    new CultureInfo("is")
                };

                opt.DefaultRequestCulture = new RequestCulture("en");
                opt.SupportedCultures = supportedCultures;
                opt.SupportedUICultures = supportedCultures;
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("sv"),
                new CultureInfo("fi"),
                new CultureInfo("hu"),
                new CultureInfo("sr"),
                new CultureInfo("is")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedUICultures = supportedCultures,
                SupportedCultures = supportedCultures
            });


            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
               builder.WithOrigins("https://ludowebapp-dev-as.azurewebsites.net"));
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Ludo}/{action=Welcome}/{id?}");
            });
        }
    }
}
