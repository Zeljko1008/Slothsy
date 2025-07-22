import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginationParams } from '../../../shared/models/pagination-params';
import { Observable } from 'rxjs';
import { Product } from '../../../shared/models/product';
import { PaginationResult } from '../../../shared/models/pagination-result';
import { ProductColorVariant } from '../../../shared/models/product-color-variant';
import { ProductVariant } from '../../../shared/models/product-variant';
import { ProductAddForm } from '../../../shared/models/product-add-form';
import { CategoryForForm } from '../../../shared/models/categories-for-form';

@Injectable({
  providedIn: 'root'
})
export class AdminProductService {

  private baseUrl= environment.apiBaseUrl;
   baseImgUrl = 'https://localhost:7053/'; // Base URL for images

  constructor(
    private http : HttpClient
  ) { }

  getAllProducts(params: PaginationParams = {}): Observable<PaginationResult<Product>> {
    let httpParams = new HttpParams();

    if (params.pageNumber != null) {
      httpParams = httpParams.set('pageNumber', params.pageNumber);
    }

    if (params.pageSize != null) {
      httpParams = httpParams.set('pageSize', params.pageSize);
    }

    if (params.includeInactive != null) {
      httpParams = httpParams.set('includeInactive', params.includeInactive);
    }

    return this.http.get<PaginationResult<Product>>(`${this.baseUrl}admin/products`, { params: httpParams });
  }
  getColorVariants(productId: string): Observable<ProductColorVariant[]> {
    return this.http.get<ProductColorVariant[]>(`${this.baseUrl}admin/products/${productId}/color-variants`);
  }
   getProductVariant(productColorVariantId: string): Observable<ProductVariant[]> {
    return this.http.get<ProductVariant[]>(
      `${this.baseUrl}admin/products/${productColorVariantId}/variants`
    );
  }
  getFormData(): Observable<ProductAddForm> {
    return this.http.get<ProductAddForm>(`${this.baseUrl}admin/enums`);
  }
  getCategoriesByGenderAndAgeGroup(gender: number, ageGroup: number) {
  return this.http.get<CategoryForForm[]>(`${this.baseUrl}admin/categories/filter`, {
    params: {
      gender: gender.toString(),
      ageGroup: ageGroup.toString()
    }
  });
}
}
