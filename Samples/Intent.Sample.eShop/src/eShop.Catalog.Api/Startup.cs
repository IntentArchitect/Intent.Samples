using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.Catalog.Api.Configuration;
using eShop.Catalog.Api.Filters;
using eShop.Catalog.Application;
using eShop.Catalog.Domain.Entities;
using eShop.Catalog.Infrastructure;
using eShop.Catalog.Infrastructure.Persistence;
using Intent.RoslynWeaver.Attributes;
using MassTransit.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AspNetCore.Startup", Version = "1.0")]

namespace eShop.Catalog.Api
{
    [IntentManaged(Mode.Merge)]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(
                opt =>
                {
                    opt.Filters.Add<ExceptionFilter>();
                });
            services.AddApplication(Configuration);
            services.ConfigureCors(Configuration);
            services.ConfigureProblemDetails();
            services.AddInfrastructure(Configuration);
            services.ConfigureSwagger(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();
            app.UseExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwashbuckle(Configuration);

            // [IntentIgnore]
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                context.CatalogBrands.AddRange(
                    new CatalogBrand { Id = 1, Brand = "Contoso Bikes" },
                    new CatalogBrand { Id = 2, Brand = "AdventureWorks" },
                    new CatalogBrand { Id = 3, Brand = "Fabrikam Cycles" },
                    new CatalogBrand { Id = 4, Brand = "Litware" },
                    new CatalogBrand { Id = 5, Brand = "World Importers" },
                    new CatalogBrand { Id = 6, Brand = "Tailspin Co." },
                    new CatalogBrand { Id = 7, Brand = "Northwind" },
                    new CatalogBrand { Id = 8, Brand = "Proseware" },
                    new CatalogBrand { Id = 9, Brand = "Blue Yonder" },
                    new CatalogBrand { Id = 10, Brand = "Fourth Coffee" },
                    new CatalogBrand { Id = 11, Brand = "Wingtip" },
                    new CatalogBrand { Id = 12, Brand = "City Power" },
                    new CatalogBrand { Id = 13, Brand = "Lucerne" },
                    new CatalogBrand { Id = 14, Brand = "Vista Trail" },
                    new CatalogBrand { Id = 15, Brand = "Southridge" }
                );

                context.CatalogTypes.AddRange(
                    new CatalogType { Id = 1, Type = "Bike" },
                    new CatalogType { Id = 2, Type = "Frame" },
                    new CatalogType { Id = 3, Type = "Fork" },
                    new CatalogType { Id = 4, Type = "Chain" },
                    new CatalogType { Id = 5, Type = "Crankset" },
                    new CatalogType { Id = 6, Type = "Pedal" },
                    new CatalogType { Id = 7, Type = "Handlebar" },
                    new CatalogType { Id = 8, Type = "Brake" },
                    new CatalogType { Id = 9, Type = "Seat" },
                    new CatalogType { Id = 10, Type = "Wheel" },
                    new CatalogType { Id = 11, Type = "Tire" },
                    new CatalogType { Id = 12, Type = "Jersey" },
                    new CatalogType { Id = 13, Type = "Shorts" },
                    new CatalogType { Id = 14, Type = "Helmet" },
                    new CatalogType { Id = 15, Type = "Glove" }
                );


                context.CatalogItems.Add(new CatalogItem
                {
                    Id = 1,
                    AvailableStock = 15,
                    CatalogBrandId = 1,
                    CatalogTypeId = 1,
                    Description = "Contoso Bikes bike designed for high performance and durability. Ideal for cyclists who demand quality.",
                    MaxStockThreshold = 100,
                    Name = "Contoso Bikes Bike CON-BIK-001",
                    OnReorder = false,
                    PictureFileName = "item1.jpg",
                    PictureUri = "",
                    Price = 1500,
                    RestockThreshold = 10
                });

                context.CatalogItems.Add(new CatalogItem
                {
                    Id = 2,
                    AvailableStock = 19,
                    CatalogBrandId = 1,
                    CatalogTypeId = 1,
                    Description = "Contoso Bikes bike designed for high performance and durability. Ideal for cyclists who demand quality.",
                    MaxStockThreshold = 100,
                    Name = "Contoso Bikes Bike CON-BIK-002",
                    OnReorder = false,
                    PictureFileName = "item2.jpg",
                    PictureUri = "",
                    Price = 395,
                    RestockThreshold = 10
                });

                context.CatalogItems.Add(new CatalogItem
                {
                    Id = 3,
                    AvailableStock = 11,
                    CatalogBrandId = 1,
                    CatalogTypeId = 2,
                    Description = "Contoso Bikes frame designed for high performance and durability. Ideal for cyclists who demand quality.",
                    MaxStockThreshold = 100,
                    Name = "Contoso Bikes Frame CON-FRA-003",
                    OnReorder = true,
                    PictureFileName = "item3.jpg",
                    PictureUri = "",
                    Price = 1723,
                    RestockThreshold = 10
                });

                context.CatalogItems.Add(new CatalogItem
                {
                    Id = 11,
                    AvailableStock = 13,
                    CatalogBrandId = 1,
                    CatalogTypeId = 5,
                    Description = "Contoso Bikes crankset designed for high performance and durability. Ideal for cyclists who demand quality.",
                    MaxStockThreshold = 100,
                    Name = "Contoso Bikes Crankset CON-CRA-011",
                    OnReorder = false,
                    PictureFileName = "item11.jpg",
                    PictureUri = "",
                    Price = 1965,
                    RestockThreshold = 10
                });

                context.CatalogItems.Add(new CatalogItem
                {
                    Id = 12,
                    AvailableStock = 5,
                    CatalogBrandId = 1,
                    CatalogTypeId = 5,
                    Description = "Contoso Bikes crankset designed for high performance and durability. Ideal for cyclists who demand quality.",
                    MaxStockThreshold = 100,
                    Name = "Contoso Bikes Crankset CON-CRA-012",
                    OnReorder = false,
                    PictureFileName = "item12.jpg",
                    PictureUri = "",
                    Price = 128,
                    RestockThreshold = 10
                });

                context.CatalogItems.Add(new CatalogItem
                {
                    Id = 13,
                    AvailableStock = 8,
                    CatalogBrandId = 1,
                    CatalogTypeId = 6,
                    Description = "Contoso Bikes pedal designed for high performance and durability. Ideal for cyclists who demand quality.",
                    MaxStockThreshold = 100,
                    Name = "Contoso Bikes Pedal CON-PED-013",
                    OnReorder = true,
                    PictureFileName = "item13.jpg",
                    PictureUri = "",
                    Price = 728,
                    RestockThreshold = 10
                });

                context.CatalogItems.Add(new CatalogItem
                {
                    Id = 14,
                    AvailableStock = 12,
                    CatalogBrandId = 1,
                    CatalogTypeId = 6,
                    Description = "Contoso Bikes pedal designed for high performance and durability. Ideal for cyclists who demand quality.",
                    MaxStockThreshold = 100,
                    Name = "Contoso Bikes Pedal CON-PED-014",
                    OnReorder = false,
                    PictureFileName = "item14.jpg",
                    PictureUri = "",
                    Price = 415,
                    RestockThreshold = 10
                });

                context.CatalogItems.Add(new CatalogItem
                {
                    Id = 15,
                    AvailableStock = 17,
                    CatalogBrandId = 1,
                    CatalogTypeId = 7,
                    Description = "Contoso Bikes handlebar engineered for control and comfort on rugged terrain.",
                    MaxStockThreshold = 100,
                    Name = "Contoso Bikes Handlebar CON-HAN-015",
                    OnReorder = true,
                    PictureFileName = "item15.jpg",
                    PictureUri = "",
                    Price = 689,
                    RestockThreshold = 10
                });

                context.CatalogItems.Add(new CatalogItem
                {
                    Id = 16,
                    AvailableStock = 9,
                    CatalogBrandId = 1,
                    CatalogTypeId = 7,
                    Description = "Contoso Bikes handlebar made from lightweight aluminum for endurance rides.",
                    MaxStockThreshold = 100,
                    Name = "Contoso Bikes Handlebar CON-HAN-016",
                    OnReorder = false,
                    PictureFileName = "item16.jpg",
                    PictureUri = "",
                    Price = 534,
                    RestockThreshold = 10
                });

                context.CatalogItems.Add(new CatalogItem
                {
                    Id = 17,
                    AvailableStock = 6,
                    CatalogBrandId = 1,
                    CatalogTypeId = 8,
                    Description = "Contoso Bikes brake system designed for precise stopping power on steep descents.",
                    MaxStockThreshold = 100,
                    Name = "Contoso Bikes Brake CON-BRA-017",
                    OnReorder = true,
                    PictureFileName = "item17.jpg",
                    PictureUri = "",
                    Price = 710,
                    RestockThreshold = 10
                });

                context.CatalogItems.Add(new CatalogItem
                {
                    Id = 18,
                    AvailableStock = 10,
                    CatalogBrandId = 1,
                    CatalogTypeId = 8,
                    Description = "Contoso Bikes high-performance disc brakes for responsive control.",
                    MaxStockThreshold = 100,
                    Name = "Contoso Bikes Brake CON-BRA-018",
                    OnReorder = false,
                    PictureFileName = "item18.jpg",
                    PictureUri = "",
                    Price = 858,
                    RestockThreshold = 10
                });

                context.CatalogItems.Add(new CatalogItem
                {
                    Id = 19,
                    AvailableStock = 14,
                    CatalogBrandId = 1,
                    CatalogTypeId = 9,
                    Description = "Contoso Bikes ergonomic seat built for long-distance comfort.",
                    MaxStockThreshold = 100,
                    Name = "Contoso Bikes Seat CON-SEA-019",
                    OnReorder = true,
                    PictureFileName = "item19.jpg",
                    PictureUri = "",
                    Price = 320,
                    RestockThreshold = 10
                });


                context.SaveChanges();
            }
        }
    }
}