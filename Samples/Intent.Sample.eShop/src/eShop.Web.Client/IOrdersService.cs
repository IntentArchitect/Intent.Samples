using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eShop.Web.Client.Contracts.Ordering.Services.Orders;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.ServiceContract", Version = "2.0")]

namespace eShop.Web.Client
{
    public interface IOrdersService : IDisposable
    {
        Task<int> CreateOrderAsync(CreateOrderCommand command, CancellationToken cancellationToken = default);
        Task<List<OrderSummaryDto>> GetOrdersAsync(CancellationToken cancellationToken = default);
    }
}