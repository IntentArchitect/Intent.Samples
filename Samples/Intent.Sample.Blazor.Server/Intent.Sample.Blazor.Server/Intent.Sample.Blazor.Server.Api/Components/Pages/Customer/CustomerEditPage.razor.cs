using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Customers;
using Intent.Sample.Blazor.Server.Application.Customers.GetCustomerById;
using Intent.Sample.Blazor.Server.Application.Customers.UpdateCustomer;
using Intent.Sample.Blazor.Server.Application.Titles;
using Intent.Sample.Blazor.Server.Application.Titles.GetTitles;
using Intent.Sample.Blazor.Server.Domain;
using Intent.Sample.Blazor.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

[assembly: DefaultIntentManaged(Mode.Merge)]
[assembly: IntentTemplate("Intent.Blazor.Templates.Client.RazorComponentCodeBehindTemplate", Version = "1.0")]

namespace Intent.Sample.Blazor.Server.Api.Components.Pages.Customer
{
    public partial class CustomerEditPage
    {
        [Parameter]
        public Guid CustomerId { get; set; }
        public UpdateCustomerModel? Model { get; set; }
        public List<TitleDto>? TitlesModels { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IScopedMediator Mediator { get; set; } = default!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        private MudForm? _form;
        private bool _saving;
        private bool _hasLoyalty;

        protected override async Task OnInitializedAsync()
        {
            await LoadTitles();
            await LoadCustomerById(CustomerId);
            _hasLoyalty = Model?.Loyalty != null;
            StateHasChanged();
        }

        private async Task UpdateCustomer()
        {
            try
            {
                await Mediator.Send(new UpdateCustomerCommand(
                    id: Model.Id.Value,
                    titleId: Model.TitleId.Value,
                    name: Model.Name,
                    surname: Model.Surname,
                    email: Model.Email,
                    isActive: Model.IsActive,
                    addresses: Model.Addresses
                        .Select(a => new UpdateCustomerCommandAddressesDto
                        {
                            Id = a.Id.Value,
                            Line1 = a.Line1,
                            Line2 = a.Line2,
                            City = a.City,
                            Postal = a.Postal,
                            AddressType = a.AddressType
                        })
                        .ToList(),
                    loyalty: Model?.Loyalty is not null
                        ? new UpdateCustomerCommandLoyaltyDto
                        {
                            Id = Model.Loyalty.Id.Value,
                            LoyaltyNo = Model.Loyalty.LoyaltyNo,
                            Points = Model.Loyalty.Points.Value
                        }
                        : null));
                Snackbar.Add("Customer updated successfully.", Severity.Success);
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
        }

        private async Task LoadCustomerById(Guid id)
        {
            try
            {
                var customerDto = await Mediator.Send(new GetCustomerByIdQuery(
                    id: id));
                Model = new UpdateCustomerModel
                {
                    Id = customerDto.Id,
                    TitleId = customerDto.TitleId,
                    Name = customerDto.Name,
                    Surname = customerDto.Surname,
                    Email = customerDto.Email,
                    IsActive = customerDto.IsActive,
                    Addresses = customerDto.Addresses
                        .Select(a => new UpdateCustomerCommandAddressesModel
                        {
                            Id = a.Id,
                            Line1 = a.Line1,
                            Line2 = a.Line2,
                            City = a.City,
                            Postal = a.Postal,
                            AddressType = a.AddressType
                        })
                        .ToList(),
                    Loyalty = customerDto.Loyalty is not null
                        ? new UpdateCustomerCommandLoyaltyModel
                        {
                            Id = customerDto.Loyalty?.Id,
                            LoyaltyNo = customerDto.Loyalty.LoyaltyNo,
                            Points = customerDto.Loyalty?.Points
                        }
                        : null
                };
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
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

        private void NavigateToCustomerListPage()
        {
            NavigationManager.NavigateTo("customer/list");
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
                if (!_hasLoyalty && Model is not null)
                {
                    Model.Loyalty = null;
                }

                await UpdateCustomer();
                NavigateToCustomerListPage();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Failed to save customer: {ex.Message}", Severity.Error);
            }
            finally
            {
                _saving = false;
            }
        }

        public void RemoveAddress(int index)
        {
            if (Model == null)
            {
                return;
            }

            if (index >= 0 && index < Model.Addresses.Count)
            {
                Model.Addresses.RemoveAt(index);
            }
        }

        public void AddAddress()
        {
            if (Model == null)
            {
                return;
            }

            var deliveryCount = Model.Addresses.Count(a => a.AddressType == AddressType.Delivery);

            Model.Addresses.Add(new UpdateCustomerCommandAddressesModel
            {
                Id = Guid.NewGuid(),
                Line1 = string.Empty,
                Line2 = string.Empty,
                City = string.Empty,
                Postal = string.Empty,
                AddressType = deliveryCount == 0 ? AddressType.Delivery : AddressType.Billing
            });
        }

        public bool HasLoyalty
        {
            get => _hasLoyalty;
            set
            {
                if (_hasLoyalty == value)
                {
                    return;
                }

                _hasLoyalty = value;

                if (Model == null)
                {
                    return;
                }

                if (_hasLoyalty)
                {
                    if (Model.Loyalty == null)
                    {
                        Model.Loyalty = new UpdateCustomerCommandLoyaltyModel
                        {
                            Id = Guid.NewGuid(),
                            LoyaltyNo = string.Empty,
                            Points = 0
                        };
                    }
                }
                else
                {
                    Model.Loyalty = null;
                }
            }
        }

        public class UpdateCustomerModel
        {
            public Guid? Id { get; set; }
            public Guid? TitleId { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public bool IsActive { get; set; }
            public List<UpdateCustomerCommandAddressesModel> Addresses { get; set; }
            public UpdateCustomerCommandLoyaltyModel? Loyalty { get; set; }
        }
        public class UpdateCustomerCommandAddressesModel
        {
            public Guid? Id { get; set; }
            public string Line1 { get; set; }
            public string Line2 { get; set; }
            public string City { get; set; }
            public string Postal { get; set; }
            public AddressType AddressType { get; set; }
        }
        public class UpdateCustomerCommandLoyaltyModel
        {
            public Guid? Id { get; set; }
            public string LoyaltyNo { get; set; }
            public int? Points { get; set; }
        }
    }
}
