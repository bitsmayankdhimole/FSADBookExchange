import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-private-layout',
  templateUrl: './private-layout.component.html',
  styleUrls: ['./private-layout.component.css']
})
export class PrivateLayoutComponent {
  addBookModalVisible = false;

  constructor(private router: Router) {}

  showAddBookModal(event: Event): void {
    event.preventDefault(); // Prevent the default action of the anchor tag
    this.addBookModalVisible = true;
  }

  logout(): void {
    // Implement logout logic here
    // For example, clear user session and navigate to login page
    this.router.navigate(['/login']);
  }
}