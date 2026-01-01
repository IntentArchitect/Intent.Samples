//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { AddressType } from './../../../service-proxies/models/address-type';
import { UpdateCustomerCommand } from './../../../service-proxies/models/backend/services/customers/update-customer-command';
import { CustomerDto } from './../../../service-proxies/models/backend/services/customers/customer-dto';
import { UpdateCustomerCommandAddressesDto } from './../../../service-proxies/models/backend/services/customers/update-customer-command-addresses-dto';
import { CustomerAddressDto } from './../../../service-proxies/models/backend/services/customers/customer-address-dto';
import { UpdateCustomerCommandLoyaltyDto } from './../../../service-proxies/models/backend/services/customers/update-customer-command-loyalty-dto';
import { CustomerLoyaltyDto } from './../../../service-proxies/models/backend/services/customers/customer-loyalty-dto';
import { TitleDto } from './../../../service-proxies/models/backend/services/titles/title-dto';
import { CustomersService } from './../../../service-proxies/customers/customers-service';
import { TitlesService } from './../../../service-proxies/titles/titles-service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

interface UpdateCustomerModel {
  id: string | null;
  titleId: string | null;
  name: string;
  surname: string;
  email: string;
  isActive: boolean;
  addresses: UpdateCustomerCommandAddressesModel[];
  loyalty: UpdateCustomerCommandLoyaltyModel | null;
}

interface UpdateCustomerCommandAddressesModel {
  id: string | null;
  line1: string;
  line2: string;
  city: string;
  postal: string;
  addressType: AddressType;
}

interface UpdateCustomerCommandLoyaltyModel {
  id: string | null;
  loyaltyNo: string;
  points: number | null;
}

@IntentMerge()
@Component({
  selector: 'app-customer-edit-page',
  standalone: true,
  templateUrl: 'customer-edit-page.component.html',
  styleUrls: ['customer-edit-page.component.scss'],
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatSlideToggleModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
    MatProgressSpinnerModule
  ]
})
export class CustomerEditPageComponent implements OnInit {
  serviceErrors = {
    loadCustomerByIdError: null as string | null,
    loadTitlesError: null as string | null,
    updateCustomerError: null as string | null
  };
  isLoading = false;
  customerByIdModels: CustomerDto | null = null;
  titlesModels: TitleDto[] | null = null;
  customerId: string = '';

  AddressType = AddressType;
  hasLoyalty = false;

  //@IntentMerge()
  constructor(private route: ActivatedRoute,
      private router: Router,
      private readonly customersService: CustomersService,
      private readonly titlesService: TitlesService) {
  }

  @IntentMerge()
  ngOnInit(): void {
    const customerId = this.route.snapshot.paramMap.get('customerId');
    if (!customerId) {
      throw new Error("Expected 'customerId' not supplied");
    }
    this.customerId = customerId;
    this.loadTitles();
    this.loadCustomerById(this.customerId);
  }

  @IntentMerge()
  loadCustomerById(id: string): void {
    this.serviceErrors.loadCustomerByIdError = null;
    this.isLoading = true;
    
    this.customersService.getCustomerById(id)
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
     )
    .subscribe({
      next: (data) => {
        this.customerByIdModels = data;
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.loadCustomerByIdError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  @IntentMerge()
  loadTitles(): void {
    this.serviceErrors.loadTitlesError = null;
    this.isLoading = true;
    
    this.titlesService.getTitles()
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
     )
    .subscribe({
      next: (data) => {
        this.titlesModels = data;
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.loadTitlesError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  navigateToCustomerListPage(): void {
    this.router.navigate(['/customer-list']);
  }

  @IntentMerge()
  updateCustomer(): void {
    this.serviceErrors.updateCustomerError = null;
    this.isLoading = true;
    
    if(!this.customerByIdModels) {
      this.serviceErrors.updateCustomerError = "Property 'customerByIdModels' cannot be null";
      this.isLoading = false;
      return;
    }

    if (!this.hasLoyalty) {
      this.customerByIdModels.loyalty = null;
    }else if (this.customerByIdModels.loyalty && !this.customerByIdModels.loyalty.id) {
      this.customerByIdModels.loyalty.id = crypto.randomUUID();
    }

    this.customersService.updateCustomer({
      id: this.customerByIdModels.id,
      titleId: this.customerByIdModels.titleId,
      name: this.customerByIdModels.name,
      surname: this.customerByIdModels.surname,
      email: this.customerByIdModels.email,
      isActive: this.customerByIdModels.isActive,
      addresses: this.customerByIdModels.addresses.map(a => ({
        id: a.id,
        line1: a.line1,
        line2: a.line2,
        city: a.city,
        postal: a.postal,
        addressType: a.addressType,
      })),
      loyalty: this.customerByIdModels.loyalty
        ? {
            id: this.customerByIdModels.loyalty.id,
            loyaltyNo: this.customerByIdModels.loyalty.loyaltyNo,
            points: this.customerByIdModels.loyalty.points,
          }
        : null,
    })
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
    )
    .subscribe({
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.updateCustomerError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  save(form: NgForm): void {
    this.serviceErrors.updateCustomerError = null;
    if (!this.customerByIdModels) {
      this.serviceErrors.updateCustomerError = 'No customer loaded.';
      return;
    }
    if (form.invalid) {
      form.control.markAllAsTouched();
      return;
    }
    this.updateCustomer();
    this.navigateToCustomerListPage();
  }

  addAddress(): void {
    if (!this.customerByIdModels) {
      return;
    }
    if (!this.customerByIdModels.addresses) {
      this.customerByIdModels.addresses = [] as any;
    }
    const hasDelivery = this.customerByIdModels.addresses.some(a => a.addressType === AddressType.Delivery);
    const nextType = hasDelivery ? AddressType.Billing : AddressType.Delivery;
    this.customerByIdModels.addresses.push({
      id: '',
      line1: '',
      line2: '',
      city: '',
      postal: '',
      addressType: nextType
    } as CustomerAddressDto);
  }

  removeAddress(index: number): void {
    if (!this.customerByIdModels || !this.customerByIdModels.addresses) {
      return;
    }
    if (index >= 0 && index < this.customerByIdModels.addresses.length) {
      this.customerByIdModels.addresses.splice(index, 1);
    }
  }

  onHasLoyaltyChange(checked: boolean): void {
    this.hasLoyalty = checked;
    if (!this.customerByIdModels) {
      return;
    }
    if (checked) {
      if (!this.customerByIdModels.loyalty) {
        this.customerByIdModels.loyalty = {
          id: '',
          loyaltyNo: '',
          points: 0
        } as CustomerLoyaltyDto;
      }
    } else {
      this.customerByIdModels.loyalty = null;
    }
  }
}
