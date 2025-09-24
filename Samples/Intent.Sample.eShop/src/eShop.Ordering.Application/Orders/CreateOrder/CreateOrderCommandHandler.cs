using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using eShop.Ordering.Application.Common.Eventing;
using eShop.Ordering.Domain.BuyerAggregate;
using eShop.Ordering.Domain.OrderAggregate;
using eShop.Ordering.Domain.Repositories.OrderAggregate;
using eShop.Ordering.Eventing.Messages;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace eShop.Ordering.Application.Orders.CreateOrder
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventBus _eventBus;

        [IntentManaged(Mode.Merge)]
        public CreateOrderCommandHandler(IOrderRepository orderRepository, IEventBus eventBus)
        {
            _orderRepository = orderRepository;
            _eventBus = eventBus;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(
                userId: request.UserId,
                userName: request.UserName,
                buyerId: request.BuyerId,
                address: new Address(
                    street: request.Street,
                    city: request.City,
                    state: request.State,
                    country: request.Country,
                    zipCode: request.ZipCode));

            // [IntentIgnore]
            foreach (var orderItem in request.OrderItems)
            {
                order.AddOrderItem(
                    orderItem.ProductId,
                    orderItem.ProductName,
                    orderItem.UnitPrice,
                    orderItem.Discount,
                    orderItem.PictureUrl,
                    orderItem.Units);
            }

            _orderRepository.Add(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            _eventBus.Publish(new OrderStartedIntegrationEvent
            {
                UserId = request.UserId
            });
            return order.Id;

        }
    }
}