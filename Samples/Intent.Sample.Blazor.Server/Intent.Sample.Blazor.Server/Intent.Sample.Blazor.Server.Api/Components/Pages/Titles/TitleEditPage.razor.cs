using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Titles.GetTitleById;
using Intent.Sample.Blazor.Server.Application.Titles.UpdateTitle;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Titles
{
    public partial class TitleEditPage
    {
        [Parameter]
        public Guid TitleId { get; set; }
        public UpdateTitleModel? Model { get; set; }
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
            await LoadTitleById(TitleId);
        }

        private async Task UpdateTitle()
        {
            try
            {
                await Mediator.Send(new UpdateTitleCommand(
                    id: Model.Id.Value,
                    name: Model.Name));
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private async Task LoadTitleById(Guid id)
        {
            try
            {
                var titleDto = await Mediator.Send(new GetTitleByIdQuery(
                    id: id));
                Model = new UpdateTitleModel
                {
                    Id = titleDto.Id,
                    Name = titleDto.Name
                };
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private void NavigateToTitleListPage()
        {
            NavigationManager.NavigateTo("titles/list");
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
                await UpdateTitle();
                Snackbar.Add("Title updated successfully.", Severity.Success);
                NavigateToTitleListPage();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Failed to save title: {ex.Message}", Severity.Error);
            }
            finally
            {
                _saving = false;
            }
        }

        private void Cancel()
        {
            NavigateToTitleListPage();
        }

        public class UpdateTitleModel
        {
            public Guid? Id { get; set; }
            public string Name { get; set; }
        }
    }
}
