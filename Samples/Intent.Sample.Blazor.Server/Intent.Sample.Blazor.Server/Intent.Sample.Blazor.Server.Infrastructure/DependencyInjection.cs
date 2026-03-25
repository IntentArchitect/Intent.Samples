using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Domain.Common.Interfaces;
using Intent.Sample.Blazor.Server.Domain.Repositories;
using Intent.Sample.Blazor.Server.Infrastructure.Persistence;
using Intent.Sample.Blazor.Server.Infrastructure.Repositories;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Infrastructure.DependencyInjection.DependencyInjection", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseSqlite(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                options.UseLazyLoadingProxies();
            });
            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IScopedExecutor, ScopedExecutor>();
            services.AddScoped<IScopedMediator, ScopedMediator>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IDiscountRepository, DiscountRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ITitleRepository, TitleRepository>();
            return services;
        }
    }
}