using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace Intent.Sample.Angular.Backend.Domain.Entities
{
    public class Title
    {
        public Title()
        {
            Name = null!;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}