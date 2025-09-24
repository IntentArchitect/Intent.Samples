using eShop.Catalog.Application.Common.Eventing;
using eShop.Catalog.Domain.Common.Interfaces;
using eShop.Catalog.Domain.Repositories;
using eShop.Catalog.Infrastructure.Configuration;
using eShop.Catalog.Infrastructure.Eventing;
using eShop.Catalog.Infrastructure.Persistence;
using eShop.Catalog.Infrastructure.Repositories;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Infrastructure.DependencyInjection.DependencyInjection", Version = "1.0")]

namespace eShop.Catalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseInMemoryDatabase("DefaultConnection");
                options.UseLazyLoadingProxies();
            });
            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddTransient<ICatalogBrandRepository, CatalogBrandRepository>();
            services.AddTransient<ICatalogItemRepository, CatalogItemRepository>();
            services.AddTransient<ICatalogTypeRepository, CatalogTypeRepository>();
            services.AddMassTransitConfiguration(configuration);
            return services;
        }
    }
}