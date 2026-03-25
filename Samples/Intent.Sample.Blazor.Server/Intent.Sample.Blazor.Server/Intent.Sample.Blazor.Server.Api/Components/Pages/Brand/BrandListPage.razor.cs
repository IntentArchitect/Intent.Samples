using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Brands;
using Intent.Sample.Blazor.Server.Application.Brands.GetBrands;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Brand
{
    public partial class BrandListPage
    {
        public List<BrandDto>? BrandsModels { get; set; }
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public IDialogService DialogService { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadBrands();
        }

        private async Task LoadBrands()
        {
            try
            {
                BrandsModels = await Mediator.Send(new GetBrandsQuery());
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private async Task AddBrand()
        {
            try
            {
                var dialog = await DialogService.ShowAsync<BrandAddDialog>("Add Brand", new DialogOptions() { FullWidth = true, MaxWidth = MaxWidth.Large, BackdropClick = false });
                var result = await dialog.Result;

                if (result.Canceled)
                {
                    return;
                }

                await LoadBrands();
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private async Task EditBrand(Guid brandId)
        {
            try
            {
                var parameters = new DialogParameters<BrandEditDialog>
                {
                    { x => x.BrandId, brandId },
                };
                var dialog = await DialogService.ShowAsync<BrandEditDialog>("Edit Brand", parameters, new DialogOptions() { FullWidth = true, MaxWidth = MaxWidth.Large, BackdropClick = false });
                var result = await dialog.Result;

                if (result.Canceled)
                {
                    return;
                }

                await LoadBrands();
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }
    }
}
