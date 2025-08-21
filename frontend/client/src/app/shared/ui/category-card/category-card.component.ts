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
     baseImgUrl = environment.baseImgUrl; // Base URL for images

 getFullImageUrl(): string {
    if (!this.category?.bannerImageUrl) return '/assets/no-item.jpg';
    return `${this.baseImgUrl}${this.category.bannerImageUrl.replace(/^\/+/, '')}`;
  }
  onImageError(event: Event): void {
  const imgElement = event.target as HTMLImageElement;
  imgElement.src = '/assets/no-item.jpg';
}

}
