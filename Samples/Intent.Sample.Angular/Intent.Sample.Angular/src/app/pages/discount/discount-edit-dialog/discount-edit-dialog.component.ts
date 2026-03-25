//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { Inject, Component, OnInit } from '@angular/core';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UpdateDiscountCommand } from './../../../service-proxies/models/backend/services/discounts/update-discount-command';
import { DiscountDto } from './../../../service-proxies/models/backend/services/discounts/discount-dto';
import { DiscountsService } from './../../../service-proxies/discounts/discounts-service';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

interface UpdateDiscountModel {
  id: string | null;
  code: string;
  discountAmount: number | null;
  expiry: Date | null;
}

@IntentMerge()
@Component({
  selector: 'app-discount-edit-dialog',
  standalone: true,
  templateUrl: 'discount-edit-dialog.component.html',
  styleUrls: ['discount-edit-dialog.component.scss'],
  imports: [
    CommonModule,
    FormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatProgressSpinnerModule
  ]
})
export class DiscountEditDialogComponent implements OnInit {
  serviceErrors = {
    loadDiscountByIdError: null as string | null,
    updateDiscountError: null as string | null
  };
  isLoading = false;
  discountId: string = '';
  model: UpdateDiscountModel | null = null;

  //@IntentMerge()
  constructor(private readonly discountsService: DiscountsService,
      @Inject(MAT_DIALOG_DATA) public data: { discountId: string },
      private dialogRef: MatDialogRef<DiscountEditDialogComponent>) {
  }

  @IntentMerge()
  ngOnInit(): void {
    if(!this.data?.discountId) {
      throw new Error("Expected 'discountId' not supplied");
    }
    this.discountId = this.data.discountId
    this.loadDiscount();
  }

  @IntentMerge()
  loadDiscountById(id: string): void {
    this.serviceErrors.loadDiscountByIdError = null;
    this.isLoading = true;
    
    this.discountsService.getDiscountById(id)
    .pipe(
      finalize(() => {
        this.isLoading = false; 
      })
    ).subscribe({
      next: (data) => {
        this.model = {
          id: data.id,
          code: data.code,
          discountAmount: data.discountAmount,
          expiry: data.expiry,
        };
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.loadDiscountByIdError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  @IntentMerge()
  updateDiscount(): void {
    this.serviceErrors.updateDiscountError = null;
    this.isLoading = true;
    
    if(!this.model) {
      this.serviceErrors.updateDiscountError = "Property 'model' cannot be null";
      this.isLoading = false;
      return;
    }
    this.discountsService.updateDiscount({
      id: this.model.id!,
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
        this.serviceErrors.updateDiscountError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  loadDiscount(): void {
    this.serviceErrors.loadDiscountByIdError = null;
    this.isLoading = true;

    this.discountsService.getDiscountById(this.discountId)
      .pipe(
        finalize(() => {
          this.isLoading = false;
        })
      )
      .subscribe({
        next: (data) => {
          this.model = {
            id: data.id,
            code: data.code,
            discountAmount: data.discountAmount,
            expiry: data.expiry ? new Date(data.expiry) : null
          };
        },
        error: (err) => {
          const message = err?.error?.message || err.message || 'Unknown error';
          this.serviceErrors.loadDiscountByIdError = `Failed to call service: ${message}`;

          console.error('Failed to call service:', err);
        }
      });
  }

  save(): void {
    this.serviceErrors.updateDiscountError = null;

    if (!this.model) {
      this.serviceErrors.updateDiscountError = "Property 'model' cannot be null";
      return;
    }

    if (this.model.discountAmount == null || this.model.expiry == null) {
      this.serviceErrors.updateDiscountError = 'Please provide all required fields.';
      return;
    }

    this.isLoading = true;

    this.discountsService.updateDiscount({
      id: this.model.id!,
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
        next: () => {
          if (!this.serviceErrors.updateDiscountError) {
            this.dialogRef.close(true);
          }
        },
        error: (err) => {
          const message = err?.error?.message || err.message || 'Unknown error';
          this.serviceErrors.updateDiscountError = `Failed to call service: ${message}`;

          console.error('Failed to call service:', err);
        }
      });
  }

  onSave(form: NgForm): void {
    this.serviceErrors.updateDiscountError = null;

    if (form.invalid) {
      form.control.markAllAsTouched();
      return;
    }

    this.save();
  }

  cancel(): void {
    this.dialogRef.close(null);
  }
}
