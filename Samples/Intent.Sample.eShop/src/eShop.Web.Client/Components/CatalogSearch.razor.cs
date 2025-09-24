using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eShop.Web.Client.Contracts.Catalog.Services.CatalogItems;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace eShop.Web.Client.Components
{
    public partial class CatalogSearch
    {
        public List<CatalogBrandDto> CatalogBrands { get; set; }
        public List<CatalogTypeDto> CatalogItemTypes { get; set; }
        [Parameter]
        public int? BrandId { get; set; }
        [Parameter]
        public int? ItemTypeId { get; set; }
        [Inject]
        public ICatalogService CatalogService { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                CatalogBrands = await CatalogService.CatalogBrandsAsync();
                CatalogItemTypes = await CatalogService.CatalogTypesAsync();
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        [IntentIgnore]
        private string BrandUri(int? brandId) => Nav.GetUriWithQueryParameters(new Dictionary<string, object?>()
        {
            { "page", null },
            { "brandId", brandId },
        });

        [IntentIgnore]
        private string TypeUri(int? typeId) => Nav.GetUriWithQueryParameters(new Dictionary<string, object?>()
        {
            { "page", null },
            { "itemTypeId", typeId },
        });

        [IntentIgnore]
        private void NavigateTo(string url)
        {
            Nav.NavigateTo(url, true);
        }

        [IntentIgnore]
        private string GetChipClassBrand(int? value) =>
        BrandId == value ? "background-color: var(--mud-palette-dark); color: white;" : null;

        [IntentIgnore]
        private string GetChipClassType(int? value) =>
        ItemTypeId == value ? "background-color: var(--mud-palette-dark); color: white;" : null;
    }
}