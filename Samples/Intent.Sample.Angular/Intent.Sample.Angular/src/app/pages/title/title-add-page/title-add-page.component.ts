//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { TitlesService } from './../../../service-proxies/titles/titles-service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

interface CreateTitleModel {
  name: string;
}

@IntentMerge()
@Component({
  selector: 'app-title-add-page',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  templateUrl: 'title-add-page.component.html',
  styleUrls: ['title-add-page.component.scss'],
})
export class TitleAddPageComponent implements OnInit {
  serviceErrors = {
    createTitleError: null as string | null
  };
  isLoading = false;
  model: CreateTitleModel = {
    name: ''
  };

  //@IntentMerge()
  constructor(private router: Router, private readonly titlesService: TitlesService) {
  }

  @IntentMerge()
  ngOnInit(): void {
  }

  @IntentMerge()
  createTitle(): void {
    this.serviceErrors.createTitleError = null;
    this.isLoading = true;
    
    this.titlesService.createTitle(this.model.name)
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
    )
    .subscribe({
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.createTitleError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  navigateToTitleListPage(): void {
    this.router.navigate(['/title', 'list']);
  }

  save(form: NgForm): void {
    if (this.isLoading || form.invalid) {
      return;
    }
    this.createTitle();
    this.navigateToTitleListPage();
  }
}
