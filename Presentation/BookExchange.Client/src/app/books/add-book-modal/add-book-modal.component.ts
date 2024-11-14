import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BookService } from '../../services/book.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-add-book-modal',
  templateUrl: './add-book-modal.component.html',
  styleUrls: ['./add-book-modal.component.css'],
  providers: [MessageService]
})
export class AddBookModalComponent implements OnInit {
  @Input() showModal = false;
  @Output() showModalChange = new EventEmitter<boolean>();

  addBookForm: FormGroup;
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

  constructor(
    private fb: FormBuilder,
    private bookService: BookService,
    private router: Router,
    private messageService: MessageService
  ) {
    this.addBookForm = this.fb.group({
      title: ['', Validators.required],
      author: ['', Validators.required],
      genre: ['', Validators.required],
      condition: ['', Validators.required],
      availabilityStatus: ['', Validators.required],
      language: ['', Validators.required],
      imageURL: ['', Validators.required],
      description: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.addBookForm.valid) {
      const book = {
        ...this.addBookForm.value,
        userId: localStorage.getItem('userId') // Fetch userId from local storage
      };
      this.bookService.addBook(book).subscribe(() => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Book added successfully' });
        this.showModal = false;
        this.showModalChange.emit(this.showModal);
        this.router.navigate(['/books']);
      });
    }
  }

  onCancel(): void {
    this.showModal = false;
    this.showModalChange.emit(this.showModal);
  }
}