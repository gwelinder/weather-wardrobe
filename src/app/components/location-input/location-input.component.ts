// LocationInputComponent
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { Router, ActivatedRoute } from '@angular/router';
import { ClothingService } from '../../services/clothing.service';
import { WeatherRecommendation } from '../../models/weather';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-location-input',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './location-input.component.html',
  styleUrls: ['./location-input.component.scss'],
})
export class LocationInputComponent implements OnInit {
  locationForm!: FormGroup;
  error: string | null = null;
  isLoading = false;

  constructor(
    private fb: FormBuilder,
    private clothingService: ClothingService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.locationForm = this.fb.group({
      location: ['', Validators.required],
    });

    // Get the location from query params if available
    this.route.queryParams.subscribe(params => {
      const location = params['location'];
      if (location) {
        this.locationForm.patchValue({ location });
      }
    });
  }

  onSubmit(): void {
    if (this.locationForm.valid) {
      const location = this.locationForm.value.location;
      this.error = null;
      this.isLoading = true;
      
      this.clothingService.getRecommendations(location).subscribe({
        next: (recommendation: WeatherRecommendation) => {
          this.isLoading = false;
          this.router.navigate(['/recommendations'], {
            queryParams: { location }
          });
        },
        error: (error: HttpErrorResponse) => {
          console.error('Failed to get recommendations', error);
          this.error = error.error?.message || error.message || 'Failed to get recommendations. Please try again.';
          this.isLoading = false;
        }
      });
    }
  }
}
