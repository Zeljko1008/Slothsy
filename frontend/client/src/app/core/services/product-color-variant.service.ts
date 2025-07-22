import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ProductColorVariant } from '../../shared/models/product-color-variant';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductColorVariantService {
  baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  /**
   * Fetches product color variants by product slug.
   * @param productSlug - The slug of the product.
   */
  getBySlug(slug: string): Observable<ProductColorVariant> {
    return this.http.get<ProductColorVariant>(
      `${this.baseUrl}ProductColorVariant/${slug}`
    );
  }
}
