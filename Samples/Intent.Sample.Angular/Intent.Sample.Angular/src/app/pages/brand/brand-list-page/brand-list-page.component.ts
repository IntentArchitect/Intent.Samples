//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { BrandDto } from './../../../service-proxies/models/backend/services/brands/brand-dto';
import { BrandsService } from './../../../service-proxies/brands/brands-service';
import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { BrandAddDialogComponent } from './../brand-add-dialog/brand-add-dialog.component';
import { BrandEditDialogComponent } from './../brand-edit-dialog/brand-edit-dialog.component';

@IntentMerge()
@Component({
  selector: 'app-brand-list-page',
  standalone: true,
  templateUrl: 'brand-list-page.component.html',
  styleUrls: ['brand-list-page.component.scss'],
  imports: [CommonModule, MatCardModule, MatButtonModule, MatTableModule, MatProgressSpinnerModule, MatIconModule, MatDialogModule]
})
export class BrandListPageComponent implements OnInit {
  serviceErrors = {
    loadBrandsError: null as string | null
  };
  isLoading = false;
  brandsModels: BrandDto[] | null = null;
  displayedColumns: string[] = ['name'];
  private dialog = inject(MatDialog);

  //@IntentMerge()
  constructor(private router: Router, private readonly brandsService: BrandsService) {
    this.displayedColumns = ['name', 'actions'];
  }

  @IntentMerge()
  ngOnInit(): void {
    this.loadBrands();
  }

  @IntentMerge()
  addBrand(): void {
    const dialogRef = this.dialog.open(BrandAddDialogComponent, {
      width: '600px',
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadBrands();
      }
    });
  }

  @IntentMerge()
  editBrand(brandId: string): void {
    const dialogRef = this.dialog.open(BrandEditDialogComponent, {
      width: '600px',
      disableClose: true,
      data: {
        brandId: brandId
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadBrands();
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

  navigateToBrandAddPage(): void {
    this.router.navigate(['/brand-add']);
  }
}
