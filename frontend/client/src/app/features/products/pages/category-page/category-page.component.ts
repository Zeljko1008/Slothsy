import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs';
import { CategoriesService } from '../../../../core/services/categories.service';
import { ProductService } from '../../../../core/services/product.service';
import { Categories } from '../../../../shared/models/categories';
import { Product } from '../../../../shared/models/product';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { CategoryCardComponent } from '../../../../shared/ui/category-card/category-card.component';
import { ProductCardComponent } from '../../../../shared/ui/product-card/product-card.component';
import { CategoryTreeSidebarComponent } from '../../../../shared/ui/category-tree-sidebar/category-tree-sidebar.component';

@Component({
  selector: 'app-category-page',
  standalone: true,
  imports: [CommonModule, NgIf, NgFor,CategoryCardComponent,ProductCardComponent, CategoryTreeSidebarComponent],
  templateUrl: './category-page.component.html',
  styleUrl: './category-page.component.scss',
})
export class CategoryPageComponent implements OnInit {

  categorySlug: string = '';
  category: Categories | null = null;
  subcategories: Categories[] = [];
  products: Product[] = [];
  isLoading = true;
  currentPage = 1;
  pageSize = 12;
  totalCount = 0;
  selectedSubcategorySlug: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private categoriesService: CategoriesService,
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.categorySlug = params['slug'];
      if (this.categorySlug) {
        this.loadCategoryData();
      }
    });
  }



 loadCategoryData(): void {
    this.isLoading = true;

    this.categoriesService.getCategoryBySlug(this.categorySlug).subscribe({
      next: category => {
        this.category = category;

        this.categoriesService
          .getSubcategoriesByParentSlug(this.categorySlug)
          .subscribe({
            next: subcats => {
              this.subcategories = subcats;
               if (!this.selectedSubcategorySlug && this.subcategories.length > 0) {
      this.selectedSubcategorySlug = this.subcategories[0].slug!;
      this.loadProducts(this.selectedSubcategorySlug);// Set the first subcategory as selected by default
    }
  },
            error: err => {
              console.error('Error loading subcategories:', err);
            },
          });

        this.loadProducts();
      },
      error: err => {
        console.error('Error loading category:', err);
        this.isLoading = false;
      },
    });
  }

 onSubcategoryClick(slug: string): void {
  this.selectedSubcategorySlug = slug;
  this.loadProducts(slug);
}

  loadProducts(categorySlug?: string): void {
  const slugToLoad = categorySlug ?? this.selectedSubcategorySlug ?? this.categorySlug;

  const paginationParams = {
    pageNumber: this.currentPage,
    pageSize: this.pageSize,
    includeInactive: false,
  };

  this.productService.getProductsByCategoryTreeSlug(slugToLoad, paginationParams).subscribe({
    next: response => {
      this.products = response.items;
      this.totalCount = response.totalCount;
      this.isLoading = false;
    },
    error: err => {
      console.error('Error loading products:', err);
      this.isLoading = false;
    },
  });
}


   onPageChange(page: number): void {
    this.currentPage = page;
    this.loadProducts();
  }

  getLowestPrice(product: Product): number {
  return Math.min(...product.variants.map(v => v.price));
}
get selectedSubcategory(): Categories | undefined {
  return this.subcategories.find(sub => sub.slug === this.selectedSubcategorySlug);
}





}

