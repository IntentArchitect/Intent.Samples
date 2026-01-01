using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Brands.DeleteBrand
{
    public class DeleteBrandCommand : IRequest, ICommand
    {
        public DeleteBrandCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}