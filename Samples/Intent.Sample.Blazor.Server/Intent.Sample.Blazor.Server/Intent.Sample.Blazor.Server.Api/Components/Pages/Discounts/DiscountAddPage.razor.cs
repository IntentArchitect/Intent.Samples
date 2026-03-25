using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Discounts.CreateDiscount;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Discounts
{
    public partial class DiscountAddPage
    {
        public CreateDiscountModel Model { get; set; } = new();
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        private MudForm? _form;
        private bool _saving;

        protected override async Task OnInitializedAsync()
        {
        }

        private async Task SaveAsync()
        {
            if (_form is null)
            {
                return;
            }

            await _form.Validate();
            if (!_form.IsValid)
            {
                Snackbar.Add("Please fix validation errors before saving.", Severity.Warning);
                return;
            }

            _saving = true;
            try
            {
                await CreateDiscount();
                Snackbar.Add("Discount created successfully.", Severity.Success);
                NavigateToDiscountListPage();
            }
            finally
            {
                _saving = false;
            }
        }

        private async Task CreateDiscount()
        {
            try
            {
                await Mediator.Send(new CreateDiscountCommand(
                    code: Model.Code,
                    discountAmount: Model.DiscountAmount.Value,
                    expiry: Model.Expiry.Value));
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private void NavigateToDiscountListPage()
        {
            NavigationManager.NavigateTo("discounts/list");
        }

        public class CreateDiscountModel
        {
            public string Code { get; set; }
            public decimal? DiscountAmount { get; set; }
            public DateTime? Expiry { get; set; }
        }
    }
}
