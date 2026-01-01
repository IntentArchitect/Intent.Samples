//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { Inject, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { OrdersService } from './../../../service-proxies/orders/orders-service';
import { finalize } from 'rxjs';

interface DeleteOrderModel {
  id: string | null;
}

@IntentMerge()
@Component({
  selector: 'app-order-delete-dialog',
  standalone: true,
  templateUrl: 'order-delete-dialog.component.html',
  styleUrls: ['order-delete-dialog.component.scss'],
  imports: [CommonModule, MatDialogModule, MatButtonModule, MatIconModule, MatProgressSpinnerModule]
})
export class OrderDeleteDialogComponent implements OnInit {
  serviceErrors = {
    deleteOrderError: null as string | null
  };
  isLoading = false;
  orderId: string = '';
  model: DeleteOrderModel | null = null;

  //@IntentMerge()
  constructor(private readonly ordersService: OrdersService,
      @Inject(MAT_DIALOG_DATA) public data: { orderId: string },
      private dialogRef: MatDialogRef<OrderDeleteDialogComponent>) {
  }

  @IntentMerge()
  ngOnInit(): void {
    if(!this.data?.orderId) {
      throw new Error("Expected 'orderId' not supplied");
    }
    this.orderId = this.data.orderId
    this.model = { id: this.orderId };
  }

  @IntentMerge()
  deleteOrder(): void {
    this.serviceErrors.deleteOrderError = null;
    this.isLoading = true;
    
    if(!this.model) {
      this.serviceErrors.deleteOrderError = "Property 'model' cannot be null";
      this.isLoading = false;
      return;
    }
    this.ordersService.deleteOrder(this.model.id!)
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
    )
    .subscribe({
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.deleteOrderError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }

  onConfirm(): void {
    this.deleteOrder();
    this.dialogRef.close(true);
  }
}
