import { Component } from '@angular/core';
import { ClothingService } from '../../services/clothing.service';
import { WeatherRecommendation } from '../../models/weather';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-clothing-recommendation',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="container mt-4">
      <h2>Weather Wardrobe Recommendations</h2>
      
      <div class="form-group mb-4">
        <label for="location">Enter Location:</label>
        <input 
          type="text" 
          class="form-control" 
          id="location" 
          [(ngModel)]="location" 
          placeholder="Enter city name">
        <button 
          class="btn btn-primary mt-2" 
          (click)="getRecommendations()">
          Get Recommendations
        </button>
      </div>

      <div *ngIf="error" class="alert alert-danger">
        {{ error }}
      </div>

      <div *ngIf="recommendation" class="card">
        <div class="card-body">
          <h3>Weather in {{ recommendation.location }}</h3>
          <p>Temperature: {{ recommendation.temperature?.toFixed(1) }}°C</p>
          <p>Condition: {{ recommendation.weatherCondition?.conditionName }}</p>
          
          <h4>Recommended Clothing:</h4>
          <ul class="list-group">
            <li *ngFor="let item of recommendation.recommendedItems" class="list-group-item">
              {{ item.name }} - {{ item.description }}
            </li>
          </ul>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .container { max-width: 800px; }
    .card { margin-top: 20px; }
  `]
})
export class ClothingRecommendationComponent {
  location: string = '';
  recommendation: WeatherRecommendation | null = null;
  error: string | null = null;

  constructor(private clothingService: ClothingService) {}

  getRecommendations() {
    if (!this.location) {
      this.error = 'Please enter a location';
      return;
    }

    this.error = null;
    this.clothingService.getRecommendations(this.location)
      .subscribe({
        next: (data) => {
          this.recommendation = data;
          this.error = null;
        },
        error: (error) => {
          console.error('Error fetching recommendations:', error);
          this.error = 'Error fetching recommendations. Please try again.';
          this.recommendation = null;
        }
      });
  }
}
