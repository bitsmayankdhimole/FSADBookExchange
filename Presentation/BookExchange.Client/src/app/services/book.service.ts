import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = 'https://localhost:7183/api'; // Replace with your API endpoint

  constructor(private http: HttpClient) {}

  getBooks(): Observable<any> {
    const userId = localStorage.getItem('userId'); // Read userId from local storage
    return this.http.get(`${this.apiUrl}/book/user/${userId}`);
  }

  addBook(book: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const userId = localStorage.getItem('userId'); // Read userId from local storage
    const bookWithUserId = { ...book, userId }; // Add userId to the book object
    return this.http.post(`${this.apiUrl}/book`, bookWithUserId, { headers });
  }

  updateBook(book: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put(`${this.apiUrl}/book/${book.bookId}`, book, { headers });
  }

  searchBooks(userId: number, searchTerm?: string, genre?: string, condition?: string, availabilityStatus?: string, language?: string, page: number = 1, pageSize: number = 10): Observable<any> {
    let params = new HttpParams()
      .set('searchTerm', searchTerm || '')
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    if (genre) {
      params = params.set('genre', genre);
    }
    if (condition) {
      params = params.set('condition', condition);
    }
    if (availabilityStatus) {
      params = params.set('availabilityStatus', availabilityStatus);
    }
    if (language) {
      params = params.set('language', language);
    }

    return this.http.get(`${this.apiUrl}/book/search/${userId}`, { params });
  }
}