import { CommonModule, NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Product } from '../../../../shared/models/product';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../../../core/services/product.service';
import { ProductVariant } from '../../../../shared/models/product-variant';
import { ProductCardComponent } from '../../../../shared/ui/product-card/product-card.component';
import { ProductVariantImage } from '../../../../shared/models/product-variant-image';
import { Categories } from '../../../../shared/models/categories';
import { CategoryTreeSidebarComponent } from '../../../../shared/ui/category-tree-sidebar/category-tree-sidebar.component';
import { ProductColorVariant } from '../../../../shared/models/product-color-variant';
import { ProductColorVariantService } from '../../../../core/services/product-color-variant.service';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule, ProductCardComponent,CategoryTreeSidebarComponent],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {

  product: Product | null = null;
  colorVariant: ProductColorVariant | null = null;
  variant: ProductVariant | null = null;
  mainImage: ProductVariantImage | null = null;
  baseImgUrl = environment.baseImgUrl;

   filteredOtherVariants: ProductColorVariant[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService,
    private productColorVariantService:ProductColorVariantService
  ) {}

   ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const colorVariantSlug = params.get('colorVariantSlug');
      if (colorVariantSlug) {
        this.loadData(colorVariantSlug);
      } else {
        console.error('colorVariantSlug is missing in route parameters');
      }
    });
  }

  loadData(slug: string): void {
    this.productColorVariantService.getBySlug(slug).subscribe({
      next: (colorVariant) => {
        this.colorVariant = colorVariant;
        this.mainImage = colorVariant.images?.[0] ?? null;

          console.log('✅ Loaded colorVariant:', colorVariant);
      console.log('📦 Calling getProductBySlug with:', colorVariant.productSlug);

        this.productService.getProductBySlug(colorVariant.productSlug).subscribe({
          next: (product) => {
            this.product = product;

            this.variant = colorVariant.variants?.[0] ?? null;


            this.filteredOtherVariants = product.colorVariants.filter(
              (cv) => cv.id !== colorVariant.id
            );
          },
          error: (err) => {
            console.error('Failed to load product', err);
          }
        });
      },
      error: (err) => {
        console.error('Failed to load color variant', err);
      }
    });
  }

  onSizeChange(event: Event): void {
    const select = event.target as HTMLSelectElement;
    const selectedSizeSlug = select.value;
    if (this.colorVariant) {
      const foundVariant = this.colorVariant.variants.find(v => v.slug === selectedSizeSlug);
      if (foundVariant) {
        this.variant = foundVariant;
      }
    }
  }

  onThumbnailClick(image: ProductVariantImage): void {
    this.mainImage = image;
  }



  getImageUrl(img?: ProductVariantImage): string {
    if (!img?.imageUrl) return '/assets/no-item.jpg';
    return `${this.baseImgUrl}${img.imageUrl.replace(/^\/+/, '')}`;
  }

  navigateToVariant(slug: string): void {
    this.router.navigate(['/product/details', slug]);
  }

  // get filteredOtherVariants(): ProductVariant[] {
  //   return this.otherVariants;
  // }
}
