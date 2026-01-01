//@IntentMerge()
import { IntentIgnoreBody, IntentMerge, IntentIgnore } from './../../../intent/intent.decorators';
import { UpdateTitleCommand } from './../../../service-proxies/models/backend/services/titles/update-title-command';
import { TitleDto } from './../../../service-proxies/models/backend/services/titles/title-dto';
import { TitlesService } from './../../../service-proxies/titles/titles-service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

interface UpdateTitleModel {
  id: string | null;
  name: string;
}

@IntentMerge()
@Component({
  selector: 'app-title-edit-page',
  standalone: true,
  templateUrl: 'title-edit-page.component.html',
  styleUrls: ['title-edit-page.component.scss'],
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ]
})
export class TitleEditPageComponent implements OnInit {
  serviceErrors = {
    loadTitleByIdError: null as string | null,
    updateTitleError: null as string | null
  };
  isLoading = false;
  titleId: string = '';
  titleByIdModels: TitleDto | null = null;

  //@IntentMerge()
  constructor(private route: ActivatedRoute, private router: Router, private readonly titlesService: TitlesService) {
  }

  @IntentMerge()
  ngOnInit(): void {
    const titleId = this.route.snapshot.paramMap.get('titleId');
    if (!titleId) {
      throw new Error("Expected 'titleId' not supplied");
    }
    this.titleId = titleId;
    this.loadTitleById(this.titleId);
  }

  @IntentMerge()
  loadTitleById(id: string): void {
    this.serviceErrors.loadTitleByIdError = null;
    this.isLoading = true;
    
    this.titlesService.getTitleById(id)
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
     )
    .subscribe({
      next: (data) => {
        this.titleByIdModels = data;
      },
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.loadTitleByIdError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  navigateToTitleListPage(): void {
    this.router.navigate(['/title', 'list']);
  }

  @IntentMerge()
  updateTitle(): void {
    this.serviceErrors.updateTitleError = null;
    this.isLoading = true;
    
    if(!this.titleByIdModels) {
      this.serviceErrors.updateTitleError = "Property 'titleByIdModels' cannot be null";
      this.isLoading = false;
      return;
    }
    this.titlesService.updateTitle({
      id: this.titleByIdModels.id,
      name: this.titleByIdModels.name,
    })
    .pipe(
        finalize(() => {
          this.isLoading = false; 
        })
    )
    .subscribe({
      error: (err) => {
        const message = err?.error?.message || err.message || 'Unknown error';
        this.serviceErrors.updateTitleError = `Failed to call service: ${message}`;

        console.error('Failed to call service:', err);
      }
    });
  }

  save(form: NgForm): void {
    if (form.invalid) {
      return;
    }

    this.serviceErrors.updateTitleError = null;

    if (!this.titleByIdModels) {
      this.serviceErrors.updateTitleError = "Property 'titleByIdModels' cannot be null";
      return;
    }

    this.isLoading = true;

    this.titlesService
      .updateTitle({
        id: this.titleByIdModels.id,
        name: this.titleByIdModels.name
      })
      .pipe(
        finalize(() => {
          this.isLoading = false;
        })
      )
      .subscribe({
        next: () => {
          this.navigateToTitleListPage();
        },
        error: (err) => {
          const message = err?.error?.message || err.message || 'Unknown error';
          this.serviceErrors.updateTitleError = `Failed to call service: ${message}`;

          console.error('Failed to call service:', err);
        }
      });
  }
}
