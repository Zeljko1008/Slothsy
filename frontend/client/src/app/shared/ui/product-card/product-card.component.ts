import { Component, Input, OnInit } from '@angular/core';
import { Product } from '../../models/product';
import { CommonModule } from '@angular/common';
import { environment } from '../../../../environments/environment';
import { ProductVariant } from '../../models/product-variant';
import { Router } from '@angular/router';
import { ProductService } from '../../../core/services/product.service';
import { ProductColorVariant } from '../../models/product-color-variant';
import { ProductColorVariantService } from '../../../core/services/product-color-variant.service';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.scss'
})
export class ProductCardComponent implements OnInit {

  @Input() variant!: ProductVariant;
  @Input() colorVariant!: ProductColorVariant;
  baseUrl = environment.apiBaseUrl;
  baseImgUrl = 'https://localhost:7053/'; // Base URL for images
  product: Product | null = null;

  constructor( private router:Router, private productService:ProductService, private productColorVariantService: ProductColorVariantService) {}

  ngOnInit(): void {
  if (this.colorVariant?.productSlug) {
    this.productService.getProductBySlug(this.colorVariant.productSlug).subscribe({
      next: (prod) => {
        this.product = prod;
      },
      error: (err) => {
        console.error('Error loading product for variant:', err);
      }
    });
  }

}

 getImageUrl(): string {
  const imageUrl = this.colorVariant?.images?.[0]?.imageUrl;
  if (!imageUrl) return '/assets/no-item.jpg';
  return `${this.baseImgUrl}${imageUrl.replace(/^\/+/, '')}`;
}
onClick() {
  if (this.colorVariant?.slug)
    this.router.navigate(['product/details', this.colorVariant.slug]);
  console.log('Navigating to product details for color variant:', this.colorVariant?.slug);


}
  onImageError(event: Event): void {
    const imgElement = event.target as HTMLImageElement;
    imgElement.src = '/assets/no-item.jpg';
  }



}



