using System;
using System.Collections.Generic;
using eShop.Ordering.Application.Common.Interfaces;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace eShop.Ordering.Application.Orders.SetStockConfirmedOrderStatus
{
    public class SetStockConfirmedOrderStatusCommand : IRequest, ICommand
    {
        public SetStockConfirmedOrderStatusCommand(int orderId)
        {
            OrderId = orderId;
        }
        public int OrderId { get; set; }

    }
}