using FluentValidation;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.Blazor.HttpClients.Dtos.FluentValidation.DtoValidator", Version = "2.0")]

namespace MudBlazor.Sample.Client.HttpClients.Contracts.Services.Invoices
{
    [IntentManaged(Mode.Fully, Body = Mode.Merge)]
    public class UpdateInvoiceCommandOrderLinesDtoValidator : AbstractValidator<UpdateInvoiceCommandOrderLinesDto>
    {
        [IntentManaged(Mode.Merge)]
        public UpdateInvoiceCommandOrderLinesDtoValidator()
        {
            ConfigureValidationRules();
        }

        private void ConfigureValidationRules()
        {
        }
    }
}