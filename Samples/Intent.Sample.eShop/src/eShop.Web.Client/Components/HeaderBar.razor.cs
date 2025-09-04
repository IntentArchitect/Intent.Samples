using System.Threading.Tasks;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace eShop.Web.Client.Components
{
    public partial class HeaderBar
    {
        protected override async Task OnInitializedAsync()
        {
        }
    }
}