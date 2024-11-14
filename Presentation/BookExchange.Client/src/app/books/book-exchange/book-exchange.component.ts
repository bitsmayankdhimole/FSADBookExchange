import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-book-exchange',
  templateUrl: './book-exchange.component.html',
  styleUrls: ['./book-exchange.component.css']
})
export class BookExchangeComponent implements OnInit {
  book: any;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.book = history.state.book;
  }
}