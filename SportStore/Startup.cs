using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.Models;
using Microsoft.AspNetCore.Identity;

namespace SportStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //����� ������������ ��� �������� � 2.2 �� 3.1 ������
            //services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreIdentity:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();
            services.AddMvc();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddMemoryCache();
            services.AddSession();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseStatusCodePages();
            app.UseBrowserLink();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: null, pattern: "{category}/Page{productPage:int}", defaults: new { controller = "Product", action = "List" });
                endpoints.MapControllerRoute(name: null, pattern: "Page{productPage:int}", defaults: new { Controller = "Product", action = "List", productPage = 1 });
                endpoints.MapControllerRoute(name: null, pattern: "{category}", defaults: new { Controller = "Product", action = "List", productPage = 1 });
                endpoints.MapControllerRoute(name: null, pattern: "", defaults: new { Controller = "Product", action = "List", productPage = 1 });
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Product}/{action=List}/{id?}");
            });


            //��� �������� �� ���������������� �� ���������
            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);




            //����� ������������ ��� �������� � ������ 2.2 �� 3.1
            //app.UseMvc(endpoints =>
            //{
            //    endpoints.MapRoute(name: "default", template: "{controller=Product}/{action=List}/{id?}");  
            //});
        }
    }
}
