using eShop.Ordering.Application.Common.Eventing;
using eShop.Ordering.Application.Common.Interfaces;
using eShop.Ordering.Domain.Common.Interfaces;
using eShop.Ordering.Domain.Repositories;
using eShop.Ordering.Domain.Repositories.BuyerAggregate;
using eShop.Ordering.Domain.Repositories.OrderAggregate;
using eShop.Ordering.Infrastructure.Configuration;
using eShop.Ordering.Infrastructure.Eventing;
using eShop.Ordering.Infrastructure.Persistence;
using eShop.Ordering.Infrastructure.Repositories;
using eShop.Ordering.Infrastructure.Repositories.BuyerAggregate;
using eShop.Ordering.Infrastructure.Repositories.OrderAggregate;
using eShop.Ordering.Infrastructure.Services;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Infrastructure.DependencyInjection.DependencyInjection", Version = "1.0")]

namespace eShop.Ordering.Infrastructure
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
            services.AddTransient<IBuyerRepository, BuyerRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddMassTransitConfiguration(configuration);
            return services;
        }
    }
}