//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { CreateOrderCommand } from './../../../service-proxies/models/backend/services/orders/create-order-command';
import { CreateOrderCommandOrderItemsDto } from './../../../service-proxies/models/backend/services/orders/create-order-command-order-items-dto';
import { GetCustomersQuery } from './../../../service-proxies/models/backend/services/customers/get-customers-query';
import { CustomerSummaryDto } from './../../../service-proxies/models/backend/services/customers/customer-summary-dto';
import { ProductDto } from './../../../service-proxies/models/backend/services/products/product-dto';
import { OrdersService } from './../../../service-proxies/orders/orders-service';
import { CustomersService } from './../../../service-proxies/customers/customers-service';
import { ProductsService } from './../../../service-proxies/products/products-service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { PagedResult } from './../../../service-proxies/models/paged-result';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

interface CreateOrderModel {
  customerId: string | null;
  orderNo: string;
  discountCode: string;
  total: number | null;
  orderedDate: Date | null;
  orderItems: CreateOrderCommandOrderItemsModel[];
}

interface CreateOrderCommandOrderItemsModel {
  productId: string | null;
  units: number | null;
  price: number | null;
}

@IntentMerge()
@Component({
  selector: 'app-order-add-page',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
    MatProgressSpinnerModule
  ],
  templateUrl: 'order-add-page.component.html',
  styleUrls: ['order-add-page.component.scss'],
})
export class OrderAddPageComponent implements OnInit {
  serviceErrors = {
    createOrderError: null as string | null,
    loadCustomersError: null as string | null,
    loadProductsError: null as string | null
  };
  isLoading = false;
  model: CreateOrderModel = {
    customerId: '',
    orderNo: '',
    discountCode: '',
    total: 0,
    orderedDate: null,
    orderItems: []
  };
  customersModels: PagedResult<CustomerSummaryDto> | null = null;
  productsModels: ProductDto[] | null = null;

  //@IntentMerge()
  constructor(private router: Router,
      private readonly ordersService: OrdersService,
      private readonly customersService: CustomersService,
      private readonly productsService: ProductsService) {
  }

  @IntentMerge()
  ngOnInit(): void {
    if (!this.model.orderItems) {
      this.model.orderItems = [];
    }
    if (this.model.orderItems.length === 0) {
      this.addOrderItem();
    }
    this.loadCustomers(1, 50, null, null, true);
    this.loadProducts();
    this.updateTotal();
  }

  save(form: NgForm): void {
    if (this.isLoading || form.invalid || !this.model.orderItems || this.model.orderItems.length === 0) {
      return;
    }
    this.createOrder();
    this.navigateToOrderListPage();
  }

  addOrderItem(): void {
    if (!this.model.orderItems) {
      this.model.orderItems = [];
    }
    this.model.orderItems.push({
      productId: null,
      units: null,
      price: null
    });
    this.updateTotal();
  }

  removeOrderItem(index: number): void {
    if (!this.model.orderItems) {
      return;
    }
    if (index >= 0 && index < this.model.orderItems.length) {
      this.model.orderItems.splice(index, 1);
      this.updateTotal();
    }
  }

  onOrderItemChanged(): void {
    this.updateTotal();
  }

  updateTotal(): void {
    if (!this.model.orderItems || this.model.orderItems.length === 0) {
      this.model.total = 0;
      return;
    }
    const total = this.model.orderItems.reduce((sum, item) => {
      const units = item.units ?? 0;
      const price = item.price ?? 0;
      return sum + units * price;
    }, 0);
    this.model.total = total;
  }

  @IntentMerge()
  createOrder(): void {
    this.serviceErrors.createOrderError = null;
    this.isLoading = true;
    
    this.ordersService.createOrder({
      customerId: this.model.customerId!,
      orderNo: this.model.orderNo,
      discountCode: this.model.discountCode,
      total: this.model.total!,
      orderedDate: this.model.orderedDate!,
      orderItems: this.model.orderItems.map(oi => ({
        productId: oi.productId!,
        units: oi.units!,
        price: oi.price!,
      })),
    })
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
    )
    .subscribe({
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.createOrderError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  @IntentMerge()
  loadCustomers(pageNo: number, pageSize: number, searchTerm: string | null, orderBy: string | null, isActive: boolean | null): void {
    this.serviceErrors.loadCustomersError = null;
    this.isLoading = true;
    
    this.customersService.getCustomers({
      pageNo: pageNo,
      pageSize: pageSize,
      searchTerm: searchTerm,
      orderBy: orderBy,
      isActive: isActive,
    })
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
     )
    .subscribe({
      next: (data) => {
        this.customersModels = data;
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.loadCustomersError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  @IntentMerge()
  loadProducts(): void {
    this.serviceErrors.loadProductsError = null;
    this.isLoading = true;
    
    this.productsService.getProducts()
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
     )
    .subscribe({
      next: (data) => {
        this.productsModels = data;
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.loadProductsError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  navigateToOrderListPage(): void {
    this.router.navigate(['/order', 'list']);
  }
}
