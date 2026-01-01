using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Entities.DomainEnum", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Domain
{
    public enum AddressType
    {
        Delivery = 1,
        Billing = 2
    }
}