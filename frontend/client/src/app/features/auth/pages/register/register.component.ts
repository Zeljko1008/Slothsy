import { CommonModule, NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';
import { TextInputComponent } from "../../../../shared/ui/text-input/text-input.component";
import { MatSnackBar } from '@angular/material/snack-bar';
import { NotificationServiceService } from '../../../../core/services/notification-service.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NgFor, TextInputComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {

  registerForm : FormGroup = new FormGroup({});
  submitted = false;
  errorMessage: string [] = [];
  validationErrors?:string[];

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private notificationService: NotificationServiceService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

register() {
  this.submitted = true;
  this.errorMessage = [];
  this.validationErrors = [];

  // Mark all form fields as touched so validation messages appear
  this.registerForm.markAllAsTouched();

  // Client-side form validation check
  if (this.registerForm.invalid) {
    this.notificationService.showError(
      'Please fill in all required fields correctly.',
      'Form Invalid'
    );
    return;
  }

  this.authService.register(this.registerForm.value).subscribe({
    next: (response: any) => {
      // Show success notification from API response
      this.notificationService.showSuccess(response.message, response.title);
      this.registerForm.reset();
      this.submitted = false;

      // Redirect after successful registration
      this.router.navigate(['/login']);
    },
    error: (err) => {
      // Case 1: API returns a general error with title and message
      if (err.error?.message && err.error?.title) {
        this.notificationService.showError(err.error.message, err.error.title);
      }

      // Case 2: API returns validation errors as a simple array of strings
      if (Array.isArray(err.error?.errors)) {
        this.validationErrors = err.error.errors;
      }

      // Case 3: API returns ModelState-style errors (key: string[])
      if (err.error?.errors && !Array.isArray(err.error.errors)) {
        this.validationErrors = Object.values(err.error.errors).flat() as string[];
      }

      // Case 4: No recognizable error structure from API
      if (!err.error?.message && !err.error?.errors) {
        this.notificationService.showError(
          'Unexpected error occurred.',
          'Error'
        );
      }
    }
  });
}


  initializeForm() {
     this.registerForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
      lastName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20)]],
    });
  }


}
