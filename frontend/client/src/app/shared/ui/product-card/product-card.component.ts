import { Component, Input } from '@angular/core';
import { Product } from '../../models/product';
import { CommonModule } from '@angular/common';
import { environment } from '../../../../environments/environment';
import { ProductVariant } from '../../models/product-variant';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.scss'
})
export class ProductCardComponent {

  @Input() product!:Product;
   @Input() variant!: ProductVariant;
  baseUrl = environment.apiBaseUrl;
       baseImgUrl = 'https://localhost:7053/'; // Base URL for images



  getLowestPrice(): number {
  if (!this.product?.variants?.length) return 0;
  return Math.min(
    ...this.product.variants.map(v => v.discountPrice ?? v.price)
  );
}

getProductImageUrl(): string {
  const imageUrl = this.product?.variants?.[0]?.images?.[0]?.imageUrl;
  if (!imageUrl) return 'assets/placeholder.jpg';
  return `${this.baseImgUrl}${imageUrl.replace(/^\/+/, '')}`;
}
}



