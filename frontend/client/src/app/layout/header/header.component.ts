import { Component } from '@angular/core';
import {MatIcon} from '@angular/material/icon';
import {MatButton} from '@angular/material/button';
import {MatBadge} from '@angular/material/badge';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    MatIcon,
    MatButton,
    MatBadge,
    RouterLink,
    CommonModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

   isLoggedIn$: Observable<boolean>;
  currentUserName$: Observable<string | null>;
  roles$: Observable<string[]>;


  constructor(
    private authService: AuthService,
    private router: Router
  ){
    this.isLoggedIn$ = this.authService.loginStatus$;
    this.currentUserName$ = this.authService.currentUserName$;
    this.roles$ = this.authService.roles$;
  }

   logout(): void {
    this.authService.logout();
  }

}
