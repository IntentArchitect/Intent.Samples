using eShop.Catalog.Eventing.Messages;
using eShop.Ordering.Application.Common.Eventing;
using eShop.Ordering.Eventing.Messages;
using eShop.Ordering.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eShop.Ordering.Api.Services
{
    public class OrderUpdateMockService(
        IServiceProvider serviceProvider) : BackgroundService
    {

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var delayTime = TimeSpan.FromSeconds(5);

            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckConfirmedGracePeriodOrders();

                await Task.Delay(delayTime, stoppingToken);
            }
        }

        private async Task CheckConfirmedGracePeriodOrders()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                IEventBus eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
                ApplicationDbContext dataSource = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var orders = await dataSource.Orders.ToListAsync();

                foreach (var order in orders)
                {
                    switch (order.OrderStatus)
                    {
                        case Domain.OrderAggregate.OrderStatus.Submitted:
                            var stockConfirmedEvent = new OrderStockConfirmedIntegrationEvent() { OrderId = order.Id };
                            eventBus.Publish(stockConfirmedEvent);
                            break;
                        case Domain.OrderAggregate.OrderStatus.StockConfirmed:
                            var stockShippedEvent = new OrderStatusChangedToShippedIntegrationEvent() { OrderId = order.Id };
                            eventBus.Publish(stockShippedEvent);
                            break;
                        default:
                            {
                                var stockStatusEvent = new OrderStockConfirmedIntegrationEvent() { OrderId = order.Id };
                                eventBus.Publish(stockStatusEvent);
                                break;
                            }
                    }
                }
            }
        }
    }
}
