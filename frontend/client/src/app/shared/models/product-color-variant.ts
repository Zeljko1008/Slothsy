import { ProductVariant } from './product-variant';
import { ProductVariantImage } from './product-variant-image';

export interface ProductColorVariant {
  id: string;
  productId: string;
  productSlug: string;
colorOptionId: string;
colorName: string;
  slug: string;
  price: number;
  discountPrice?: number;

  variants: ProductVariant[];
  images: ProductVariantImage[];
}
