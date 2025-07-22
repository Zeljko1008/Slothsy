import { EnumOption } from "./enum-option";

export interface ProductAddForm {
  purposes: EnumOption[];
  fits: EnumOption[];
  genders: EnumOption[];
  ageGroups: EnumOption[];
  materials: EnumOption[];
  seasons: EnumOption[];
}
