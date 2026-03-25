using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Brands.CreateBrand;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Brand
{
    public partial class BrandAddDialog
    {
        public CreateBrandModel Model { get; set; } = new();
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;
        [CascadingParameter]
        public IMudDialogInstance Dialog { get; set; }

        private MudForm? _form;
        private bool _saving;

        protected override async Task OnInitializedAsync()
        {
        }

        private async Task CreateBrand()
        {
            try
            {
                await Mediator.Send(new CreateBrandCommand(
                    name: Model.Name));
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private void Cancel()
        {
            Dialog.Close(DialogResult.Cancel());
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
                await CreateBrand();
                Snackbar.Add("Brand created successfully.", Severity.Success);
                Dialog.Close(DialogResult.Ok(true));
            }
            finally
            {
                _saving = false;
            }
        }

        public class CreateBrandModel
        {
            public string Name { get; set; }
        }
    }
}