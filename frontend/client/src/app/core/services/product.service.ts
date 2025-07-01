import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaginationParams } from '../../shared/models/pagination-params';
import { PaginationResult } from '../../shared/models/pagination-result';
import { Product } from '../../shared/models/product';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private baseUrl = 'https://localhost:7053/api/';

  constructor(private http: HttpClient) {}

  // getProductsByCategorySlug(slug: string, params?: PaginationParams) {
  //   let queryParams = new HttpParams();

  //   if (params) {
  //     if (params.pageNumber)
  //       queryParams = queryParams.set('PageNumber', params.pageNumber.toString());
  //     if (params.pageSize)
  //       queryParams = queryParams.set('PageSize', params.pageSize.toString());
  //     if (params.includeInactive !== undefined) {
  //       queryParams = queryParams.set(
  //         'IncludeInactive',
  //         params.includeInactive.toString()
  //       );
  //     }
  //   }

  //   return this.http.get<PaginationResult<Product>>(
  //     `${this.baseUrl}products/category/${slug}`,
  //     { params: queryParams }
  //   );
  // }

  getProductsByCategoryTreeSlug(slug: string, params?: PaginationParams) {
  let queryParams = new HttpParams();

  if (params) {
    if (params.pageNumber)
      queryParams = queryParams.set('PageNumber', params.pageNumber.toString());
    if (params.pageSize)
      queryParams = queryParams.set('PageSize', params.pageSize.toString());
    if (params.includeInactive !== undefined)
      queryParams = queryParams.set('IncludeInactive', params.includeInactive.toString());
  }

  return this.http.get<PaginationResult<Product>>(
    `${this.baseUrl}products/categorytree/${slug}`,
    { params: queryParams }
  );
}
}
