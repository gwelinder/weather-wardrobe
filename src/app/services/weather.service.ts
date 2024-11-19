// weather.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { WeatherCondition } from '../models/weather';
import { environment } from '../environment/environment';

@Injectable({
  providedIn: 'root',
})
export class WeatherService {
  private weatherData = new BehaviorSubject<WeatherCondition | null>(null);

  constructor(private http: HttpClient) {}

  getWeatherData(location: string): Observable<WeatherCondition> {
    return this.http
      .get<WeatherCondition>(
        `${environment.apiUrl}/api/weather?location=${location}`
      )
      .pipe(tap((data) => this.weatherData.next(data)));
  }

  getCurrentWeather(): Observable<WeatherCondition | null> {
    return this.weatherData.asObservable();
  }
}
