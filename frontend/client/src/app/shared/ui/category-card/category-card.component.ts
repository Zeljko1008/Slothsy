import { Component, Input } from '@angular/core';
import { Categories } from '../../models/categories';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-category-card',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './category-card.component.html',
  styleUrl: './category-card.component.scss'
})
export class CategoryCardComponent {
 @Input() category!: Categories;

    baseUrl = environment.apiBaseUrl;
     baseImgUrl = 'https://localhost:7053/'; // Base URL for images

 getFullImageUrl(): string {
    if (!this.category?.bannerImageUrl) return 'assets/placeholder.jpg';
    return `${this.baseImgUrl}${this.category.bannerImageUrl.replace(/^\/+/, '')}`;
  }
}
