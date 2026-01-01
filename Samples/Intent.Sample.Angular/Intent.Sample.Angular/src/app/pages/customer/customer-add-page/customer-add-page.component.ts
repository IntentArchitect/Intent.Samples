//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { AddressType } from './../../../service-proxies/models/address-type';
import { CreateCustomerCommand } from './../../../service-proxies/models/backend/services/customers/create-customer-command';
import { CreateCustomerCommandAddressesDto } from './../../../service-proxies/models/backend/services/customers/create-customer-command-addresses-dto';
import { CreateCustomerCommandLoyaltyDto } from './../../../service-proxies/models/backend/services/customers/create-customer-command-loyalty-dto';
import { TitleDto } from './../../../service-proxies/models/backend/services/titles/title-dto';
import { CustomersService } from './../../../service-proxies/customers/customers-service';
import { TitlesService } from './../../../service-proxies/titles/titles-service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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

interface CreateCustomerModel {
  titleId: string | null;
  name: string;
  surname: string;
  email: string;
  isActive: boolean;
  addresses: CreateCustomerCommandAddressesModel[];
  loyalty: CreateCustomerCommandLoyaltyModel | null;
}

interface CreateCustomerCommandAddressesModel {
  line1: string;
  line2: string;
  city: string;
  postal: string;
  addressType: AddressType;
}

interface CreateCustomerCommandLoyaltyModel {
  loyaltyNo: string;
  points: number | null;
}

@IntentMerge()
@Component({
  selector: 'app-customer-add-page',
  standalone: true,
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
  ],
  templateUrl: 'customer-add-page.component.html',
  styleUrls: ['customer-add-page.component.scss'],
})
export class CustomerAddPageComponent implements OnInit {
  serviceErrors = {
    createCustomerError: null as string | null,
    loadTitlesError: null as string | null
  };
  isLoading = false;
  model: CreateCustomerModel = {
    titleId: '',
    name: '',
    surname: '',
    email: '',
    isActive: false,
    addresses: [],
    loyalty: {
    loyaltyNo: '',
    points: 0
  }
  };
  titlesModels: TitleDto[] | null = null;

  AddressType = AddressType;
  hasLoyalty = false;

  //@IntentMerge()
  constructor(private router: Router, private readonly customersService: CustomersService, private readonly titlesService: TitlesService) {
  }

  @IntentMerge()
  ngOnInit(): void {
    if (!this.model.addresses) {
      this.model.addresses = [];
    }
    if (this.model.addresses.length === 0) {
      this.model.addresses.push({
        line1: '',
        line2: '',
        city: '',
        postal: '',
        addressType: AddressType.Delivery
      });
    }
    this.hasLoyalty = !!this.model.loyalty;
    if (!this.hasLoyalty) {
      this.model.loyalty = null;
    }
    this.loadTitles();
  }

  save(form: NgForm): void {
    if (this.isLoading || form.invalid) {
      return;
    }
    this.createCustomer();
    this.navigateToCustomerListPage();
  }

  addAddress(): void {
    const hasDelivery = this.model.addresses.some(a => a.addressType === AddressType.Delivery);
    const nextType = hasDelivery ? AddressType.Billing : AddressType.Delivery;
    this.model.addresses.push({
      line1: '',
      line2: '',
      city: '',
      postal: '',
      addressType: nextType
    });
  }

  removeAddress(index: number): void {
    if (index >= 0 && index < this.model.addresses.length) {
      this.model.addresses.splice(index, 1);
    }
  }

  onHasLoyaltyChange(value: boolean): void {
    this.hasLoyalty = value;
    if (this.hasLoyalty) {
      if (!this.model.loyalty) {
        this.model.loyalty = {
          loyaltyNo: '',
          points: 0
        };
      }
    } else {
      this.model.loyalty = null;
    }
  }

  @IntentMerge()
  createCustomer(): void {
    this.serviceErrors.createCustomerError = null;
    this.isLoading = true;
    
    this.customersService.createCustomer({
      titleId: this.model.titleId!,
      name: this.model.name,
      surname: this.model.surname,
      email: this.model.email,
      isActive: this.model.isActive,
      addresses: this.model.addresses.map(a => ({
        line1: a.line1,
        line2: a.line2,
        city: a.city,
        postal: a.postal,
        addressType: a.addressType,
      })),
      loyalty: this.model.loyalty
        ? {
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
        this.serviceErrors.createCustomerError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
    this.serviceErrors.createCustomerError = null;
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
        this.serviceErrors.createCustomerError = `Failed to call service: ${message}`;

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
}