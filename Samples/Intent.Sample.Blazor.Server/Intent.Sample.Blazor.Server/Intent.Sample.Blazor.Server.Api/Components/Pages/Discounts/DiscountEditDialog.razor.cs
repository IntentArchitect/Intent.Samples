using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Discounts.GetDiscountById;
using Intent.Sample.Blazor.Server.Application.Discounts.UpdateDiscount;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Discounts
{
    public partial class DiscountEditDialog
    {
        [Parameter]
        public Guid DiscountId { get; set; }
        public UpdateDiscountModel? Model { get; set; }
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;
        [CascadingParameter]
        public IMudDialogInstance Dialog { get; set; }

        private MudForm? _form;
        private bool _isSaving;

        protected override async Task OnInitializedAsync()
        {
            await LoadDiscountById(DiscountId);
        }

        private async Task SaveAsync()
        {
            if (_form == null)
            {
                return;
            }

            await _form.Validate();
            if (!_form.IsValid)
            {
                return;
            }

            _isSaving = true;
            await UpdateDiscount();
            _isSaving = false;

            Dialog.Close(DialogResult.Ok(true));
        }

        private void Cancel()
        {
            Dialog.Cancel();
        }

        private async Task UpdateDiscount()
        {
            try
            {
                await Mediator.Send(new UpdateDiscountCommand(
                    id: Model.Id.Value,
                    code: Model.Code,
                    discountAmount: Model.DiscountAmount.Value,
                    expiry: Model.Expiry.Value));
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private async Task LoadDiscountById(Guid id)
        {
            try
            {
                var discountDto = await Mediator.Send(new GetDiscountByIdQuery(
                    id: id));
                Model = new UpdateDiscountModel
                {
                    Id = discountDto.Id,
                    Code = discountDto.Code,
                    DiscountAmount = discountDto.DiscountAmount,
                    Expiry = discountDto.Expiry
                };
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        public class UpdateDiscountModel
        {
            public Guid? Id { get; set; }
            public string Code { get; set; }
            public decimal? DiscountAmount { get; set; }
            public DateTime? Expiry { get; set; }
        }
    }
}
