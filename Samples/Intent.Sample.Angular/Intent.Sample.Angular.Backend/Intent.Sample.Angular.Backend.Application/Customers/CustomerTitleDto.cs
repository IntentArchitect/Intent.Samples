using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Customers
{
    public class CustomerTitleDto
    {
        public CustomerTitleDto()
        {
            Name = null!;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public static CustomerTitleDto Create(Guid id, string name)
        {
            return new CustomerTitleDto
            {
                Id = id,
                Name = name
            };
        }
    }
}