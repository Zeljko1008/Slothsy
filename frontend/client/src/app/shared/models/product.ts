import { CategorySummary } from "./category-summary";
import { AgeGroup } from "../enums/age-group";
import { FitType } from "../enums/fit-type";
import { Gender } from "../enums/gender";
import { ProductPurpose } from "../enums/product-purpose";
import { Season } from "../enums/season";
import { ProductVariant } from "./product-variant";
import { MaterialType } from "../enums/material-type";

/**
 * Product model representing an item in the e-commerce platform.
 */
export interface Product {
  id: string;
  name: string;
  shortDescription?: string;
  description?: string;
  purpose: ProductPurpose;
  fit: FitType;
  brand: string;
  gender: Gender;
  ageGroup: AgeGroup;
  material: MaterialType;
  season: Season;
  isActive: boolean;
  createdAt: string; // Date string from API
  seoTitle?: string;
  seoDescription?: string;
  slug?: string;
  updatedAt?: string;
  categories: CategorySummary[];
  variants: ProductVariant[];
}
