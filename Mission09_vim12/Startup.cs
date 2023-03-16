using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mission09_vim12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_vim12
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        
        public IConfiguration Configuration { get; set; }
        public Startup (IConfiguration temp)
        {
            Configuration = temp;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookDBConnection"]);
            });
            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();
            services.AddScoped<IPurchaseRepository, EFPurchaseRepository>();

            //enable the use of razor pages so can add cart
            services.AddRazorPages();

            //set up session to save data between pages
            services.AddDistributedMemoryCache();
            services.AddSession();

            //add service for child class that inherited from Cart
            services.AddScoped<Cart>(x => SessionCart.GetCart(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Corresponds to wwwroot folder
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();
            //Make URLs/slugs neater
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("categorypage","{bookCategory}/Page{pageNum}",new { Controller = "Home", action = "Index" });
                endpoints.MapControllerRoute(
                                    name: "Paging",
                                    pattern: "Page{pageNum}",
                                    defaults: new { Controller = "Home", Action = "Index", pageNum=1}
                                    );
                endpoints.MapControllerRoute("category", "{bookCategory}", new { Controller = "Home", action="Index",pageNum=1 });

                
                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
            });
        }
    }
}
