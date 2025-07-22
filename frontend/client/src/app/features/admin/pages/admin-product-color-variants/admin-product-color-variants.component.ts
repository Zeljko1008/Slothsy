import { Component, Input, OnInit } from '@angular/core';
import { AdminProductService } from '../../services/admin-product.service';
import { ProductColorVariant } from '../../../../shared/models/product-color-variant';
import { CommonModule } from '@angular/common';
import { ProductVariantImage } from '../../../../shared/models/product-variant-image';
import { AdminProductVariantComponent } from '../admin-product-variant/admin-product-variant.component';

@Component({
  selector: 'app-admin-product-color-variants',
  standalone: true,
  imports: [CommonModule, AdminProductVariantComponent],
  templateUrl: './admin-product-color-variants.component.html',
  styleUrl: './admin-product-color-variants.component.scss'
})
export class AdminProductColorVariantsComponent implements OnInit {

   @Input() productId!: string;
  @Input() productName!: string;
  colorVariants: ProductColorVariant[] = [];
  baseImgUrl = 'https://localhost:7053/'; // Base URL for images
  expandedVariantId: string | null = null;
  expandedImageId: string | null = null;
  activeColorVariantId: string | null = null;


  constructor(private adminProductService: AdminProductService) {}

   ngOnInit(): void {
    this.adminProductService.getColorVariants(this.productId).subscribe({
      next: (res) => (this.colorVariants = res),
      error: (err) => console.error('Greška kod dohvaćanja color varijanti', err),
    });
  }
   getMainImageUrl(variant: ProductColorVariant): string {
  const mainImage = variant.images?.find(img => img.isMain);
  if (!mainImage?.imageUrl) return '/assets/no-item.jpg';
  return `${this.baseImgUrl}${mainImage.imageUrl.replace(/^\/+/, '')}`;
}

getImageUrl(img?: ProductVariantImage): string {
  if (!img?.imageUrl) return '/assets/no-item.jpg';
  return `${this.baseImgUrl}${img.imageUrl.replace(/^\/+/, '')}`;
}

toggleVariants(id: string) {
  if (this.expandedVariantId === id) {
    this.expandedVariantId = null;
    // If the same variant is clicked again, reset the active color variant
  } else {
    // Clicking a different variant resets the active color variant
    if (this.activeColorVariantId && this.activeColorVariantId !== id) {
      this.expandedVariantId = null;
      this.expandedImageId = null;
    }

    this.expandedVariantId = id;
    this.activeColorVariantId = id;
  }
}


toggleImages(id: string) {
  if (this.expandedImageId === id) {
    this.expandedImageId = null;
    // if the same image is clicked again, reset the active color variant
  } else {
    // Clicking a different image resets the active color variant
    if (this.activeColorVariantId && this.activeColorVariantId !== id) {
      this.expandedVariantId = null;
      this.expandedImageId = null;
    }

    this.expandedImageId = id;
    this.activeColorVariantId = id;
  }
}


addVariant(variantId: string): void {
  // open modal or navigate to create form
  console.log('Add ProductVariant for ColorVariant ID:', variantId);
}

  onAddColorVariant() {
  console.log('Add color variant for product:', this.productId);
}

addImages(variantId: string): void {}

}


