//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { DiscountDto } from './../../../service-proxies/models/backend/services/discounts/discount-dto';
import { DiscountsService } from './../../../service-proxies/discounts/discounts-service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { DiscountEditDialogComponent } from './../discount-edit-dialog/discount-edit-dialog.component';
import { DiscountDeleteDialogComponent } from './../discount-delete-dialog/discount-delete-dialog.component';

@IntentMerge()
@Component({
  selector: 'app-discount-list-page',
  standalone: true,
  templateUrl: 'discount-list-page.component.html',
  styleUrls: ['discount-list-page.component.scss'],
  imports: [CommonModule, MatCardModule, MatTableModule, MatButtonModule, MatIconModule, MatProgressSpinnerModule, MatDialogModule, MatTooltipModule]
})
export class DiscountListPageComponent implements OnInit {
  serviceErrors = {
    loadDiscountsError: null as string | null
  };
  isLoading = false;
  discountsModels: DiscountDto[] | null = null;
  displayedColumns: string[] = ['code', 'discountAmount', 'expiry', 'actions'];

  //@IntentMerge()
  constructor(private router: Router, private readonly discountsService: DiscountsService, private dialog: MatDialog) {
  }

  @IntentMerge()
  ngOnInit(): void {
    this.loadDiscounts();
  }

  @IntentMerge()
  deleteDiscount(discountId: string): void {
    const dialogRef = this.dialog.open(DiscountDeleteDialogComponent, {
      width: '400px',
      disableClose: true,
      data: {
        discountId: discountId
      }
    });

    dialogRef.componentInstance.loadDiscountById(discountId);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadDiscounts();
      }
    });
  }

  @IntentMerge()
  editDiscount(discountId: string): void {
    const dialogRef = this.dialog.open(DiscountEditDialogComponent, {
      width: '800px',
      disableClose: true,
      data: {
        discountId: discountId
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadDiscounts();
      }
    });
  }

  @IntentMerge()
  loadDiscounts(): void {
    this.serviceErrors.loadDiscountsError = null;
    this.isLoading = true;
    
    this.discountsService.getDiscounts()
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
     )
    .subscribe({
      next: (data) => {
        this.discountsModels = data;
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.loadDiscountsError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  navigateToDiscountAddPage(): void {
    this.router.navigate(['/discount', 'add']);
  }
}
