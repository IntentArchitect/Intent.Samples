using System.ComponentModel.DataAnnotations;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Brands.GetBrandById;
using Intent.Sample.Blazor.Server.Application.Brands.UpdateBrand;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Brand
{
    public partial class BrandEditDialog
    {
        [Parameter]
        public Guid BrandId { get; set; }

        [CascadingParameter]
        public IMudDialogInstance Dialog { get; set; }

        public UpdateBrandModel? Model { get; set; }
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        private MudForm? _form;
        private bool _isFormValid;
        private bool _isSaving;
        private bool _isLoading;

        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;
            await LoadBrandById(BrandId);
            _isLoading = false;
        }

        private async Task UpdateBrand()
        {
            try
            {
                await Mediator.Send(new UpdateBrandCommand(
                    id: Model.Id.Value,
                    name: Model.Name));
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private async Task LoadBrandById(Guid id)
        {
            try
            {
                var brandDto = await Mediator.Send(new GetBrandByIdQuery(
                    id: id));
                Model = new UpdateBrandModel
                {
                    Id = brandDto.Id,
                    Name = brandDto.Name
                };
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        public async Task SaveAsync()
        {
            if (_form == null || Model == null)
            {
                return;
            }

            await _form.Validate();

            _isFormValid = _form.IsValid;

            if (!_isFormValid)
            {
                return;
            }

            _isSaving = true;
            try
            {
                await UpdateBrand();
                Dialog.Close(DialogResult.Ok(true));
            }
            finally
            {
                _isSaving = false;
            }
        }

        public void Cancel()
        {
            Dialog.Cancel();
        }

        public class UpdateBrandModel
        {
            public Guid? Id { get; set; }

            [Required]
            public string Name { get; set; } = string.Empty;
        }
    }
}