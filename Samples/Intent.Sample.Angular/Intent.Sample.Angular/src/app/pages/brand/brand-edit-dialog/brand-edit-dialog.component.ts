//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { Inject, Component, OnInit } from '@angular/core';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UpdateBrandCommand } from './../../../service-proxies/models/backend/services/brands/update-brand-command';
import { BrandDto } from './../../../service-proxies/models/backend/services/brands/brand-dto';
import { BrandsService } from './../../../service-proxies/brands/brands-service';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

interface UpdateBrandModel {
  id: string | null;
  name: string;
}

@IntentMerge()
@Component({
  selector: 'app-brand-edit-dialog',
  standalone: true,
  templateUrl: 'brand-edit-dialog.component.html',
  styleUrls: ['brand-edit-dialog.component.scss'],
  imports: [
    CommonModule,
    FormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatProgressSpinnerModule
  ]
})
export class BrandEditDialogComponent implements OnInit {
  serviceErrors = {
    updateBrandError: null as string | null,
    loadBrandByIdError: null as string | null
  };
  isLoading = false;
  brandId: string = '';
  model: UpdateBrandModel | null = null;

  //@IntentMerge()
  constructor(private readonly brandsService: BrandsService,
      @Inject(MAT_DIALOG_DATA) public data: { brandId: string },
      private dialogRef: MatDialogRef<BrandEditDialogComponent>) {
  }

  @IntentMerge()
  ngOnInit(): void {
    if(!this.data?.brandId) {
      throw new Error("Expected 'brandId' not supplied");
    }
    this.brandId = this.data.brandId
    this.loadBrandById(this.brandId);
  }

  @IntentMerge()
  loadBrandById(id: string): void {
    this.serviceErrors.loadBrandByIdError = null;
    this.isLoading = true;
    
    this.brandsService.getBrandById(id)
    .pipe(
      finalize(() => {
        this.isLoading = false; 
      })
    ).subscribe({
      next: (data) => {
        this.model = {
          id: data.id,
          name: data.name,
        };
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.loadBrandByIdError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  @IntentMerge()
  updateBrand(): void {
    this.serviceErrors.updateBrandError = null;
    this.isLoading = true;
    
    if(!this.model) {
      this.serviceErrors.updateBrandError = "Property 'model' cannot be null";
      this.isLoading = false;
      return;
    }
    
    this.brandsService.updateBrand({
      id: this.model.id!,
      name: this.model.name,
    })
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
    )
    .subscribe({
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.updateBrandError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });

  }

  onSave(form: NgForm): void {
    this.serviceErrors.updateBrandError = null;

    if (form.invalid) {
      form.control.markAllAsTouched();
      return;
    }

    this.updateBrand();
    this.dialogRef.close(null);
  }

  cancel(): void {
    this.dialogRef.close(null);
  }
}
