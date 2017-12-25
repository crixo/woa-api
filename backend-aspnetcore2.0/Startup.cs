using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Woa.Data;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json;
using Woa.Infrastructure;

namespace Woa
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
            services.AddDbContext<WoaContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))); 
            
            services.AddMvc()
                        .AddJsonOptions(options => {
                            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        });

            //https://github.com/domaindrivendev/Swashbuckle.AspNetCore
            //https://localhost:5000/swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "ASP.NET Core WOA API",
                    Description = "Web osteopatic aapplication API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Cristiano Degiorgis", Url = "http://www.webprofessor.it" },
                    License = new License { Name = "MIT", Url = "https://en.wikipedia.org/wiki/MIT_License" }
                });

                //Add XML comment document by uncommenting the following
                // var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "MyApi.xml");
                // options.IncludeXmlComments(filePath);

            });

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //This would need to be locked down as needed (very open right now)
            app.UseCors((corsPolicyBuilder) =>
            {
                corsPolicyBuilder.AllowAnyOrigin();
                corsPolicyBuilder.AllowAnyMethod();
                corsPolicyBuilder.AllowAnyHeader();
            });

            app.UseMiddleware<IgnoreRouteMiddleware>();

            //app.UseStaticFiles();

            app.UseMvc();


            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            // Visit http://localhost:5000/swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WOA API V1");
            });

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");

            //    //routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });

            //});

            app.Run(context => {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });

        }




    }
}
