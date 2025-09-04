using System.Threading;
using System.Threading.Tasks;
using eShop.Basket.Application.CustomerBaskets;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Contracts.ServiceContract", Version = "1.0")]

namespace eShop.Basket.Application.Interfaces
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketById(string id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Will create the Basket if it doesn't already exist.
        /// </summary>
        Task UpdateBasket(BasketDto dto, CancellationToken cancellationToken = default);
        Task DeleteBasket(string id, CancellationToken cancellationToken = default);
    }
}