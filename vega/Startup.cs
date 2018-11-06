using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vega.Persistence;

namespace vega {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //'IServiceCollection' is the container of all the dependencies of the application
        public void ConfigureServices (IServiceCollection services) {
            //for dependency injection
            //we need to add the services we use in our controllers
            //example: services.AddTransient<IRepository, Repository>(); -> inject the implementation of the interface

            services.AddScoped<IVehicleRepository, VehicleRepository> ();
            services.AddScoped<IUnitOfWork, UnitOfWork> ();
            services.AddAutoMapper ();

            services.AddDbContext<VegaDbContext> (options =>
                options.UseSqlServer (Configuration.GetConnectionString ("Default")));
            // services.AddDbContext<VegaDbContext> (options => 
            // options.UseSqlServer("server=localhost\\SQLEXPRESS; database=vega; integrated security=sspi;"));
            services.AddMvc ();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
                app.UseWebpackDevMiddleware (new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true //if we change the client files we dont need to reload the page
                });
            } else {
                app.UseExceptionHandler ("/Home/Error");
            }

            app.UseStaticFiles (); //search for images etc.

            app.UseMvc (routes => {
                routes.MapRoute (
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute (
                    name: "spa-fallback",
                    defaults : new { controller = "Home", action = "Index" });
            });
        }
    }
}