using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace Intent.Sample.Blazor.Server.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            Name = null!;
            Description = null!;
            Code = null!;
            Brand = null!;
        }

        public Guid Id { get; set; }

        public Guid BrandId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public virtual Brand Brand { get; set; }
    }
}