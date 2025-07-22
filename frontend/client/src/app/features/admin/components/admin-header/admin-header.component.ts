import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-admin-header',
  standalone: true,
  imports: [],
  templateUrl: './admin-header.component.html',
  styleUrl: './admin-header.component.scss'
})
export class AdminHeaderComponent {

  @Output() toggleSidebar = new EventEmitter<void>();

   username = 'admin@slothsy.com'; // Dummy — kasnije doći iz AuthService

onSearch(query: string) {
  console.log('Search query:', query);
}


  onLogout() {
    console.log('Logging out...');
    // kasnije: authService.logout() + redirect
  }

}
