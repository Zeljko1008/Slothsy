import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';

import { RouterModule, RouterOutlet } from '@angular/router';
import { CategoriesService } from '../../core/services/categories.service';
import { Categories } from '../../shared/models/categories';
import { CarouselItemComponent } from './components/carousel-item.component';
import { FooterComponent } from '../../layout/footer/footer.component';
import { WhatIsSlothsyComponent } from './components/what-is-slothsy/what-is-slothsy.component';
import { CategoryCardComponent } from '../../shared/ui/category-card/category-card.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    CarouselItemComponent,
    RouterModule,
    WhatIsSlothsyComponent,
    CategoryCardComponent,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  private categoriesService = inject(CategoriesService);
  title = 'Slothsy';
  genders = ['Men', 'Women', 'Kids'];
  selectedGender = 'Men';
  carouselCategories: Categories[] = [];
  filteredCategories: Categories[] = [];
  currentIndex = 0;
  slideInterval: any;
  currentFilter: string = 'men'; // default type

  ngOnInit(): void {
    this.loadCarouselCategories();
    this.loadFilteredCategories(this.currentFilter);
  }

  loadFilteredCategories(filter: string): void {
    this.currentFilter = filter;
    this.categoriesService.getMainCategoriesFiltered(filter).subscribe({
      next: (categories) => {
        this.filteredCategories = categories.sort((a, b) => a.order - b.order);
      },
      error: (err) => console.error('Error loading filtered categories:', err),
    });
  }

  /**
   * Fetches the main categories from the CategoriesService and initializes the carousel.
   */
  loadCarouselCategories(): void {
    this.categoriesService.getMainCategoriesFiltered('').subscribe({
      next: (categories) => {
        this.carouselCategories = categories.sort((a, b) => a.order - b.order);
        this.resetCarousel();
      },
      error: (err) => console.error('Error loading carousel categories:', err),
    });
  }

  resetCarousel(): void {
    this.currentIndex = 0;
    if (this.slideInterval) {
      clearInterval(this.slideInterval);
    }
    if (this.carouselCategories.length > 0) {
      this.startAutoSlide();
    }
  }
  /**
   * Starts the automatic sliding of categories in the carousel.
   * The current index is updated every 5 seconds to show the next category.
   */
  startAutoSlide(): void {
    this.slideInterval = setInterval(() => {
      this.currentIndex =
        (this.currentIndex + 1) % this.carouselCategories.length;
    }, 5000);
  }

  onFilterClick(filter: string): void {
    this.loadFilteredCategories(filter);
  }
}
