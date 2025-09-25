using System;
using System.Reflection;
using eShop.Basket.Eventing.Messages;
using eShop.Catalog.Eventing.Messages;
using eShop.Ordering.Application.Common.Eventing;
using eShop.Ordering.Eventing.Messages;
using eShop.Ordering.Infrastructure.Eventing;
using Intent.RoslynWeaver.Attributes;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.MassTransit.MassTransitConfiguration", Version = "1.0")]

namespace eShop.Ordering.Infrastructure.Configuration
{
    public static class MassTransitConfiguration
    {
        public static void AddMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<MassTransitEventBus>();
            services.AddScoped<IEventBus>(provider => provider.GetRequiredService<MassTransitEventBus>());

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumers();

                x.UsingRabbitMq((context, cfg) =>
                {
                    // IntentIgnore
                    var rabbitConn = configuration.GetConnectionString("rabbit");
                    // IntentIgnore
                    cfg.Host(new Uri(rabbitConn), h => { });

                    cfg.UseMessageRetry(r => r.Interval(
                        configuration.GetValue<int?>("MassTransit:RetryInterval:RetryCount") ?? 10,
                        configuration.GetValue<TimeSpan?>("MassTransit:RetryInterval:Interval") ?? TimeSpan.FromSeconds(5)));

                    cfg.ConfigureEndpoints(context);
                    cfg.UseInMemoryOutbox(context);
                });
                x.AddInMemoryInboxOutbox();
            });
        }

        private static void AddConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<IntegrationEventConsumer<IIntegrationEventHandler<GracePeriodConfirmedIntegrationEvent>, GracePeriodConfirmedIntegrationEvent>>(typeof(IntegrationEventConsumerDefinition<IIntegrationEventHandler<GracePeriodConfirmedIntegrationEvent>, GracePeriodConfirmedIntegrationEvent>)).Endpoint(config => config.InstanceId = "eShop-Ordering");
            cfg.AddConsumer<IntegrationEventConsumer<IIntegrationEventHandler<OrderStockConfirmedIntegrationEvent>, OrderStockConfirmedIntegrationEvent>>(typeof(IntegrationEventConsumerDefinition<IIntegrationEventHandler<OrderStockConfirmedIntegrationEvent>, OrderStockConfirmedIntegrationEvent>)).Endpoint(config => config.InstanceId = "eShop-Ordering");
            cfg.AddConsumer<IntegrationEventConsumer<IIntegrationEventHandler<OrderStockRejectedIntegrationEvent>, OrderStockRejectedIntegrationEvent>>(typeof(IntegrationEventConsumerDefinition<IIntegrationEventHandler<OrderStockRejectedIntegrationEvent>, OrderStockRejectedIntegrationEvent>)).Endpoint(config => config.InstanceId = "eShop-Ordering");
            cfg.AddConsumer<IntegrationEventConsumer<IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>, UserCheckoutAcceptedIntegrationEvent>>(typeof(IntegrationEventConsumerDefinition<IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>, UserCheckoutAcceptedIntegrationEvent>)).Endpoint(config => config.InstanceId = "eShop-Ordering");

        }
    }
}