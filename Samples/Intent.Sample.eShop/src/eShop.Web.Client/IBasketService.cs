using System;
using System.Threading;
using System.Threading.Tasks;
using eShop.Web.Client.Contracts.Basket.Services.CustomerBaskets;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.ServiceContract", Version = "2.0")]

namespace eShop.Web.Client
{
    public interface IBasketService : IDisposable
    {
        Task<BasketDto> GetBasketByIdAsync(string id, CancellationToken cancellationToken = default);
        Task UpdateBasketAsync(BasketDto dto, CancellationToken cancellationToken = default);
    }
}