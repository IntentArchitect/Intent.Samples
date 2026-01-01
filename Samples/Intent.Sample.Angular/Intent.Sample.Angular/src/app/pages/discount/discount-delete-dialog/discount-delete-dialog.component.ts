//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { Inject, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DiscountDto } from './../../../service-proxies/models/backend/services/discounts/discount-dto';
import { DiscountsService } from './../../../service-proxies/discounts/discounts-service';
import { finalize } from 'rxjs';

interface DeleteDiscountModel {
  id: string | null;
}

@IntentMerge()
@Component({
  selector: 'app-discount-delete-dialog',
  standalone: true,
  templateUrl: 'discount-delete-dialog.component.html',
  styleUrls: ['discount-delete-dialog.component.scss'],
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ]
})
export class DiscountDeleteDialogComponent implements OnInit {
  serviceErrors = {
    loadDiscountByIdError: null as string | null,
    deleteDiscountError: null as string | null
  };
  isLoading = false;
  discountId: string = '';
  discountByIdModels: DiscountDto | null = null;

  //@IntentMerge()
  constructor(private readonly discountsService: DiscountsService,
      @Inject(MAT_DIALOG_DATA) public data: { discountId: string },
      private dialogRef: MatDialogRef<DiscountDeleteDialogComponent>) {
  }

  @IntentMerge()
  ngOnInit(): void {
    if(!this.data?.discountId) {
      throw new Error("Expected 'discountId' not supplied");
    }
    this.discountId = this.data.discountId
    if(!this.data?.discountId) {
      throw new Error("Expected 'dicountId' not supplied");
    }
    this.discountId = this.data.discountId

    if (this.discountId) {
      this.loadDiscountById(this.discountId);
    }
  }

  @IntentMerge()
  deleteDiscount(): void {
    this.serviceErrors.deleteDiscountError = null;
    this.isLoading = true;
    
    if(!this.discountByIdModels) {
      this.serviceErrors.deleteDiscountError = "Property 'discountByIdModels' cannot be null";
      this.isLoading = false;
      return;
    }
    this.discountsService.deleteDiscount(this.discountByIdModels.id)
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
    )
    .subscribe({
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.deleteDiscountError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
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
     )
    .subscribe({
      next: (data) => {
        this.discountByIdModels = data;
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.loadDiscountByIdError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  save(): void {
    this.serviceErrors.deleteDiscountError = null;

    if (!this.discountByIdModels) {
      this.serviceErrors.deleteDiscountError = "Property 'discountByIdModels' cannot be null";
      return;
    }

    this.isLoading = true;

    this.discountsService.deleteDiscount(this.discountByIdModels.id)
      .pipe(
        finalize(() => {
          this.isLoading = false;
        })
      )
      .subscribe({
        next: () => {
          this.dialogRef.close(true);
        },
        error: (err) => {
          const message = err?.error?.message || err.message || 'Unknown error';
          this.serviceErrors.deleteDiscountError = `Failed to call service: ${message}`;
          console.error('Failed to call service:', err);
        }
      });
  }

  cancel(): void {
    this.dialogRef.close(null);
  }
}
