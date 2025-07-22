import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaginationParams } from '../../shared/models/pagination-params';
import { PaginationResult } from '../../shared/models/pagination-result';
import { Product } from '../../shared/models/product';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  /**
   * Fetches products by category tree slug with optional pagination parameters.
   * @param slug - The slug of the category tree.
   * @param params - Optional pagination parameters.
   * @returns An observable of PaginationResult containing products.
   */
  getProductsByCategoryTreeSlug(slug: string, params?: PaginationParams) {
    let queryParams = new HttpParams();

    if (params) {
      if (params.pageNumber)
        queryParams = queryParams.set(
          'PageNumber',
          params.pageNumber.toString()
        );
      if (params.pageSize)
        queryParams = queryParams.set('PageSize', params.pageSize.toString());
      if (params.includeInactive !== undefined)
        queryParams = queryParams.set(
          'IncludeInactive',
          params.includeInactive.toString()
        );
    }

    return this.http.get<PaginationResult<Product>>(
      `${this.baseUrl}products/categorytree/${slug}`,
      { params: queryParams }
    );
  }

  /**
   * fetches a product by its slug.
   * @param slug - The slug of the product to fetch.
   * @returns - An observable of Product.
   */
  getProductBySlug(slug: string): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}products/${slug}`);
  }
}
