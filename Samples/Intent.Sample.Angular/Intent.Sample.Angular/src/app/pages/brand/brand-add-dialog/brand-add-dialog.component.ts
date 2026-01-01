//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { BrandsService } from './../../../service-proxies/brands/brands-service';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { finalize } from 'rxjs';

interface CreateBrandModel {
  name: string;
}

@IntentMerge()
@Component({
  selector: 'app-brand-add-dialog',
  standalone: true,
  templateUrl: 'brand-add-dialog.component.html',
  styleUrls: ['brand-add-dialog.component.scss'],
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
export class BrandAddDialogComponent implements OnInit {
  serviceErrors = {
    createBrandError: null as string | null
  };
  isLoading = false;
  model: CreateBrandModel = {
    name: ''
  };

  //@IntentMerge()
  constructor(
    private dialogRef: MatDialogRef<BrandAddDialogComponent>,
    private readonly brandsService: BrandsService
  ) {
  }

  @IntentMerge()
  ngOnInit(): void {
  }

  save(form: NgForm): void {
    this.serviceErrors.createBrandError = null;

    if (form.invalid) {
      form.control.markAllAsTouched();
      return;
    }

    this.createBrand();
  }

  cancel(): void {
    this.dialogRef.close(null);
  }

  @IntentIgnore()
  createBrand(): void {
    this.serviceErrors.createBrandError = null;
    this.isLoading = true;
    
    this.brandsService.createBrand(this.model.name)
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
    )
    .subscribe({
      next: () => {
        this.dialogRef.close(true);
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.createBrandError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }
}
