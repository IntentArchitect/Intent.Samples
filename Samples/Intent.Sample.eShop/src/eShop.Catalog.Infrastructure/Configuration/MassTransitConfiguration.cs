using System;
using System.Reflection;
using eShop.Catalog.Application.Common.Eventing;
using eShop.Catalog.Infrastructure.Eventing;
using eShop.Ordering.Eventing.Messages;
using Intent.RoslynWeaver.Attributes;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.MassTransit.MassTransitConfiguration", Version = "1.0")]

namespace eShop.Catalog.Infrastructure.Configuration
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

                x.UsingInMemory((context, cfg) =>
                {
                    cfg.UseMessageRetry(r => r.Interval(
                        configuration.GetValue<int?>("MassTransit:RetryInterval:RetryCount") ?? 10,
                        configuration.GetValue<TimeSpan?>("MassTransit:RetryInterval:Interval") ?? TimeSpan.FromSeconds(5)));
                    cfg.ConfigureEndpoints(context);
                });
            });
        }

        private static void AddConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<IntegrationEventConsumer<IIntegrationEventHandler<OrderStatusChangedToAwaitingValidationIntegrationEvent>, OrderStatusChangedToAwaitingValidationIntegrationEvent>>(typeof(IntegrationEventConsumerDefinition<IIntegrationEventHandler<OrderStatusChangedToAwaitingValidationIntegrationEvent>, OrderStatusChangedToAwaitingValidationIntegrationEvent>)).Endpoint(config => config.InstanceId = "eShop-Catalog");
        }
    }
}