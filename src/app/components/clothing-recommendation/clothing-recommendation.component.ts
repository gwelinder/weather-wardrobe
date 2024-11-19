import { Component, OnInit } from '@angular/core';
import { ClothingService } from '../../services/clothing.service';
import { WeatherRecommendation } from '../../models/weather';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-clothing-recommendation',
  standalone: true,
  imports: [CommonModule, MatProgressSpinnerModule, MatCardModule, MatIconModule],
  template: `
    <div class="container">
      <mat-spinner *ngIf="isLoading"></mat-spinner>

      <div *ngIf="error" class="error-message">
        {{ error }}
      </div>

      <div *ngIf="recommendation && !isLoading" class="recommendation-container">
        <!-- Weather Info Card -->
        <div class="weather-section">
          <mat-card class="weather-card">
            <mat-card-header>
              <mat-card-title>
                <mat-icon [class]="getWeatherIcon()">{{ getWeatherIcon() }}</mat-icon>
                Weather in {{ recommendation.Location }}
              </mat-card-title>
            </mat-card-header>
            <mat-card-content>
              <div class="weather-info">
                <p><mat-icon>thermostat</mat-icon> {{ recommendation.Temperature.toFixed(1) }}°C</p>
                <p><mat-icon>wb_sunny</mat-icon> {{ recommendation.WeatherCondition.ConditionName }}</p>
              </div>
            </mat-card-content>
          </mat-card>
        </div>

        <!-- Recommendations Section -->
        <div class="recommendations-section">
          <h2>Recommended Clothing</h2>
          <div class="clothing-grid">
            <mat-card *ngFor="let item of recommendation.RecommendedItems" class="clothing-item">
              <mat-card-header>
                <mat-card-title>{{ item.Name }}</mat-card-title>
              </mat-card-header>
              <img mat-card-image [src]="item.ImageURL || getDefaultImage(item.Name)" [alt]="item.Name">
              <mat-card-content>
                <p>{{ item.Description }}</p>
              </mat-card-content>
            </mat-card>
          </div>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .container {
      max-width: 1400px;
      margin: 2rem auto;
      padding: 1rem;
    }

    .recommendation-container {
      display: flex;
      flex-direction: column;
      gap: 2rem;
    }

    .weather-section {
      width: 100%;
    }

    .weather-card {
      background: linear-gradient(135deg, #f8f9fa 0%, #e9ecef 100%);
      
      mat-card-header {
        margin-bottom: 1rem;
      }

      mat-card-title {
        display: flex;
        align-items: center;
        gap: 0.5rem;
        font-size: 1.5rem;
        
        mat-icon {
          font-size: 2rem;
          width: 2rem;
          height: 2rem;
        }
      }
    }

    .weather-info {
      display: flex;
      flex-wrap: wrap;
      gap: 2rem;
      padding: 0.5rem;
      
      p {
        display: flex;
        align-items: center;
        gap: 0.5rem;
        margin: 0;
        font-size: 1.2rem;
        
        mat-icon {
          color: #1976d2;
        }
      }
    }

    .recommendations-section {
      width: 100%;

      h2 {
        color: #1976d2;
        margin-bottom: 1.5rem;
        font-size: 1.8rem;
        text-align: center;
      }
    }

    .clothing-grid {
      display: grid;
      grid-template-columns: repeat(3, 1fr);
      gap: 1.5rem;
      align-items: stretch;
    }

    .clothing-item {
      height: 100%;
      display: flex;
      flex-direction: column;
      transition: transform 0.2s, box-shadow 0.2s;
      
      &:hover {
        transform: translateY(-4px);
        box-shadow: 0 4px 15px rgba(0,0,0,0.1);
      }

      mat-card-header {
        background: #f8f9fa;
        padding: 1rem;
        
        mat-card-title {
          font-size: 1.2rem;
          color: #1976d2;
          margin: 0;
        }
      }

      img {
        height: 200px;
        object-fit: cover;
      }

      mat-card-content {
        padding: 1rem;
        flex-grow: 1;
        display: flex;
        flex-direction: column;
        
        p {
          margin: 0;
          color: #666;
          line-height: 1.5;
        }
      }
    }

    .error-message {
      color: #f44336;
      padding: 1rem;
      border-radius: 4px;
      background-color: rgba(244, 67, 54, 0.1);
      margin-bottom: 1rem;
    }

    mat-spinner {
      margin: 2rem auto;
    }

    .cold {
      color: #2196f3;
    }

    .warm {
      color: #ff9800;
    }

    .hot {
      color: #f44336;
    }

    @media (max-width: 1200px) {
      .clothing-grid {
        grid-template-columns: repeat(2, 1fr);
      }
    }

    @media (max-width: 768px) {
      .container {
        padding: 0.5rem;
      }

      .clothing-grid {
        grid-template-columns: 1fr;
        gap: 1rem;
      }

      .weather-info {
        gap: 1rem;
        
        p {
          font-size: 1rem;
        }
      }
    }
  `]
})
export class ClothingRecommendationComponent implements OnInit {
  recommendation: WeatherRecommendation | null = null;
  error: string | null = null;
  isLoading = true;

  constructor(
    private clothingService: ClothingService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const location = params['location'];
      if (location) {
        this.getRecommendations(location);
      } else {
        this.error = 'No location provided';
        this.isLoading = false;
      }
    });
  }

  private getRecommendations(location: string) {
    this.isLoading = true;
    this.error = null;
    
    this.clothingService.getRecommendations(location)
      .subscribe({
        next: (data) => {
          this.recommendation = data;
          this.error = null;
          this.isLoading = false;
        },
        error: (error) => {
          console.error('Error fetching recommendations:', error);
          this.error = 'Error fetching recommendations. Please try again.';
          this.recommendation = null;
          this.isLoading = false;
        }
      });
  }

  getWeatherIcon(): string {
    if (!this.recommendation) return 'wb_sunny';
    
    const temp = this.recommendation.Temperature;
    if (temp < 0) return 'ac_unit';
    if (temp < 15) return 'thermostat';
    if (temp < 25) return 'wb_sunny';
    return 'whatshot';
  }

  getDefaultImage(itemName: string): string {
    // Return a placeholder image URL based on the item name
    const defaultImage = 'https://via.placeholder.com/300x200/f8f9fa/1976d2?text=' + 
      encodeURIComponent(itemName.replace(/\s+/g, '+'));
    return defaultImage;
  }
}
