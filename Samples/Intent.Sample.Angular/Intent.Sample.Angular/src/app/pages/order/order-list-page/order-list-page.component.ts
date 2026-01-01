//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { OrderSummaryDto } from './../../../service-proxies/models/backend/services/orders/order-summary-dto';
import { OrdersService } from './../../../service-proxies/orders/orders-service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { OrderDeleteDialogComponent } from './../order-delete-dialog/order-delete-dialog.component';

@IntentMerge()
@Component({
  selector: 'app-order-list-page',
  standalone: true,
  templateUrl: 'order-list-page.component.html',
  styleUrls: ['order-list-page.component.scss'],
  imports: [
    CommonModule,
    MatCardModule,
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatTooltipModule,
    MatDialogModule
  ]
})
export class OrderListPageComponent implements OnInit {
  serviceErrors = {
    loadOrdersError: null as string | null
  };
  isLoading = false;
  ordersModels: OrderSummaryDto[] | null = null;

  displayedColumns: string[] = ['orderNo', 'customer', 'email', 'orderedDate', 'total', 'title'];
  displayedColumnsWithActions: string[] = [...this.displayedColumns, 'actions'];
  pageSize = 10;
  pageIndex = 0;

  get pagedOrders(): OrderSummaryDto[] {
    const data = this.ordersModels ?? [];
    const start = this.pageIndex * this.pageSize;
    return data.slice(start, start + this.pageSize);
  }

  get totalItems(): number {
    return this.ordersModels?.length ?? 0;
  }

  //@IntentMerge()
  constructor(private router: Router, private readonly ordersService: OrdersService, private dialog: MatDialog) {
  }

  @IntentMerge()
  ngOnInit(): void {
    this.loadOrders();
  }

  @IntentMerge()
  deleteOrder(orderId: string): void {
  }

  @IntentMerge()
  loadOrders(): void {
    this.serviceErrors.loadOrdersError = null;
    this.isLoading = true;
    
    this.ordersService.getOrders()
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
     )
    .subscribe({
      next: (data) => {
        this.ordersModels = data;
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.loadOrdersError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
    
    this.ordersService.getOrders()
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
     )
    .subscribe({
      next: (data) => {
        this.ordersModels = data;
        this.pageIndex = 0;
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.loadOrdersError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  navigateToOrderAddPage(): void {
    this.router.navigate(['/order', 'add']);
  }

  navigateToOrderViewPage(orderId: string): void {
    this.router.navigate(['/order', 'view', orderId]);
  }

  onPageChange(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
  }

  openOrderDeleteDialog(orderId: string): void {
    const dialogRef = this.dialog.open(OrderDeleteDialogComponent, {
      width: '400px',
      disableClose: true,
      data: {
        orderId: orderId
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadOrders();
      }
    });
  }
}
