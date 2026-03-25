using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Titles;
using Intent.Sample.Blazor.Server.Application.Titles.GetTitles;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Titles
{
    public partial class TitleListPage
    {
        public List<TitleDto>? TitlesModels { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadTitles();
        }

        private async Task LoadTitles()
        {
            try
            {
                TitlesModels = await Mediator.Send(new GetTitlesQuery());
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private void NavigateToTitleAddPage()
        {
            NavigationManager.NavigateTo("titles/add");
        }

        private void NavigateToTitleEditPage(Guid titleId)
        {
            NavigationManager.NavigateTo($"titles/edit/{titleId}");
        }
    }
}
