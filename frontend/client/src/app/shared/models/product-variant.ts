import { ProductVariantImage } from "./product-variant-image";

export interface ProductVariant {
  id: string;
  slug: string;
  productColorVariantSlug: string;
  productColorVariantId: string;
  sizeOptionId: string;
  sizeLabel: string;
  stockQuantity: number;
  images: ProductVariantImage[];
}
