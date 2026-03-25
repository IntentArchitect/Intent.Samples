using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace Intent.Sample.Blazor.Server.Domain.Entities
{
    public class Customer
    {
        public Customer()
        {
            Name = null!;
            Surname = null!;
            Email = null!;
            Title = null!;
        }

        public Guid Id { get; set; }

        public Guid TitleId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Address> Addresses { get; set; } = [];

        public virtual Loyalty? Loyalty { get; set; }

        public virtual Title Title { get; set; }
    }
}