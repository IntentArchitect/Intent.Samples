using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Domain.Common;
using Intent.Sample.Blazor.Server.Domain.Common.Exceptions;
using Intent.Sample.Blazor.Server.Domain.Entities;
using Intent.Sample.Blazor.Server.Domain.Repositories;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandHandler", Version = "2.0")]

namespace Intent.Sample.Blazor.Server.Application.Orders.UpdateOrder
{
    [IntentManaged(Mode.Merge, Signature = Mode.Fully)]
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        [IntentManaged(Mode.Merge)]
        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindByIdAsync(request.Id, cancellationToken);
            if (order is null)
            {
                throw new NotFoundException($"Could not find Order '{request.Id}'");
            }

            order.CustomerId = request.CustomerId;
            order.OrderNo = request.OrderNo;
            order.DiscountCode = request.DiscountCode;
            order.Total = request.Total;
            order.OrderedDate = request.OrderedDate;
            order.OrderItems = UpdateHelper.CreateOrUpdateCollection(order.OrderItems, request.OrderItems, (e, d) => e.Id == d.Id, CreateOrUpdateOrderItem);
        }

        [IntentManaged(Mode.Fully)]
        private static OrderItem CreateOrUpdateOrderItem(OrderItem? entity, UpdateOrderCommandOrderItemsDto dto)
        {
            entity ??= new OrderItem();
            entity.Id = dto.Id;
            entity.ProductId = dto.ProductId;
            entity.Units = dto.Units;
            entity.Price = dto.Price;
            return entity;
        }
    }
}