// weather-data.service.ts
import { Injectable } from '@angular/core';
import { WeatherCondition } from '../models/weather';

@Injectable({
  providedIn: 'root',
})
export class WeatherDataService {
  private weatherData: WeatherCondition | null = null;
  private location: string = '';

  setWeatherData(data: WeatherCondition, location: string): void {
    this.weatherData = data;
    this.location = location;
  }

  getWeatherData(): WeatherCondition | null {
    return this.weatherData;
  }

  getLocation(): string {
    return this.location;
  }

  clearData(): void {
    this.weatherData = null;
    this.location = '';
  }
}
