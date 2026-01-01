//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { CreateDiscountCommand } from './../../../service-proxies/models/backend/services/discounts/create-discount-command';
import { DiscountsService } from './../../../service-proxies/discounts/discounts-service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

interface CreateDiscountModel {
  code: string;
  discountAmount: number | null;
  expiry: Date | null;
}

@IntentMerge()
@Component({
  selector: 'app-discount-add-page',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  templateUrl: 'discount-add-page.component.html',
  styleUrls: ['discount-add-page.component.scss'],
})
export class DiscountAddPageComponent implements OnInit {
  serviceErrors = {
    createDiscountError: null as string | null
  };
  isLoading = false;
  model: CreateDiscountModel = {
    code: '',
    discountAmount: 0,
    expiry: null
  };

  //@IntentMerge()
  constructor(private router: Router, private readonly discountsService: DiscountsService) {
  }

  @IntentMerge()
  ngOnInit(): void {
  }

  save(form: NgForm): void {
    if (this.isLoading || form.invalid) {
      return;
    }
    this.createDiscount();
    this.navigateToDiscountListPage();
  }

  @IntentMerge()
  createDiscount(): void {
    this.serviceErrors.createDiscountError = null;
    this.isLoading = true;
    
    this.discountsService.createDiscount({
      code: this.model.code,
      discountAmount: this.model.discountAmount!,
      expiry: this.model.expiry!,
    })
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
    )
    .subscribe({
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.createDiscountError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  navigateToDiscountListPage(): void {
    this.router.navigate(['/discount-list']);
  }
}
