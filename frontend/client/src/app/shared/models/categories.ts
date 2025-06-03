/**
 * Categories interface
 * Represents a collection of categories in the e-commerce platform.
 */
export interface Categories{

  id: string;
  name: string;
  description?: string;
  shortDescription?: string;
  isActive: boolean;
  bannerImageUrl?: string;
  order: number;
  seoTitle?: string;
  seoDescription?: string;
  slug?: string;
  parentCategoryId?: string;
}
