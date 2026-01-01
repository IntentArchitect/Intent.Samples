//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { ProductDto } from './../../../service-proxies/models/backend/services/products/product-dto';
import { ProductsService } from './../../../service-proxies/products/products-service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@IntentMerge()
@Component({
  selector: 'app-product-list-page',
  standalone: true,
  templateUrl: 'product-list-page.component.html',
  styleUrls: ['product-list-page.component.scss'],
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, MatTableModule, MatProgressSpinnerModule]
})
export class ProductListPageComponent implements OnInit {
  serviceErrors = {
    loadProductsError: null as string | null
  };
  isLoading = false;
  productsModels: ProductDto[] | null = null;
  displayedColumns: string[] = ['name', 'code', 'brandName', 'description'];
  displayedColumnsWithActions: string[] = ['name', 'code', 'brandName', 'description', 'actions'];

  //@IntentMerge()
  constructor(private router: Router, private readonly productsService: ProductsService) {
  }

  @IntentMerge()
  ngOnInit(): void {
    this.loadProducts();
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

  navigateToProductAddPage(): void {
    this.router.navigate(['/product', 'add']);
  }

  navigateToProductEditPage(productId: string): void {
    this.router.navigate(['/product', 'edit', productId]);
  }
}
