using System;
using System.Collections.Generic;
using eShop.Ordering.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace eShop.Ordering.Application.Orders.SetStockRejectedOrderStatus
{
    public class SetStockRejectedOrderStatusCommand : IRequest, ICommand
    {
        public SetStockRejectedOrderStatusCommand(int orderId, List<int> orderStockItems)
        {
            OrderId = orderId;
            OrderStockItems = orderStockItems;
        }
        public int OrderId { get; set; }

        public List<int> OrderStockItems { get; set; }

    }
}