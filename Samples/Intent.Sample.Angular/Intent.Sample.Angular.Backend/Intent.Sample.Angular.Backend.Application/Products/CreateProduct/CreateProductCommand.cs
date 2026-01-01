using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Products.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>, ICommand
    {
        public CreateProductCommand(string name, string description, string code, Guid brandId)
        {
            Name = name;
            Description = description;
            Code = code;
            BrandId = brandId;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public Guid BrandId { get; set; }
    }
}