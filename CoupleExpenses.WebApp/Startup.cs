using System;
using System.Threading.Tasks;
using CoupleExpenses.Application;
using CoupleExpenses.Application.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoupleExpenses.Infrastructure;
using CoupleExpenses.WebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace CoupleExpenses.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "clientapp/build";
            });            

            /*services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            services.AddScoped<IUserService, UserService>();*/

            services.AddAuthentication("GuidAuthentication")
                .AddScheme<AuthenticationSchemeOptions, GuidAuthenticationHandler>("GuidAuthentication", null);

            services.AddSingleton<IAuthorizationService, AuthorizationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services.GetAutofacProvider();
        }        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "clientapp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            await ReplayAllEvent(app);
        }

        private async Task ReplayAllEvent(IApplicationBuilder app)
        {
            var commandBus = app.ApplicationServices.GetService<ICommandBus>();
            await commandBus.SendAsync(new ReplayAllEvents());
        }
    }       
}
