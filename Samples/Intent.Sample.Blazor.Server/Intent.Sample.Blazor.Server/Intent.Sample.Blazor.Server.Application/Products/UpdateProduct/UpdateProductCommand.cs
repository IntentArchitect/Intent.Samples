using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Products.UpdateProduct
{
    public class UpdateProductCommand : IRequest, ICommand
    {
        public UpdateProductCommand(Guid id, Guid brandId, string name, string description, string code)
        {
            Id = id;
            BrandId = brandId;
            Name = name;
            Description = description;
            Code = code;
        }

        public Guid Id { get; set; }
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}