using System;
using System.Threading.Tasks;
using eShop.Basket.Application.Common.Eventing;
using eShop.Basket.Domain.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.MassTransit.IntegrationEventConsumer", Version = "1.0")]

namespace eShop.Basket.Infrastructure.Eventing
{
    public class IntegrationEventConsumer<THandler, TMessage> : IConsumer<TMessage>
        where TMessage : class
        where THandler : IIntegrationEventHandler<TMessage>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMongoDbUnitOfWork _mongoDbUnitOfWork;

        public IntegrationEventConsumer(IServiceProvider serviceProvider, IMongoDbUnitOfWork mongoDbUnitOfWork)
        {
            _serviceProvider = serviceProvider;
            _mongoDbUnitOfWork = mongoDbUnitOfWork ?? throw new ArgumentNullException(nameof(mongoDbUnitOfWork));
        }

        public async Task Consume(ConsumeContext<TMessage> context)
        {
            var messageBus = _serviceProvider.GetRequiredService<MassTransitMessageBus>();
            messageBus.ConsumeContext = context;
            var handler = _serviceProvider.GetRequiredService<THandler>();
            await handler.HandleAsync(context.Message, context.CancellationToken);
            await _mongoDbUnitOfWork.SaveChangesAsync(context.CancellationToken);
            await messageBus.FlushAllAsync(context.CancellationToken);
        }
    }

    public class IntegrationEventConsumerDefinition<THandler, TMessage> : ConsumerDefinition<IntegrationEventConsumer<THandler, TMessage>>
        where TMessage : class
        where THandler : IIntegrationEventHandler<TMessage>
    {
        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<IntegrationEventConsumer<THandler, TMessage>> consumerConfigurator,
            IRegistrationContext context)
        {
        }
    }
}