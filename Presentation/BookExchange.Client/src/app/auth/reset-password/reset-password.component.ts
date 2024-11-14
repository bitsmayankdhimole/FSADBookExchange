import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  resetPasswordForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.resetPasswordForm = this.fb.group({
      code: ['', [Validators.required]],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      verifyPassword: ['', [Validators.required]]
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(form: FormGroup): any {
    return form.get('newPassword')?.value === form.get('verifyPassword')?.value
      ? null : { mismatch: true };
  }

  onSubmit(): void {
    if (this.resetPasswordForm.valid) {
      const { code, newPassword } = this.resetPasswordForm.value;
      this.authService.resetPassword(code, newPassword).subscribe(
        response => {
          // Handle successful password reset
          this.router.navigate(['/login']);
        },
        error => {
          // Handle password reset error
          console.error('Password reset error', error);
        }
      );
    }
  }
}