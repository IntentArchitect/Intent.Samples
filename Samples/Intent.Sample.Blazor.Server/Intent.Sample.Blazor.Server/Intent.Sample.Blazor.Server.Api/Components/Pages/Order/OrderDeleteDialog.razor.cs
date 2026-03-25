using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Orders.DeleteOrder;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Order
{
    public partial class OrderDeleteDialog
    {
        [Parameter]
        public Guid OrderId { get; set; }
        public DeleteOrderModel? Model { get; set; }
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;
        [CascadingParameter]
        public IMudDialogInstance Dialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Model = new DeleteOrderModel
            {
                Id = OrderId
            };
            await Task.CompletedTask;
        }

        private async Task DeleteOrder()
        {
            try
            {
                await Mediator.Send(new DeleteOrderCommand(
                    id: Model.Id.Value));
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private void OnCancel()
        {
            Dialog.Cancel();
        }

        private async Task OnConfirmDelete()
        {
            await DeleteOrder();
            Dialog.Close(DialogResult.Ok(true));
        }

        public class DeleteOrderModel
        {
            public Guid? Id { get; set; }
        }
    }
}
