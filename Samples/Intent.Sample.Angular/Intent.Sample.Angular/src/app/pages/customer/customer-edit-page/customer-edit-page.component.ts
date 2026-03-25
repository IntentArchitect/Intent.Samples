//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { AddressType } from './../../../service-proxies/models/address-type';
import { UpdateCustomerCommand } from './../../../service-proxies/models/backend/services/customers/update-customer-command';
import { UpdateCustomerCommandAddressesDto } from './../../../service-proxies/models/backend/services/customers/update-customer-command-addresses-dto';
import { UpdateCustomerCommandLoyaltyDto } from './../../../service-proxies/models/backend/services/customers/update-customer-command-loyalty-dto';
import { CustomerDto } from './../../../service-proxies/models/backend/services/customers/customer-dto';
import { CustomerAddressDto } from './../../../service-proxies/models/backend/services/customers/customer-address-dto';
import { CustomerLoyaltyDto } from './../../../service-proxies/models/backend/services/customers/customer-loyalty-dto';
import { TitleDto } from './../../../service-proxies/models/backend/services/titles/title-dto';
import { TitlesService } from './../../../service-proxies/titles/titles-service';
import { CustomersService } from './../../../service-proxies/customers/customers-service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonModule } from '@angular/material/button';
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
    MatIconModule,
    MatSelectModule,
    MatSlideToggleModule,
    MatButtonModule,
    MatDividerModule,
    MatProgressSpinnerModule
  ],
})
export class CustomerEditPageComponent implements OnInit {
  serviceErrors = {
    loadTitlesError: null as string | null,
    updateCustomerError: null as string | null,
    loadCustomerByIdError: null as string | null
  };
  isLoading = false;
  titlesModels: TitleDto[] | null = null;
  customerId: string = '';
  model: UpdateCustomerModel | null = null;

  AddressType = AddressType;
  hasLoyalty = false;

  //@IntentMerge()
  constructor(private route: ActivatedRoute,
      private router: Router,
      private readonly titlesService: TitlesService,
      private readonly customersService: CustomersService) {
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
    ).subscribe({
      next: (data) => {
        this.model = {
          id: data.id,
          titleId: data.titleId,
          name: data.name,
          surname: data.surname,
          email: data.email,
          isActive: data.isActive,
          addresses: data.addresses.map(a => ({
            id: a.id,
            line1: a.line1,
            line2: a.line2,
            city: a.city,
            postal: a.postal,
            addressType: a.addressType,
          })),
          loyalty: data.loyalty
        ? {
                id: data.loyalty.id,
                loyaltyNo: data.loyalty.loyaltyNo,
                points: data.loyalty.points,
              }
            : null,
        };
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
    
    if(!this.model) {
      this.serviceErrors.updateCustomerError = "Property 'model' cannot be null";
      this.isLoading = false;
      return;
    }
    this.customersService.updateCustomer({
      id: this.model.id!,
      titleId: this.model.titleId!,
      name: this.model.name,
      surname: this.model.surname,
      email: this.model.email,
      isActive: this.model.isActive,
      addresses: this.model.addresses.map(a => ({
        id: a.id!,
        line1: a.line1,
        line2: a.line2,
        city: a.city,
        postal: a.postal,
        addressType: a.addressType,
      })),
      loyalty: this.model.loyalty
        ? {
            id: this.model.loyalty.id!,
            loyaltyNo: this.model.loyalty.loyaltyNo,
            points: this.model.loyalty.points!,
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

  save(): void {
    if (!this.hasLoyalty && this.model) {
      this.model.loyalty = null;
    }
    this.updateCustomer();
    this.navigateToCustomerListPage();
  }

  onHasLoyaltyChange(value: boolean): void {
    this.hasLoyalty = value;
    if (!this.model) {
      return;
    }
    if (value) {
      if (!this.model.loyalty) {
        this.model.loyalty = {
          id: null,
          loyaltyNo: '',
          points: 0
        };
      }
    } else {
      this.model.loyalty = null;
    }
  }

  addAddress(): void {
    if (!this.model) {
      return;
    }
    if (!this.model.addresses) {
      this.model.addresses = [];
    }
    const hasDelivery = this.model.addresses.some(a => a.addressType === AddressType.Delivery);
    const nextType = hasDelivery ? AddressType.Billing : AddressType.Delivery;
    this.model.addresses.push({
      id: null,
      line1: '',
      line2: '',
      city: '',
      postal: '',
      addressType: nextType
    });
  }

  removeAddress(index: number): void {
    if (!this.model || !this.model.addresses) {
      return;
    }
    if (index >= 0 && index < this.model.addresses.length) {
      this.model.addresses.splice(index, 1);
    }
  }
}
