import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { CustomSnackbarComponent } from '../../shared/ui/custom-snackbar/custom-snackbar.component';

@Injectable({
  providedIn: 'root'
})
export class NotificationServiceService {

  constructor(private snackBar: MatSnackBar) { }

  showSuccess(message: string, title?: string) {
    this.openSnackBar(message, title, 'success');
  }

  showError(message: string, title?: string) {
    this.openSnackBar(message, title, 'error');
  }

  showInfo(message: string, title?: string) {
    this.openSnackBar(message, title, 'info');
  }

  showWarning(message: string, title?: string) {
    this.openSnackBar(message, title, 'warning');
  }


   private openSnackBar(message: string, title: string | undefined, type: string) {
    this.snackBar.openFromComponent(CustomSnackbarComponent, {
      data: { message, title, type },
      duration: 4000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
    });
  }
}
