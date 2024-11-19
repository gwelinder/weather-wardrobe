import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { tap, map, catchError } from 'rxjs/operators';
import { environment } from '../environment/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<boolean> {
    const authToken = btoa(`${username}:${password}`);
    const headers = new HttpHeaders({
      Authorization: 'Basic ' + authToken,
    });
    return this.http.get(`${this.apiUrl}/api/auth/validate`, { headers }).pipe(
      tap(() => {
        localStorage.setItem('authToken', authToken);
      }),
      map(() => true),
      catchError(() => of(false))
    );
  }

  logout(): void {
    localStorage.removeItem('authToken');
  }

  isLoggedIn(): boolean {
    return localStorage.getItem('authToken') !== null;
  }

  getAuthToken(): string | null {
    return localStorage.getItem('authToken');
  }
}
