//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { ProductDto } from './../../../service-proxies/models/backend/services/products/product-dto';
import { UpdateProductCommand } from './../../../service-proxies/models/backend/services/products/update-product-command';
import { BrandDto } from './../../../service-proxies/models/backend/services/brands/brand-dto';
import { ProductsService } from './../../../service-proxies/products/products-service';
import { BrandsService } from './../../../service-proxies/brands/brands-service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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

interface UpdateProductModel {
  id: string | null;
  name: string;
  description: string;
  code: string;
  brandId: string | null;
}

@IntentMerge()
@Component({
  selector: 'app-product-edit-page',
  standalone: true,
  templateUrl: 'product-edit-page.component.html',
  styleUrls: ['product-edit-page.component.scss'],
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ]
})
export class ProductEditPageComponent implements OnInit {
  serviceErrors = {
    loadProductByIdError: null as string | null,
    updateProductError: null as string | null,
    loadBrandsError: null as string | null
  };
  isLoading = false;
  productId: string = '';
  brandsModels: BrandDto[] | null = null;
  model: UpdateProductModel | null = null;

  //@IntentMerge()
  constructor(private route: ActivatedRoute,
      private router: Router,
      private readonly productsService: ProductsService,
      private readonly brandsService: BrandsService) {
  }

  @IntentMerge()
  ngOnInit(): void {
    const productId = this.route.snapshot.paramMap.get('productId');
    if (!productId) {
      throw new Error("Expected 'productId' not supplied");
    }
    this.productId = productId;
    this.loadBrands();
    this.loadProductById(this.productId);
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

  @IntentMerge()
  loadProductById(id: string): void {
    this.serviceErrors.loadProductByIdError = null;
    this.isLoading = true;
    
    this.productsService.getProductById(id)
    .pipe(
      finalize(() => {
        this.isLoading = false; 
      })
    ).subscribe({
      next: (data) => {
        this.model = {
          id: data.id,
          name: data.name,
          description: data.description,
          code: data.code,
          brandId: data.brandId,
        };
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.loadProductByIdError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  navigateToProductListPage(): void {
    this.router.navigate(['/product', 'list']);
  }

  @IntentMerge()
  updateProduct(): void {
    this.serviceErrors.updateProductError = null;
    this.isLoading = true;
    
    if(!this.model) {
      this.serviceErrors.updateProductError = "Property 'model' cannot be null";
      this.isLoading = false;
      return;
    }
    this.productsService.updateProduct({
      id: this.model.id!,
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
        this.serviceErrors.updateProductError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  save(form: NgForm): void {
    if (form.invalid || !this.model) {
      form.form.markAllAsTouched();
      return;
    }
    this.updateProduct();
    this.navigateToProductListPage();
  }
}
