using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Application.Discounts.DeleteDiscount
{
    public class DeleteDiscountCommand : IRequest, ICommand
    {
        public DeleteDiscountCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}