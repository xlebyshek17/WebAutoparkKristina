using AutoparkWebEF.BLL.DTO;
using AutoparkWebEF.BLL.Interfaces;
using AutoparkWebEF.BLL.Services;
using AutoparkWebEF.DAL.EF;
using AutoparkWebEF.DAL.Interfaces;
using AutoparkWebEF.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoparkWebEF
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            DbContextOptionsBuilder<AutoparkContext> optionsBuilder = new DbContextOptionsBuilder<AutoparkContext>();
            optionsBuilder.EnableSensitiveDataLogging(true);
            
            // TODO: Please read how to setup DbContext by DI. Because you are using useless code here.
            services.AddScoped<IUnitOfWork, EFUnitOfWork>(ptovider => new EFUnitOfWork(optionsBuilder.UseSqlServer(connection).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options));
            services.AddScoped<IService<VehicleDto>, VehicleService>(provider => new VehicleService(new EFUnitOfWork(optionsBuilder.UseSqlServer(connection).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options)));
            services.AddScoped<IService<VehicleTypeDto>, VehicleTypeService>(provider => new VehicleTypeService(new EFUnitOfWork(optionsBuilder.UseSqlServer(connection).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options)));
            services.AddScoped<IService<SparePartDto>, SparePartsService>(provider => new SparePartsService(new EFUnitOfWork(optionsBuilder.UseSqlServer(connection).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options)));
            services.AddScoped<IService<OrderDto>, OrderService>(provider => new OrderService(new EFUnitOfWork(optionsBuilder.UseSqlServer(connection).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options)));
            services.AddScoped<IService<OrderItemDto>, OrderItemService>(provider => new OrderItemService(new EFUnitOfWork(optionsBuilder.UseSqlServer(connection).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options)));
            //
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
