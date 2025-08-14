import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../../core/services/auth.service';
import { Router } from '@angular/router';
import {MatCard} from '@angular/material/card';
import { MatButton } from '@angular/material/button';
import {MatFormFieldModule, MatLabel} from '@angular/material/form-field';
import {MatInput, MatInputModule} from '@angular/material/input';
import { NotificationServiceService } from '../../../../core/services/notification-service.service';
import { TextInputComponent } from '../../../../shared/ui/text-input/text-input.component';
import { LoginResponse } from '../../../../shared/models/login-response';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCard,
    MatFormFieldModule,
    MatLabel,
    MatInput,
    MatButton,
    TextInputComponent


  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {

  loginForm:FormGroup;
  submitted = false;
  validationErrors?: string[];
    errorMessage: string [] = [];

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
     this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }




  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private notificationService: NotificationServiceService
  ) {
    this.loginForm = this.fb.group({
      email: ['',[Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  onSubmit() {
  this.submitted = true;
  this.errorMessage = [];
  this.validationErrors = [];

  this.loginForm.markAllAsTouched();

  // Check if the form is valid
  if (this.loginForm.invalid) {
    this.notificationService.showError(
      'Please fill in all required fields correctly.',
      'Form Invalid'
    );
    return;
  }

  this.authService.login(this.loginForm.value).subscribe({
    next: (response: LoginResponse) => {
      // ✅ Snackbar for successful login
      this.notificationService.showSuccess(response.message, response.title);
      this.loginForm.reset();
      this.submitted = false;
      this.router.navigate(['/dashboard']);
    },
    error: (err) => {
      // 1️⃣ If backend returns validation errors
      if (err.error?.errors) {
        for (let key in err.error.errors) {
          if (err.error.errors.hasOwnProperty(key)) {
            this.validationErrors?.push(...err.error.errors[key]);
          }
        }
      }

      // 2️⃣ If backend sends custom ApiResponse with message and title
      else if (err.error?.message && err.error?.title) {
        this.notificationService.showError(err.error.message, err.error.title);
      }

      // 3️⃣ Fallback if none of the above exist
      else {
        this.notificationService.showError(
          'Unexpected error occurred.',
          'Error'
        );
      }
    }
  });
}



  }
