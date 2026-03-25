using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.Dtos.DtoModel", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Products
{
    public record ProductDto
    {
        public ProductDto()
        {
            Name = null!;
            Description = null!;
            Code = null!;
            BrandName = null!;
        }

        public Guid Id { get; init; }
        public Guid BrandId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Code { get; init; }
        public string BrandName { get; init; }
    }
}