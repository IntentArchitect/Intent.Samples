using System.Reflection;
using AutoMapper;
using eShop.Basket.Application.Common.Behaviours;
using eShop.Basket.Application.Common.Eventing;
using eShop.Basket.Application.Common.Validation;
using eShop.Basket.Application.Implementation;
using eShop.Basket.Application.IntegrationEvents.EventHandlers;
using eShop.Basket.Application.Interfaces;
using eShop.Basket.Eventing.Messages;
using eShop.Ordering.Eventing.Messages;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.DependencyInjection.DependencyInjection", Version = "1.0")]

namespace eShop.Basket.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), lifetime: ServiceLifetime.Transient);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidatorProvider, ValidatorProvider>();
            services.AddTransient<IValidationService, ValidationService>();
            services.AddTransient<IBasketService, BasketService>();
            services.AddTransient<IIntegrationEventHandler<OrderStartedIntegrationEvent>, OrderStartedIntegrationEventHandler>();
            services.AddTransient<IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>, UserCheckoutAcceptedIntegrationEventHandler>();
            return services;
        }
    }
}