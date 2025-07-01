import { ProductVariantImage } from "./product-variant-image";

export interface ProductVariant {
  id: string;
  sizeOptionId: string;
  sizeLabel: string;
  colorOptionId: string;
  colorName: string;
  colorHex?: string;
  price: number;
  discountPrice?: number;
  stockQuantity: number;
  images: ProductVariantImage[];
}
