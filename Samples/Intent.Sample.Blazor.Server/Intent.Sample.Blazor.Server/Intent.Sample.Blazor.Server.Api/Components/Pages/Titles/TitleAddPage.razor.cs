using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Titles.CreateTitle;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Titles
{
    public partial class TitleAddPage
    {
        public CreateTitleModel Model { get; set; } = new();
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
                await CreateTitle();
                Snackbar.Add("Title created successfully.", Severity.Success);
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

        private async Task CreateTitle()
        {
            try
            {
                await Mediator.Send(new CreateTitleCommand(
                    name: Model.Name));
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

        public class CreateTitleModel
        {
            public string Name { get; set; }
        }
    }
}
