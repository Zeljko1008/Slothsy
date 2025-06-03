/**
 * Product model representing an item in the e-commerce platform.
 */
export interface Product {
  id: string;
  name: string;
  description: string;
  price: number;
  discountPrice?: number;
  stockQuantity: number;
  sku: string;
  isActive: boolean;
  createdAt: string;
  imageUrl: string;
  categoryId: string;
  categoryName?: string;

  // SEO
  seoTitle?: string;
  seoDescription?: string;
  slug?: string;
}
