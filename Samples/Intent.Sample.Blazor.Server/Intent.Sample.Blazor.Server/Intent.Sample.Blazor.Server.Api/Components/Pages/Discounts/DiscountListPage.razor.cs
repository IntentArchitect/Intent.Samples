using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Discounts;
using Intent.Sample.Blazor.Server.Application.Discounts.GetDiscounts;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Discounts
{
    public partial class DiscountListPage
    {
        public List<DiscountDto>? DiscountsModels { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public IDialogService DialogService { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadDiscounts();
        }

        private async Task LoadDiscounts()
        {
            try
            {
                DiscountsModels = await Mediator.Send(new GetDiscountsQuery());
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private async Task EditDiscount(Guid discountId)
        {
            try
            {
                var parameters = new DialogParameters<DiscountEditDialog>
                {
                    { x => x.DiscountId, discountId },
                };
                var dialog = await DialogService.ShowAsync<DiscountEditDialog>("Edit Discount", parameters, new DialogOptions() { FullWidth = true, MaxWidth = MaxWidth.Large, BackdropClick = false });
                var result = await dialog.Result;

                if (result.Canceled)
                {
                    return;
                }
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private void NavigateToDiscountAddPage()
        {
            NavigationManager.NavigateTo("discounts/add");
        }

        private async Task OnEditDiscount(Guid discountId)
        {
            await EditDiscount(discountId);
            await LoadDiscounts();
            StateHasChanged();
        }
    }
}
