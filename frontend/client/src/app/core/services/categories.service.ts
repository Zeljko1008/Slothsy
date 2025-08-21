import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, OnInit } from '@angular/core';
import { Categories } from '../../shared/models/categories';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  baseUrl = environment.apiBaseUrl;
  baseImgUrl = 'http://localhost:5000/'; // Base URL for images
  private http = inject(HttpClient);
  //Categories: Categories[] = [];

  /**
   * fetches the main categories from the API.
   */

  getMainCategoriesFiltered(type: string): Observable<Categories[]> {
    const params = new HttpParams()
      .set('includeInactive', false)
      .set('type', type.toLowerCase()); // osiguraj lowercase

    return this.http.get<Categories[]>(`${this.baseUrl}categories/main`, {
      params,
    });
  }

  /**
   * fetches categories from the API by slug.
   * @param slug - The slug of the category to fetch.
   */
  getCategoryBySlug(slug: string) {
    return this.http.get<Categories>(`${this.baseUrl}categories/slug/${slug}`);
  }
  /**
   * fetches subcategories by parent slug from the API.
   * @param parentSlug - The slug of the parent category.
   */
  getSubcategoriesByParentSlug(parentSlug: string, includeInactive = false) {
    return this.http.get<Categories[]>(
      `${this.baseUrl}categories/slug/${parentSlug}/subcategories?includeInactive=${includeInactive}`
    );
  }
  /**
   * fetches all categories from the API.
   * @param includeInactive - Whether to include inactive categories.
   */
  getAllCategories(includeInactive: boolean = false): Observable<Categories[]> {
    const params = new HttpParams().set(
      'includeInactive',
      includeInactive.toString()
    );

    return this.http.get<Categories[]>(`${this.baseUrl}categories`, { params });
  }
}
