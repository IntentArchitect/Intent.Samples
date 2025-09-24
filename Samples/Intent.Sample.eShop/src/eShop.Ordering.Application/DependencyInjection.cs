using System.Reflection;
using AutoMapper;
using eShop.Basket.Eventing.Messages;
using eShop.Catalog.Eventing.Messages;
using eShop.Ordering.Application.Common.Behaviours;
using eShop.Ordering.Application.Common.Eventing;
using eShop.Ordering.Application.Common.Validation;
using eShop.Ordering.Application.IntegrationEvents.EventHandlers;
using eShop.Ordering.Eventing.Messages;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.DependencyInjection.DependencyInjection", Version = "1.0")]

namespace eShop.Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), lifetime: ServiceLifetime.Transient);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
                cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
                cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(EventBusPublishBehaviour<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidatorProvider, ValidatorProvider>();
            services.AddTransient<IValidationService, ValidationService>();
            services.AddTransient<IIntegrationEventHandler<GracePeriodConfirmedIntegrationEvent>, GracePeriodConfirmedIntegrationEventHandler>();
            services.AddTransient<IIntegrationEventHandler<OrderStockConfirmedIntegrationEvent>, OrderStockConfirmedIntegrationEventHandler>();
            services.AddTransient<IIntegrationEventHandler<OrderStockRejectedIntegrationEvent>, OrderStockRejectedIntegrationEventHandler>();
            services.AddTransient<IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>, UserCheckoutAcceptedIntegrationEventHandler>();
            return services;
        }
    }
}