// clothing.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { WeatherRecommendation } from '../models/weather';
import { ClothingItem } from '../models/wardrobe';
import { WeatherCondition } from '../models/weather';
import { environment } from '../environment/environment';

interface ProblemDetails {
  type?: string;
  title?: string;
  status?: number;
  detail?: string;
  instance?: string;
}

@Injectable({
  providedIn: 'root'
})
export class ClothingService {
  private apiUrl = 'http://localhost:5055/api';

  constructor(private http: HttpClient) { }

  getRecommendations(location: string): Observable<WeatherRecommendation> {
    return this.http.get<WeatherRecommendation>(
      `${this.apiUrl}/weather/recommendations?location=${encodeURIComponent(location)}`
    ).pipe(
      catchError(this.handleError)
    );
  }

  getAllClothingItems(): Observable<ClothingItem[]> {
    return this.http.get<ClothingItem[]>(`${this.apiUrl}/clothingitems`).pipe(
      catchError(this.handleError)
    );
  }

  getClothingItem(id: number): Observable<ClothingItem> {
    return this.http.get<ClothingItem>(`${this.apiUrl}/clothingitems/${id}`).pipe(
      catchError(this.handleError)
    );
  }

  createClothingItem(item: Partial<ClothingItem>): Observable<ClothingItem> {
    console.log('Creating clothing item with payload:', JSON.stringify(item, null, 2));
    return this.http.post<ClothingItem>(`${this.apiUrl}/clothingitems`, item).pipe(
      catchError(error => {
        console.error('Error creating clothing item:', error);
        console.error('Error response:', error.error);
        return this.handleError(error);
      })
    );
  }

  updateClothingItem(item: ClothingItem): Observable<ClothingItem> {
    return this.http.put<ClothingItem>(`${this.apiUrl}/clothingitems/${item.ClothingItemId}`, item).pipe(
      catchError(this.handleError)
    );
  }

  deleteClothingItem(itemId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/clothingitems/${itemId}`).pipe(
      catchError(this.handleError)
    );
  }

  getWeatherConditions(): Observable<WeatherCondition[]> {
    return this.http.get<WeatherCondition[]>(`${this.apiUrl}/weather/conditions`).pipe(
      catchError(this.handleError)
    );
  }

  uploadImage(formData: FormData): Observable<{ imageUrl: string }> {
    return this.http.post<{ imageUrl: string }>(`${this.apiUrl}/clothingitems/images/upload`, formData).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An error occurred';
    
    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = error.error.message;
    } else if (error.error && (error.error as ProblemDetails).detail) {
      // API error with problem details
      errorMessage = (error.error as ProblemDetails).detail || errorMessage;
    } else {
      // Other API error
      errorMessage = error.message;
    }
    
    return throwError(() => new Error(errorMessage));
  }
}
