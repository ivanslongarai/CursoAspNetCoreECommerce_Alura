using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeHome.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHome
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
            // Used to Add Services
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession();

            string connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<IDataService, DataService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            services.AddTransient<IRegisterRepository, RegisterRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {

            // Used to Consume and set its Configuration
            // IServiceProvider was injected with Dependency Injection

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Order/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Order}/{action=Carousel}/{internalid?}");
            });

            serviceProvider
                .GetService<IDataService>()
                .DbInitialize();
        }
    }
}
