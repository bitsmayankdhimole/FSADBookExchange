import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Router } from '@angular/router';
import { tap, catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private apiUrl = 'https://localhost:7183/api'; // Replace with your API endpoint
  private loggedIn = false;

  constructor(private http: HttpClient, private router: Router) {}

  login(credentials: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(`${this.apiUrl}/session/login`, credentials, { headers, withCredentials: true }).pipe(
      tap((response: any) => {
        this.loggedIn = true;
        localStorage.setItem('userId', response.userId); // Store userId in local storage
      })
    );
  }

  passwordResetToken(email: string): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(`${this.apiUrl}/user/password-reset-token`, { email }, { headers, withCredentials: true });
  }

  resetPassword(resetToken: string, newPassword: string): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(`${this.apiUrl}/user/reset-password`, { resetToken, newPassword }, { headers, withCredentials: true });
  }

  logout(): void {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    this.http.post(`${this.apiUrl}/session/logout`, null, { headers, withCredentials: true }).subscribe(() => {
      this.loggedIn = false;
      localStorage.removeItem('userId'); // Remove userId from local storage
      this.router.navigate(['/login']);
    });
  }

  isLoggedIn(): Observable<boolean> {
    if (this.loggedIn) {
      return of(true);
    } else {
      return this.http.get(`${this.apiUrl}/session/check-session`, { withCredentials: true }).pipe(
        tap(() => this.loggedIn = true),
        map(() => true),
        catchError(() => of(false))
      );
    }
  }
}