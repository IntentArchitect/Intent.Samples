using System;
using eShop.Basket.Application.Common.Eventing;
using eShop.Basket.Domain.Common.Interfaces;
using eShop.Basket.Domain.Repositories;
using eShop.Basket.Infrastructure.Configuration;
using eShop.Basket.Infrastructure.Eventing;
using eShop.Basket.Infrastructure.Persistence;
using eShop.Basket.Infrastructure.Repositories;
using Intent.RoslynWeaver.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Infrastructure.DependencyInjection.DependencyInjection", Version = "1.0")]

namespace eShop.Basket.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //var cs = configuration.GetConnectionString("MongoDbConnection");
            // IntentIgnore
            var cs = configuration.GetConnectionString("eshop");
            services.TryAddSingleton<IMongoClient>(_ => new MongoClient(cs));
            services.TryAddSingleton<IMongoDatabase>(sp =>
                    {
                        var dbName = new MongoUrl(cs).DatabaseName
                                     ?? throw new InvalidOperationException(
                                         "MongoDbConnection must include a database name.");
                        return sp.GetRequiredService<IMongoClient>().GetDatabase(dbName);
                    });
            services.RegisterMongoCollections(typeof(DependencyInjection).Assembly);
            services.AddScoped<ICustomerBasketRepository, CustomerBasketMongoRepository>();
            services.AddScoped<MongoDbUnitOfWork>();
            services.AddScoped<IMongoDbUnitOfWork>(provider => provider.GetRequiredService<MongoDbUnitOfWork>());
            services.AddMassTransitConfiguration(configuration);
            return services;
        }
    }
}