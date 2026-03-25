using System.ComponentModel.DataAnnotations;
using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Blazor.Server.Application.Customers;
using Intent.Sample.Blazor.Server.Application.Customers.CreateCustomer;
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
    public partial class CustomerAddPage
    {
        public CreateCustomerModel Model { get; set; } = new();
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
                if (_hasLoyalty)
                {
                    Model.Loyalty ??= new CreateCustomerCommandLoyaltyModel
                    {
                        LoyaltyNo = string.Empty,
                        Points = 0
                    };
                }
                else
                {
                    Model.Loyalty = null;
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadTitles();
            Model.Addresses ??= new List<CreateCustomerCommandAddressesModel>();
            if (Model.Addresses.Count == 0)
            {
                Model.Addresses.Add(new CreateCustomerCommandAddressesModel
                {
                    AddressType = AddressType.Delivery,
                    Line1 = string.Empty,
                    Line2 = string.Empty,
                    City = string.Empty,
                    Postal = string.Empty
                });
            }

            _hasLoyalty = false;
            Model.Loyalty = null;
        }

        private async Task CreateCustomer()
        {
            try
            {
                await Mediator.Send(new CreateCustomerCommand(
                    titleId: Model.TitleId.Value,
                    name: Model.Name,
                    surname: Model.Surname,
                    email: Model.Email,
                    isActive: Model.IsActive,
                    addresses: Model.Addresses
                        .Select(a => new CreateCustomerCommandAddressesDto
                        {
                            Line1 = a.Line1,
                            Line2 = a.Line2,
                            City = a.City,
                            Postal = a.Postal,
                            AddressType = a.AddressType
                        })
                        .ToList(),
                    loyalty: Model.Loyalty is not null
                        ? new CreateCustomerCommandLoyaltyDto
                        {
                            LoyaltyNo = Model.Loyalty.LoyaltyNo,
                            Points = Model.Loyalty.Points.Value
                        }
                        : null));
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
                if (!HasLoyalty)
                {
                    Model.Loyalty = null;
                }

                await CreateCustomer();
                Snackbar.Add("Customer created successfully.", Severity.Success);
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

        private void Cancel()
        {
            NavigateToCustomerListPage();
        }

        public void AddAddress()
        {
            var nextType = Model.Addresses.Count(a => a.AddressType == AddressType.Delivery) == 0
                ? AddressType.Delivery
                : AddressType.Billing;

            Model.Addresses.Add(new CreateCustomerCommandAddressesModel
            {
                AddressType = nextType,
                Line1 = string.Empty,
                Line2 = string.Empty,
                City = string.Empty,
                Postal = string.Empty
            });
        }

        public void RemoveAddress(int index)
        {
            if (index >= 0 && index < Model.Addresses.Count)
            {
                Model.Addresses.RemoveAt(index);
            }
        }

        public class CreateCustomerModel
        {
            public Guid? TitleId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Surname { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public bool IsActive { get; set; }
            public List<CreateCustomerCommandAddressesModel> Addresses { get; set; } = new();
            public CreateCustomerCommandLoyaltyModel? Loyalty { get; set; }
        }
        public class CreateCustomerCommandAddressesModel
        {
            public string Line1 { get; set; } = string.Empty;
            public string Line2 { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
            public string Postal { get; set; } = string.Empty;
            public AddressType AddressType { get; set; }
        }
        public class CreateCustomerCommandLoyaltyModel
        {
            public string LoyaltyNo { get; set; } = string.Empty;
            public int? Points { get; set; }
        }
    }
}
