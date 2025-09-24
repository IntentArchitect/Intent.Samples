using System.Threading.Tasks;
using eShop.Web.Client.Services;
using Intent.RoslynWeaver.Attributes;
using Microsoft.AspNetCore.Components;
using static eShop.Web.Client.Pages.Cart.Cart;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace eShop.Web.Client.Pages.Cart.Components
{
    public partial class ProductImagePrice
    {
        [Parameter]
        public int ProductId { get; set; }
        [Parameter]
        public string ProductName { get; set; }
        [Parameter]
        public decimal ProductPrice { get; set; }

        [IntentIgnore]
        [Inject]
        ProductImageUrlProvider ProductImageUrlProvider { get; set; }
        protected override async Task OnInitializedAsync()
        {
        }
    }
}