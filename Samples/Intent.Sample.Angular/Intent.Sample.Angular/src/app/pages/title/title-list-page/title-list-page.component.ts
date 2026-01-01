//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { TitleDto } from './../../../service-proxies/models/backend/services/titles/title-dto';
import { TitlesService } from './../../../service-proxies/titles/titles-service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';

@IntentMerge()
@Component({
  selector: 'app-title-list-page',
  standalone: true,
  templateUrl: 'title-list-page.component.html',
  styleUrls: ['title-list-page.component.scss'],
  imports: [
    CommonModule,
    MatCardModule,
    MatTableModule,
    MatChipsModule,
    MatProgressSpinnerModule,
    MatIconModule,
    MatButtonModule,
    MatTooltipModule
  ]
})
export class TitleListPageComponent implements OnInit {
  serviceErrors = {
    loadTitlesError: null as string | null
  };
  isLoading = false;
  titlesModels: TitleDto[] | null = null;

  //@IntentMerge()
  constructor(private router: Router, private readonly titlesService: TitlesService) {
  }

  @IntentMerge()
  ngOnInit(): void {
    this.loadTitles();
  }

  @IntentMerge()
  loadTitles(): void {
    this.serviceErrors.loadTitlesError = null;
    this.isLoading = true;
    
    this.titlesService.getTitles()
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
     )
    .subscribe({
      next: (data) => {
        this.titlesModels = data;
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.loadTitlesError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  navigateToTitleAddPage(): void {
    this.router.navigate(['/title', 'add']);
  }

  navigateToTitleEditPage(titleId: string): void {
    this.router.navigate(['/title', 'edit', titleId]);
  }
}
