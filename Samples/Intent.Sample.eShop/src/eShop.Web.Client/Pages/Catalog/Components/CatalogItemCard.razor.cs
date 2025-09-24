using System.Collections.Generic;
using System.Threading.Tasks;
using eShop.Web.Client.Contracts.Catalog.Services.CatalogItems;
using eShop.Web.Client.Services;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Components;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace eShop.Web.Client.Pages.Catalog.Components
{
    public partial class CatalogItemCard
    {
        [Parameter]
        public List<CatalogItemDto> Model { get; set; }

        [IntentIgnore]
        [Inject]
        public ProductImageUrlProvider ProductImages { get; set; }

        [IntentIgnore]
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }

        [IntentIgnoreBody]
        private void NavigateToDetails(int id)
        {
            NavigationManager.NavigateTo($"/item/{id}");
        }
    }
}