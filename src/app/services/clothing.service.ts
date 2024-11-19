// clothing.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WeatherRecommendation } from '../models/weather';
import { ClothingItem } from '../models/wardrobe';

@Injectable({
  providedIn: 'root'
})
export class ClothingService {
  private apiUrl = 'http://localhost:5055/api';

  constructor(private http: HttpClient) { }

  getRecommendations(location: string): Observable<WeatherRecommendation> {
    return this.http.get<WeatherRecommendation>(`${this.apiUrl}/weather/recommendations?location=${encodeURIComponent(location)}`);
  }

  getAllClothingItems(): Observable<ClothingItem[]> {
    return this.http.get<ClothingItem[]>(`${this.apiUrl}/clothingitems`);
  }

  deleteClothingItem(itemId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/clothingitems/${itemId}`);
  }
}
