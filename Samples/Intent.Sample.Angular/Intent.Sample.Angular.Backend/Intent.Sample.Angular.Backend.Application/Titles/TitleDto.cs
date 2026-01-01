using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Titles
{
    public class TitleDto
    {
        public TitleDto()
        {
            Name = null!;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public static TitleDto Create(Guid id, string name)
        {
            return new TitleDto
            {
                Id = id,
                Name = name
            };
        }
    }
}