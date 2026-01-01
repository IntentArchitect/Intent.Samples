//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { OrderDto } from './../../../service-proxies/models/backend/services/orders/order-dto';
import { OrdersService } from './../../../service-proxies/orders/orders-service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@IntentMerge()
@Component({
  selector: 'app-order-view-page',
  standalone: true,
  templateUrl: 'order-view-page.component.html',
  styleUrls: ['order-view-page.component.scss'],
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatDividerModule,
    MatButtonModule,
    MatProgressSpinnerModule
  ],
})
export class OrderViewPageComponent implements OnInit {
  serviceErrors = {
    loadOrderByIdError: null as string | null
  };
  isLoading = false;
  orderId: string = '';
  orderByIdModels: OrderDto | null = null;

  //@IntentMerge()
  constructor(private route: ActivatedRoute, private router: Router, private readonly ordersService: OrdersService) {
  }

  @IntentMerge()
  ngOnInit(): void {
    const orderId = this.route.snapshot.paramMap.get('orderId');
    if (!orderId) {
      throw new Error("Expected 'orderId' not supplied");
    }
    this.orderId = orderId;
    this.loadOrderById(this.orderId);
  }

  @IntentMerge()
  loadOrderById(id: string): void {
    this.serviceErrors.loadOrderByIdError = null;
    this.isLoading = true;
    
    this.ordersService.getOrderById(id)
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
     )
    .subscribe({
      next: (data) => {
        this.orderByIdModels = data;
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.loadOrderByIdError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  navigateToOrderListPage(): void {
    this.router.navigate(['/order', 'list']);
  }
}
