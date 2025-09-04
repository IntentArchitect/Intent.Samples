using eShop.Basket.Application.Common.Eventing;
using eShop.Basket.Domain.Common.Interfaces;
using eShop.Basket.Domain.Repositories;
using eShop.Basket.Infrastructure.Configuration;
using eShop.Basket.Infrastructure.Eventing;
using eShop.Basket.Infrastructure.Persistence;
using eShop.Basket.Infrastructure.Persistence.Documents;
using eShop.Basket.Infrastructure.Repositories;
using Intent.RoslynWeaver.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Infrastructure.DependencyInjection.DependencyInjection", Version = "1.0")]

namespace eShop.Basket.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(sp =>
                    {
                        var connectionString = configuration.GetConnectionString("MongoDbConnection");
                        return new MongoClient(connectionString);
                    });
            services.AddSingleton(sp =>
                    {
                        var connectionString = configuration.GetConnectionString("MongoDbConnection");

                        // Parse connection string to get the database name
                        var mongoUrl = new MongoUrl(connectionString);
                        var client = sp.GetRequiredService<IMongoClient>();

                        return client.GetDatabase(mongoUrl.DatabaseName);
                    });
            services.AddSingleton<IMongoCollection<BasketItemDocument>>(sp =>
                            {
                                var database = sp.GetRequiredService<IMongoDatabase>();
                                return database.GetCollection<BasketItemDocument>("BasketItem");
                            });
            services.AddSingleton<IMongoCollection<CustomerBasketDocument>>(sp =>
                            {
                                var database = sp.GetRequiredService<IMongoDatabase>();
                                return database.GetCollection<CustomerBasketDocument>("CustomerBasket");
                            });
            services.AddScoped<ICustomerBasketRepository, CustomerBasketMongoRepository>();
            services.AddScoped<MongoDbUnitOfWork>();
            services.AddScoped<IMongoDbUnitOfWork>(provider => provider.GetRequiredService<MongoDbUnitOfWork>());
            services.AddMassTransitConfiguration(configuration);
            return services;
        }
    }
}