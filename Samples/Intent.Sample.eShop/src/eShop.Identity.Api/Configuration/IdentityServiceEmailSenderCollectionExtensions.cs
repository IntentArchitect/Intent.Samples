using eShop.Identity.Infrastructure.Options;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AspNetCore.IdentityService.IdentityServiceEmailSenderCollectionExtensions", Version = "1.0")]

namespace eShop.Identity.Api.Configuration
{
    public static class IdentityServiceEmailSenderCollectionExtensions
    {
        public static void ConfigureIdentityEmailSender(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSenderOptions>(configuration.GetSection("EmailSender"));
        }
    }
}