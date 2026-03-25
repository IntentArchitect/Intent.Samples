using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Brands.CreateBrand
{
    public class CreateBrandCommand : IRequest<Guid>, ICommand
    {
        public CreateBrandCommand(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}