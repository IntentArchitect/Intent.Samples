using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eShop.Basket.Application.Common.Interfaces;
using eShop.Basket.Application.CustomerBaskets;
using eShop.Basket.Application.Interfaces;
using eShop.Basket.Domain.Common;
using eShop.Basket.Domain.Common.Exceptions;
using eShop.Basket.Domain.Common.Interfaces;
using eShop.Basket.Domain.Entities;
using eShop.Basket.Domain.Repositories;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.ServiceImplementations.ServiceImplementation", Version = "1.0")]

namespace eShop.Basket.Application.Implementation
{
    [IntentManaged(Mode.Merge)]
    public class BasketService : IBasketService
    {
        private readonly ICustomerBasketRepository _customerBasketRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMongoDbUnitOfWork _mongoDbUnitOfWork;

        public BasketService(ICustomerBasketRepository customerBasketRepository, IMapper mapper, ICurrentUserService currentUserService, IMongoDbUnitOfWork mongoDbUnitOfWork)
        {
            _customerBasketRepository = customerBasketRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _mongoDbUnitOfWork = mongoDbUnitOfWork;
        }

        [IntentManaged(Mode.Fully, Body = Mode.Ignore)]
        public async Task<BasketDto> GetBasketById(string id, CancellationToken cancellationToken = default)
        {
            var customerBasket = await _customerBasketRepository.FindByIdAsync(id, cancellationToken);
            if (customerBasket is null)
            {
                customerBasket = new CustomerBasket(id);
                _customerBasketRepository.Add(customerBasket);
                await _mongoDbUnitOfWork.SaveChangesAsync();
            }

            return customerBasket.MapToBasketDto(_mapper);
        }

        /// <summary>
        /// Will create the Basket if it doesn't already exist.
        /// </summary>
        [IntentManaged(Mode.Fully, Body = Mode.Merge)]
        public async Task UpdateBasket(BasketDto dto, CancellationToken cancellationToken = default)
        {
            var customerBasket = await _customerBasketRepository.FindByIdAsync(dto.BuyerId, cancellationToken);
            // [IntentIgnore]
            if (customerBasket is null)
            {
                customerBasket = new Domain.Entities.CustomerBasket(dto.BuyerId);
                customerBasket.BasketItems = dto.BasketItems.Select(i => new BasketItem
                {
                    Id = i.Id,
                    OldUnitPrice = i.OldUnitPrice,
                    PictureUrl = i.PictureUrl,
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList();
                _customerBasketRepository.Add(customerBasket);
                return;
            }

            customerBasket.BasketItems = UpdateHelper.CreateOrUpdateCollection(customerBasket.BasketItems, dto.BasketItems, (e, d) => e.Id == d.Id, CreateOrUpdateBasketItem);

            _customerBasketRepository.Update(customerBasket);

        }

        [IntentManaged(Mode.Fully, Body = Mode.Fully)]
        public async Task DeleteBasket(string id, CancellationToken cancellationToken = default)
        {
            var customerBasket = await _customerBasketRepository.FindByIdAsync(id, cancellationToken);
            if (customerBasket is null)
            {
                throw new NotFoundException($"Could not find CustomerBasket '{id}'");
            }

            _customerBasketRepository.Remove(customerBasket);
        }

        [IntentManaged(Mode.Fully)]
        private static BasketItem CreateOrUpdateBasketItem(BasketItem? entity, BasketItemDto dto)
        {
            entity ??= new BasketItem();
            entity.Id = dto.Id;
            entity.ProductId = dto.ProductId;
            entity.ProductName = dto.ProductName;
            entity.UnitPrice = dto.UnitPrice;
            entity.OldUnitPrice = dto.OldUnitPrice;
            entity.Quantity = dto.Quantity;
            entity.PictureUrl = dto.PictureUrl;
            return entity;
        }
    }
}