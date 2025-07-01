import { CommonModule, NgFor } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { Categories } from '../../models/categories';
import { CategoriesService } from '../../../core/services/categories.service';
import { Router, RouterModule } from '@angular/router';
import { Gender } from '../../enums/gender';

@Component({
  selector: 'app-category-tree-sidebar',
  standalone: true,
  imports: [CommonModule, NgFor, RouterModule],
  templateUrl: './category-tree-sidebar.component.html',
  styleUrl: './category-tree-sidebar.component.scss'
})
export class CategoryTreeSidebarComponent implements OnInit {

  categories: Categories[] = [];
  filteredCategories: Categories[] = [];

  loading = false;
  error: string | null = null;
 @Input() genderFilter?: Gender | null;
 @Input() ageGroupFilter?: number | null;


  constructor(
    private categoriesService:CategoriesService

  ) { }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories():void{
    this.loading = true;
    this.categoriesService.getAllCategories().subscribe({
      next: (categories) => {
        this.categories = categories.filter(cat => {
        let genderMatch = true;
        let ageGroupMatch = true;

        if (this.genderFilter) {
          genderMatch = cat.gender === this.genderFilter;
        }
        if (this.ageGroupFilter) {
          ageGroupMatch = cat.ageGroup === this.ageGroupFilter;
        }

        return genderMatch && ageGroupMatch;
      });

        this.filteredCategories = this.removeDuplicateSubcategories(this.categories);
      this.loading = false;
    },
      error: (err) => {
        console.error('Error loading categories:', err);
        this.error = 'Failed to load categories';
        this.loading = false;
      }
    });
  }
  private removeDuplicateSubcategories(categories: Categories[]): Categories[] {
    // Collect all subcategory slugs in a set
    const subcategorySlugs = new Set<string>();
    categories.forEach(mainCat => {
      mainCat.subcategories?.forEach(sub => {
        if (sub.slug) {
          subcategorySlugs.add(sub.slug);
        }
      });
    });

    // Filter main categories to exclude those that have subcategories already in the set
    return categories.filter(cat => !subcategorySlugs.has(cat.slug ?? ''));
  }

}
