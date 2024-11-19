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
import { Router } from '@angular/router';
import { WeatherService } from '../../services/weather.service';
import { WeatherDataService } from '../../services/weather-data.service';
import { WeatherCondition } from '../../models/weather';

@Component({
  selector: 'app-location-input',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './location-input.component.html',
  styleUrls: ['./location-input.component.scss'],
})
export class LocationInputComponent implements OnInit {
  locationForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private weatherService: WeatherService,
    private weatherDataService: WeatherDataService, // Inject the WeatherDataService
    private router: Router
  ) {}
  ngOnInit(): void {
    this.locationForm = this.fb.group({
      location: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.locationForm.valid) {
      const location = this.locationForm.value.location;
      this.weatherService.getWeatherData(location).subscribe(
        (weatherCondition: WeatherCondition) => {
          // Save weather data and location to the shared service
          this.weatherDataService.setWeatherData(weatherCondition, location);

          // Extract the temperature
          const temperature = weatherCondition.temperature;

          // Navigate to recommendations with temperature as query parameter
          this.router.navigate(['/recommendations'], {
            queryParams: { temperature },
          });
        },
        (error) => {
          // Handle error, e.g., show an error message
          console.error('Failed to get weather data', error);
        }
      );
    }
  }
}
