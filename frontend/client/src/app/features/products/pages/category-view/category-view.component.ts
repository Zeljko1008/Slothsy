import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../../../core/services/product.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../../../shared/models/product';
import { PaginationResult } from '../../../../shared/models/pagination-result';

@Component({
  selector: 'app-category-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './category-view.component.html',
  styleUrl: './category-view.component.scss'
})
export class CategoryViewComponent implements OnInit{

  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);

  products: Product[] = [];
  isLoading = true;


  ngOnInit(): void {
      const slug = this.route.snapshot.paramMap.get('slug');
    if (slug) {
      this.loadProducts(slug);
    }
  }

   loadProducts(slug: string): void {
    this.isLoading = true;

    this.productService.getProductsByCategoryTreeSlug(slug).subscribe({
      next: (res: PaginationResult<Product>) => {
        this.products = res.items;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Failed to load products:', err);
        this.isLoading = false;
      },
    });
  }

  getLowestPrice(product: Product): number | null {
  if (!product.variants || product.variants.length === 0) {
    return null;
  }
  return product.variants.reduce((lowest, variant) => {
    const priceToCheck = variant.discountPrice ?? variant.price;
    return priceToCheck < lowest ? priceToCheck : lowest;
  }, product.variants[0].discountPrice ?? product.variants[0].price);
}

}
