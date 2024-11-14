import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-password-reset-token',
  templateUrl: './password-reset-token.component.html',
  styleUrls: ['./password-reset-token.component.css']
})
export class PasswordResetTokenComponent implements OnInit {
  passwordResetTokenForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.passwordResetTokenForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onSubmit(): void {
    if (this.passwordResetTokenForm.valid) {
      const { email } = this.passwordResetTokenForm.value;
      this.authService.passwordResetToken(email).subscribe(
        response => {
          this.router.navigate(['/reset-password']);
        },
        error => {
          console.error('Token request error', error);
        }
      );
    }
  }
}