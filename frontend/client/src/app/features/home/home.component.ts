import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';

import { RouterOutlet } from '@angular/router';
import { CategoriesService } from '../../core/services/categories.service';
import { Categories } from '../../shared/models/categories';
import { CarouselItemComponent } from './components/carousel-item.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule,  RouterOutlet, CarouselItemComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit  {


  private categoriesService = inject(CategoriesService);
  title = 'Slothsy';
  categories: Categories[] = [];
  categories$ = this.categoriesService.getMainCategories();
    currentIndex = 0;
  intervalId?: any;


  ngOnInit(): void {
   this.getMainCategories();
  }
  ngOnDestroy(): void {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }
  /**
   * Fetches the main categories from the CategoriesService and initializes the carousel.
   */
  getMainCategories(): void{
 this.categoriesService.getMainCategories().subscribe({
    next: (response) =>{
       this.categories = response;
       console.log('Fetched categories:', response);
      if(this.categories.length > 0) {
        this.startAutoSlide();
      }
    },
    error: error => console.error('Error fetching categories:', error),
    complete: () => console.log('Categories fetched successfully', this.categories)
  });

  }
  /**
   * Starts the automatic sliding of categories in the carousel.
   * The current index is updated every 5 seconds to show the next category.
   */
  startAutoSlide(): void {
    this.intervalId = setInterval(() => {
      this.currentIndex = (this.currentIndex + 1) % this.categories.length;
    }, 5000); // each slide changes every 5 seconds
  }
  /**
   * Returns the current category based on the current index.
   * @returns The current category or undefined if no categories are available.
   */
  get currentCategory(): Categories | undefined {
    return this.categories[this.currentIndex];
  }

}

