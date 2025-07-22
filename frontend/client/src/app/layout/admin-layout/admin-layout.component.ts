import { Component } from '@angular/core';
import { AdminHeaderComponent } from '../../features/admin/components/admin-header/admin-header.component';
import { AdminSidebarComponent } from '../../features/admin/components/admin-sidebar/admin-sidebar.component';
import { RouterModule } from '@angular/router';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-admin-layout',
  standalone: true,
  imports: [AdminHeaderComponent, AdminSidebarComponent, RouterModule, NgIf],
  templateUrl: './admin-layout.component.html',
  styleUrl: './admin-layout.component.scss'
})
export class AdminLayoutComponent {

   isSidebarOpen = false;

  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }

}
