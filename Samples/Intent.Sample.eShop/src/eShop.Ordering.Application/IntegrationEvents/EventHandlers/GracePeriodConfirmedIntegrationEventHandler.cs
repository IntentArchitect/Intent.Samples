using System;
using System.Threading;
using System.Threading.Tasks;
using eShop.Ordering.Application.Common.Eventing;
using eShop.Ordering.Application.Orders.SetAwaitingValidationOrderStatus;
using eShop.Ordering.Eventing.Messages;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Eventing.MassTransit.IntegrationEventHandler", Version = "1.0")]

namespace eShop.Ordering.Application.IntegrationEvents.EventHandlers
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class GracePeriodConfirmedIntegrationEventHandler : IIntegrationEventHandler<GracePeriodConfirmedIntegrationEvent>
    {
        private readonly ISender _mediator;

        [IntentManaged(Mode.Merge)]
        public GracePeriodConfirmedIntegrationEventHandler(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task HandleAsync(
            GracePeriodConfirmedIntegrationEvent message,
            CancellationToken cancellationToken = default)
        {
            var command = new SetAwaitingValidationOrderStatusCommand(orderId: message.OrderId);

            await _mediator.Send(command, cancellationToken);
        }
    }
}