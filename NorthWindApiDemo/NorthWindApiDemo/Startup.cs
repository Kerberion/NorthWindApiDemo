using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NorthWindApiDemo.EFModels;
using NorthWindApiDemo.Services;

namespace NorthWindApiDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(); //reciba las peticiones http

            //Nuevas configuraciones
            //Con esto podemos usar el contexto de datos
            services.AddDbContext<NorthWindContext>( options =>
            {
                options.UseSqlServer("Server=.\\KERBEROSDEV;Database=NorthWind;User Id = sa;Password=SoyElAveDeHermes2906");
            });

            //Configurar servicios personalizados
            services.AddScoped<ICustomerRepository, CustomerRepository>();
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
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages();

            app.UseMvc();


            //tipo de ruteo por convención no recomendable
            //app.UseMvc(config =>
            //{
            //    config.MapRoute(name: "Default",
            //        template: "{controller}/{action}/{id?})",
            //        defaults: new
            //        {
            //            controller = "Home",
            //            action = "Index"
            //        });
            //});

            //app.Run(async (context) =>
            //{
            //    throw new Exception("Testint exceptions");
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
