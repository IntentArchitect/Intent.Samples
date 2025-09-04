using System;
using System.Collections.Generic;
using eShop.Ordering.Application.Common.Interfaces;
using eShop.Ordering.Application.Common.Security;
using Intent.RoslynWeaver.Attributes;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace eShop.Ordering.Application.Orders.CancelOrder
{
    [Authorize]
    public class CancelOrderCommand : IRequest, ICommand
    {
        public CancelOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
        public int OrderId { get; set; }

    }
}