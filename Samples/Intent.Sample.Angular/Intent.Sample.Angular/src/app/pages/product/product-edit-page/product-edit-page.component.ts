//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { UpdateProductCommand } from './../../../service-proxies/models/backend/services/products/update-product-command';
import { ProductDto } from './../../../service-proxies/models/backend/services/products/product-dto';
import { BrandDto } from './../../../service-proxies/models/backend/services/brands/brand-dto';
import { BrandsService } from './../../../service-proxies/brands/brands-service';
import { ProductsService } from './../../../service-proxies/products/products-service';
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
import { MatDividerModule } from '@angular/material/divider';

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
    MatProgressSpinnerModule,
    MatDividerModule
  ],
})
export class ProductEditPageComponent implements OnInit {
  serviceErrors = {
    loadBrandsError: null as string | null,
    loadProductByIdError: null as string | null,
    updateProductError: null as string | null
  };
  isLoading = false;
  productId: string = '';
  brandsModels: BrandDto[] | null = null;
  productByIdModels: ProductDto | null = null;

  //@IntentMerge()
  constructor(private route: ActivatedRoute,
      private router: Router,
      private readonly brandsService: BrandsService,
      private readonly productsService: ProductsService) {
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
     )
    .subscribe({
      next: (data) => {
        this.productByIdModels = data;
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

  @IntentIgnore()
  updateProduct(): void {
    this.serviceErrors.updateProductError = null;
    this.isLoading = true;
    
    if(!this.productByIdModels) {
      this.serviceErrors.updateProductError = "Property 'productByIdModels' cannot be null";
      this.isLoading = false;
      return;
    }
    this.productsService.updateProduct({
      id: this.productByIdModels.id,
      name: this.productByIdModels.name,
      description: this.productByIdModels.description,
      code: this.productByIdModels.code,
      brandId: this.productByIdModels.brandId,
    })
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
    )
    .subscribe({
      next: () => {
        this.navigateToProductListPage();
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.updateProductError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  save(form: NgForm): void {
    this.serviceErrors.updateProductError = null;

    if (form.invalid) {
      this.serviceErrors.updateProductError = 'Please fix validation errors before saving.';
      return;
    }

    if (!this.productByIdModels) {
      this.serviceErrors.updateProductError = "Property 'productByIdModels' cannot be null";
      return;
    }

    this.updateProduct();
  }
}
