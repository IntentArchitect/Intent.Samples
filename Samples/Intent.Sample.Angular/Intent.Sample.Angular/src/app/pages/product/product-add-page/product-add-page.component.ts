//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { CreateProductCommand } from './../../../service-proxies/models/backend/services/products/create-product-command';
import { BrandDto } from './../../../service-proxies/models/backend/services/brands/brand-dto';
import { BrandsService } from './../../../service-proxies/brands/brands-service';
import { ProductsService } from './../../../service-proxies/products/products-service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDividerModule } from '@angular/material/divider';

interface CreateProductModel {
  name: string;
  description: string;
  code: string;
  brandId: string | null;
}

@IntentMerge()
@Component({
  selector: 'app-product-add-page',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDividerModule
  ],
  templateUrl: 'product-add-page.component.html',
  styleUrls: ['product-add-page.component.scss'],
})
export class ProductAddPageComponent implements OnInit {
  serviceErrors = {
    loadBrandsError: null as string | null,
    createProductError: null as string | null
  };
  isLoading = false;
  brandsModels: BrandDto[] | null = null;
  model: CreateProductModel = {
    name: '',
    description: '',
    code: '',
    brandId: ''
  };

  //@IntentMerge()
  constructor(private router: Router, private readonly brandsService: BrandsService, private readonly productsService: ProductsService) {
  }

  @IntentMerge()
  ngOnInit(): void {
    this.loadBrands();
  }

  save(form: NgForm): void {
    if (this.isLoading || form.invalid) {
      return;
    }
    this.createProduct();
    this.navigateToProductListPage();
  }

  @IntentMerge()
  createProduct(): void {
    this.serviceErrors.createProductError = null;
    this.isLoading = true;
    
    this.productsService.createProduct({
      name: this.model.name,
      description: this.model.description,
      code: this.model.code,
      brandId: this.model.brandId!,
    })
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
    )
    .subscribe({
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.createProductError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  @IntentMerge()
  loadBrands(): void {
    this.serviceErrors.loadBrandsError = null;
    this.isLoading = true;
    
    this.brandsService.getBrands()
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
     )
    .subscribe({
      next: (data) => {
        this.brandsModels = data;
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.loadBrandsError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  navigateToProductListPage(): void {
    this.router.navigate(['/product', 'list']);
  }
}
