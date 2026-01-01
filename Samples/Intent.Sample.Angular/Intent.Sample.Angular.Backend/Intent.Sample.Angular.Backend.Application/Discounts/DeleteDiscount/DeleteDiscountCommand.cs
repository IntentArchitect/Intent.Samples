using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Application.Common.Interfaces;
using MediatR;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Application.MediatR.CommandModels", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Application.Discounts.DeleteDiscount
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