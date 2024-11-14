import { Component, OnInit } from '@angular/core';
import { BookService } from '../../services/book.service';

@Component({
  selector: 'app-book-listing',
  templateUrl: './book-listing.component.html',
  styleUrls: ['./book-listing.component.css']
})
export class BookListingComponent implements OnInit {
  books: any[] = [];
  conditions = [
    { label: 'New', value: 'New' },
    { label: 'Like New', value: 'Like New' },
    { label: 'Very Good', value: 'Very Good' },
    { label: 'Good', value: 'Good' },
    { label: 'Fair', value: 'Fair' },
    { label: 'Poor', value: 'Poor' }
  ];
  availabilityStatuses = [
    { label: 'Available', value: 'Available' },
    { label: 'Unavailable', value: 'Unavailable' }
  ];
  genres = [
    { label: 'Fiction', value: 'Fiction' },
    { label: 'Non-Fiction', value: 'Non-Fiction' },
    // Add more genres as needed
  ];
  languages = [
    { label: 'English', value: 'English' },
    { label: 'Spanish', value: 'Spanish' },
    { label: 'French', value: 'French' },
    { label: 'Hindi', value: 'Hindi' },
    { label: 'Mandarin', value: 'Mandarin' },
    { label: 'Arabic', value: 'Arabic' }
  ];

  constructor(private bookService: BookService) {}

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    this.bookService.getBooks().subscribe((data: any) => {
      this.books = data.map((book: any) => ({ ...book, editing: false }));
    });
  }

  editBook(book: any): void {
    book.editing = true;
  }

  cancelEdit(book: any): void {
    book.editing = false;
    this.loadBooks(); // Reload books to discard changes
  }

  updateBook(book: any): void {
    this.bookService.updateBook(book).subscribe(() => {
      book.editing = false;
      this.loadBooks(); // Reload books to reflect changes
    });
  }
}