using System;
using System.Reflection;
using eShop.Basket.Application.Common.Eventing;
using eShop.Basket.Eventing.Messages;
using eShop.Basket.Infrastructure.Eventing;
using eShop.Ordering.Eventing.Messages;
using Intent.RoslynWeaver.Attributes;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.MassTransit.MassTransitConfiguration", Version = "1.0")]

namespace eShop.Basket.Infrastructure.Configuration
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
                    var rabbitConn = configuration.GetConnectionString("rabbit");

                    cfg.Host(new Uri(rabbitConn), h =>{ });

                    cfg.UseMessageRetry(r => r.Interval(
                        configuration.GetValue<int?>("MassTransit:RetryInterval:RetryCount") ?? 10,
                        configuration.GetValue<TimeSpan?>("MassTransit:RetryInterval:Interval") ?? TimeSpan.FromSeconds(5)));

                    cfg.ConfigureEndpoints(context);
                });
            });
        }

        private static void AddConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<IntegrationEventConsumer<IIntegrationEventHandler<OrderStartedIntegrationEvent>, OrderStartedIntegrationEvent>>(typeof(IntegrationEventConsumerDefinition<IIntegrationEventHandler<OrderStartedIntegrationEvent>, OrderStartedIntegrationEvent>)).Endpoint(config => config.InstanceId = "eShop-Basket");
            cfg.AddConsumer<IntegrationEventConsumer<IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>, UserCheckoutAcceptedIntegrationEvent>>(typeof(IntegrationEventConsumerDefinition<IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>, UserCheckoutAcceptedIntegrationEvent>)).Endpoint(config => config.InstanceId = "eShop-Basket");

        }
    }
}