using System;
using eShop.Web.Client.Implementations;
using eShop.Web.Client.Implementations.Orders;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: DefaultIntentManaged(Mode.Fully, Targets = Targets.Usings)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.HttpClientConfiguration", Version = "2.0")]

namespace eShop.Web.Client
{
    public static class HttpClientConfiguration
    {
        public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHttpClient<IBasketService, BasketServiceHttpClient>(http =>
                {
                    http.BaseAddress = GetUrl(configuration, "EShopBasket");
                })
                .AddHttpMessageHandler(sp =>
                {
                    return sp.GetRequiredService<AuthorizationMessageHandler>()
                        .ConfigureHandler(
                            authorizedUrls: new[] { GetUrl(configuration, "EShopBasket").AbsoluteUri });
                });

            services
                .AddHttpClient<ICatalogService, CatalogServiceHttpClient>(http =>
                {
                    http.BaseAddress = GetUrl(configuration, "EShopCatalog");
                });

            services
                .AddHttpClient<IOrdersService, OrdersServiceHttpClient>(http =>
                {
                    http.BaseAddress = GetUrl(configuration, "EShopOrdering");
                })
                .AddHttpMessageHandler(sp =>
                {
                    return sp.GetRequiredService<AuthorizationMessageHandler>()
                        .ConfigureHandler(
                            authorizedUrls: new[] { GetUrl(configuration, "EShopOrdering").AbsoluteUri });
                });
        }

        private static Uri GetUrl(IConfiguration configuration, string applicationName)
        {
            var url = configuration.GetValue<Uri?>($"Urls:{applicationName}");

            return url ?? throw new Exception($"Configuration key \"Urls:{applicationName}\" is not set");
        }
    }
}