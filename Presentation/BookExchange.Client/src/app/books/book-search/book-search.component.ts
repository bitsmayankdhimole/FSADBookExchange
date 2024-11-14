import { Component, OnInit } from '@angular/core';
import { BookService } from '../../services/book.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-book-search',
  templateUrl: './book-search.component.html',
  styleUrls: ['./book-search.component.css']
})
export class BookSearchComponent implements OnInit {
  searchTerm: string = '';
  genre: string | null = null;
  condition: string | null = null;
  availabilityStatus: string | null = null;
  language: string | null = null;
  books: any[] = [];
  page: number = 1;
  pageSize: number = 3;
  hasMoreBooks: boolean = true;

  genres = [
    { label: 'All', value: null },
    { label: 'Fiction', value: 'Fiction' },
    { label: 'Non-Fiction', value: 'Non-Fiction' },
    // Add more genres as needed
  ];

  conditions = [
    { label: 'All', value: null },
    { label: 'New', value: 'New' },
    { label: 'Like New', value: 'Like New' },
    { label: 'Very Good', value: 'Very Good' },
    { label: 'Good', value: 'Good' },
    { label: 'Fair', value: 'Fair' },
    { label: 'Poor', value: 'Poor' }
  ];

  availabilityStatuses = [
    { label: 'All', value: null },
    { label: 'Available', value: 'Available' },
    { label: 'Unavailable', value: 'Unavailable' }
  ];

  languages = [
    { label: 'English', value: 'English' },
    { label: 'Spanish', value: 'Spanish' },
    { label: 'French', value: 'French' },
    { label: 'Hindi', value: 'Hindi' },
    { label: 'Mandarin', value: 'Mandarin' },
    { label: 'Arabic', value: 'Arabic' }
  ];

  constructor(private bookService: BookService, private router: Router) {}

  ngOnInit(): void {
    this.searchBooks();
  }

  searchBooks(): void {
    this.page = 1;
    this.books = [];
    this.loadMoreBooks();
  }

  loadMoreBooks(): void {
    const userId = parseInt(localStorage.getItem('userId') || '0', 10); // Read userId from local storage
    this.bookService.searchBooks(userId, this.searchTerm, this.genre || undefined, this.condition || undefined, this.availabilityStatus || undefined, this.language || undefined, this.page, this.pageSize).subscribe((data: any) => {
      this.books = [...this.books, ...data];
      this.hasMoreBooks = data.length === this.pageSize;
      this.page++;
    });
  }

  goToExchange(book: any): void {
    this.router.navigate(['/book-exchange'], { state: { book } });
  }
}