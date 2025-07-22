import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { switchMap } from 'rxjs';
import { CategoriesService } from '../../../../core/services/categories.service';
import { ProductService } from '../../../../core/services/product.service';
import { Categories } from '../../../../shared/models/categories';
import { Product } from '../../../../shared/models/product';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { CategoryCardComponent } from '../../../../shared/ui/category-card/category-card.component';
import { ProductCardComponent } from '../../../../shared/ui/product-card/product-card.component';
import { CategoryTreeSidebarComponent } from '../../../../shared/ui/category-tree-sidebar/category-tree-sidebar.component';
import { ProductVariant } from '../../../../shared/models/product-variant';
import { ProductColorVariant } from '../../../../shared/models/product-color-variant';

@Component({
  selector: 'app-category-page',
  standalone: true,
  imports: [
    CommonModule,
    NgIf,
    NgFor,
    CategoryCardComponent,
    ProductCardComponent,
    CategoryTreeSidebarComponent,
    RouterModule,
  ],
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
  colorVariantsToShow: ProductColorVariant[] = [];
  colorVariants: ProductColorVariant[] = [];

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
      next: (category) => {
        this.category = category;

        this.categoriesService
          .getSubcategoriesByParentSlug(this.categorySlug)
          .subscribe({
            next: (subcats) => {
              this.subcategories = subcats;
            },
            error: (err) => {
              console.error('Error loading subcategories:', err);
            },
          });

        this.loadProducts();
      },
      error: (err) => {
        console.error('Error loading category:', err);
        this.isLoading = false;
      },
    });
  }

  loadProducts(categorySlug?: string): void {
    const slugToLoad =
      categorySlug ?? this.selectedSubcategorySlug ?? this.categorySlug;

    const paginationParams = {
      pageNumber: this.currentPage,
      pageSize: this.pageSize,
      includeInactive: false,
    };

    this.productService
      .getProductsByCategoryTreeSlug(slugToLoad, paginationParams)
      .subscribe({
        next: (response) => {
          this.products = response.items;
          this.totalCount = response.totalCount;
          this.colorVariantsToShow = this.getCheapestColorVariants(
            this.products
          );
          this.isLoading = false;
        },
        error: (err) => {
          console.error('Error loading products:', err);
          this.isLoading = false;
        },
      });
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadProducts();
  }

  extractColorVariantsForDisplay(products: Product[]): ProductVariant[] {
    const variantsToShow: ProductVariant[] = [];

    for (const product of products) {
      for (const colorVariant of product.colorVariants ?? []) {
        if (colorVariant.variants?.length > 0) {
          const baseVariant = { ...colorVariant.variants[0] };

          (baseVariant as any).price = colorVariant.price;
          (baseVariant as any).discountPrice = colorVariant.discountPrice;

          baseVariant.productColorVariantSlug = colorVariant.slug;
          (baseVariant as any).productSlug = product.slug;

          baseVariant.images = colorVariant.images;

          variantsToShow.push(baseVariant);
        }
      }
    }

    return variantsToShow;
  }
  get selectedSubcategory(): Categories | undefined {
    return this.subcategories.find(
      (sub) => sub.slug === this.selectedSubcategorySlug
    );
  }
  getCheapestColorVariants(products: Product[]): ProductColorVariant[] {
    const cheapestVariants: ProductColorVariant[] = [];

    for (const product of products) {
      if (!product.colorVariants || product.colorVariants.length === 0)
        continue;

      const sortedByPrice = product.colorVariants
        .slice()
        .sort((a, b) => a.price - b.price);

      const cheapest = sortedByPrice[0];

      cheapest.productSlug = product.slug || '';

      cheapestVariants.push(cheapest);
    }

    return cheapestVariants;
  }
}
