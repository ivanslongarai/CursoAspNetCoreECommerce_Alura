using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CursoAspNetCoreECommerce
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            /* Temporary type, each calling of getServices it creates a new instances of the classes */
            //services.AddTransient<IBooksCatalog, BooksCatalog>();
            //services.AddTransient<IBooksReport, BooksReport>();

            /* One instance is created for each Browser's requsition */
            //services.AddScoped<IBooksCatalog, BooksCatalog>();
            //services.AddScoped<IBooksReport, BooksReport>();

            /* It doesn't metter which requisition calls it, it will be working with just one instance of classes bellow */
            var booksCatalog = new BooksCatalog();
            services.AddSingleton<IBooksCatalog>(booksCatalog);
            services.AddSingleton<IBooksReport>(new BooksReport(booksCatalog));

            //                         ^
            // Dependency injection - ´ 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            IBooksCatalog listOfBooks = serviceProvider.GetService<IBooksCatalog>();
            IBooksReport booksReport = serviceProvider.GetService<IBooksReport>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await booksReport.Print(context);
                });
            });
        }
    }
}
