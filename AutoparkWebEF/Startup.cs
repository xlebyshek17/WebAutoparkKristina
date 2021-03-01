using AutoMapper;
using AutoparkWebEF.BLL.DTO;
using AutoparkWebEF.BLL.Interfaces;
using AutoparkWebEF.BLL.Services;
using AutoparkWebEF.DAL.EF;
using AutoparkWebEF.DAL.Entities;
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

            services.AddDbContext<AutoparkContext>(options => options.UseSqlServer(connection));
            services.AddMvc();
            services.AddScoped<GenericRepository<Order>, OrderRepository>();
            services.AddScoped<GenericRepository<OrderItem>, OrderItemsRepository>();
            services.AddScoped<GenericRepository<Vehicle>, VehicleRepository>();
            services.AddScoped<GenericRepository<VehicleType>, VehicleTypeRepository>();
            services.AddScoped<GenericRepository<SparePart>, SparePartRepository>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped<IService<VehicleDto>, VehicleService>();
            services.AddScoped<IService<VehicleTypeDto>, VehicleTypeService>();
            services.AddScoped<IService<SparePartDto>, SparePartsService>();
            services.AddScoped<IService<OrderDto>, OrderService>();
            services.AddScoped<IService<OrderItemDto>, OrderItemService>();
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddControllersWithViews();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderItem, OrderItemDto>();
                cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<SparePart, SparePartDto>();
            }).CreateMapper();
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
