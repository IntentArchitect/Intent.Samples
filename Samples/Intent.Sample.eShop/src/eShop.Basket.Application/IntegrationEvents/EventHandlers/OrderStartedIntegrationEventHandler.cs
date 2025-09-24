using System;
using System.Threading;
using System.Threading.Tasks;
using eShop.Basket.Application.Common.Eventing;
using eShop.Basket.Application.Interfaces;
using eShop.Ordering.Eventing.Messages;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.MassTransit.IntegrationEventHandler", Version = "1.0")]

namespace eShop.Basket.Application.IntegrationEvents.EventHandlers
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        private readonly IBasketService _basketService;

        [IntentManaged(Mode.Merge)]
        public OrderStartedIntegrationEventHandler(IBasketService basketService, IBasketService _basketService)
        {
            _basketService = basketService;
            this._basketService = _basketService ?? throw new ArgumentNullException(nameof(_basketService));
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task HandleAsync(OrderStartedIntegrationEvent message, CancellationToken cancellationToken = default)
        {
            await _basketService.DeleteBasket(message.UserId, cancellationToken);
        }
    }
}