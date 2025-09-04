using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eShop.Basket.Eventing.Messages;
using eShop.Ordering.Application.Common.Eventing;
using eShop.Ordering.Application.Orders;
using eShop.Ordering.Application.Orders.CreateOrder;
using eShop.Ordering.Domain.BuyerAggregate;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.MassTransit.IntegrationEventHandler", Version = "1.0")]

namespace eShop.Ordering.Application.IntegrationEvents.EventHandlers
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class UserCheckoutAcceptedIntegrationEventHandler : IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>
    {
        private readonly ISender _mediator;
        [IntentManaged(Mode.Merge)]
        public UserCheckoutAcceptedIntegrationEventHandler(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task HandleAsync(
            UserCheckoutAcceptedIntegrationEvent message,
            CancellationToken cancellationToken = default)
        {
            var command = new CreateOrderCommand(
                userId: message.UserId,
                userName: message.UserName,
                street: message.Street,
                city: message.City,
                state: message.State,
                country: message.Country,
                zipCode: message.ZipCode,
                orderItems: message.Basket.Items
                    .Select(oi => new OrderItemDto
                    {
                        ProductId = oi.ProductId,
                        ProductName = oi.ProductName,
                        UnitPrice = oi.UnitPrice,
                        Discount = 0,
                        PictureUrl = oi.PictureUrl,
                        Units = oi.Quantity
                    })
                    .ToList(),
                buyerId: message.Basket.BuyerId);

            await _mediator.Send(command, cancellationToken);
        }
    }
}