using System.Reflection;
using AutoMapper;
using eShop.Catalog.Application.Common.Behaviours;
using eShop.Catalog.Application.Common.Eventing;
using eShop.Catalog.Application.Common.Validation;
using eShop.Catalog.Application.Implementation;
using eShop.Catalog.Application.IntegrationEvents.EventHandlers;
using eShop.Catalog.Application.Interfaces;
using eShop.Ordering.Eventing.Messages;
using FluentValidation;
using Intent.RoslynWeaver.Attributes;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.DependencyInjection.DependencyInjection", Version = "1.0")]

namespace eShop.Catalog.Application
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
            services.AddTransient<ICatalogService, CatalogService>();
            services.AddTransient<IIntegrationEventHandler<OrderStatusChangedToAwaitingValidationIntegrationEvent>, OrderStatusChangedToAwaitingValidationIntegrationEventHandler>();
            return services;
        }
    }
}