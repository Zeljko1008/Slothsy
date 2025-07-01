import { SizeType } from "../enums/size-type";

export interface SizeOption {
  id: string;
  label: string;
  sizeType: SizeType;
  order: number;
}
