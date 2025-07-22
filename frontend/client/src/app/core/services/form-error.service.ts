import { Injectable } from '@angular/core';
import { AbstractControl } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FormErrorService {

  shouldShowError(control:AbstractControl| null):boolean{
    return !!control && control.invalid && (control.touched || control.dirty);
  }

  getErrorMessage(control: AbstractControl | null): string | null {
    if (!control) return null;

    if (control.hasError('required')) {
      return 'This field is required';
    } else if (control.hasError('maxlength')) {
      const maxLength = control.getError('maxlength')?.requiredLength;
      return `Maximum length is ${maxLength} characters`;
    } else if (control.hasError('minlength')) {
      const minLength = control.getError('minlength')?.requiredLength;
      return `Minimum length is ${minLength} characters`;
    } else if (control.hasError('email')) {
      return 'Invalid email format';
    }

    return null;
  }
}
