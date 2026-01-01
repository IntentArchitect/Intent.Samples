using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Products.UpdateProduct
{
    public class UpdateProductCommand : IRequest, ICommand
    {
        public UpdateProductCommand(Guid id, string name, string description, string code, Guid brandId)
        {
            Id = id;
            Name = name;
            Description = description;
            Code = code;
            BrandId = brandId;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public Guid BrandId { get; set; }
    }
}