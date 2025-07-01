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
   baseImgUrl = 'https://localhost:7053/'; // Base URL for images
  private http = inject(HttpClient);
  //Categories: Categories[] = [];

  /**
   * fetches the main categories from the API.
   */
  // getMainCategories() {
  //   return this.http.get<Categories[]>(this.baseUrl + 'categories/main');
  // }

  getMainCategoriesFiltered(type: string): Observable<Categories[]> {
  const params = new HttpParams()
    .set('includeInactive', false)
    .set('type', type.toLowerCase()); // osiguraj lowercase

  return this.http.get<Categories[]>(`${this.baseUrl}categories/main`, { params });
}


  /**
   * fetches categories from the API by slug.
   * @param slug - The slug of the category to fetch.
   */
  getCategoryBySlug(slug: string) {
    return this.http.get<Categories>(`${this.baseUrl}categories/slug/${slug}`);
  }


  getSubcategoriesByParentId(parentId: string) {
    return this.http.get<Categories[]>(
      `${this.baseUrl}categories/${parentId}/subcategories`
    );
  }

   getSubcategoriesByParentSlug(parentSlug: string, includeInactive = false) {
    return this.http.get<Categories[]>(
      `${this.baseUrl}categories/slug/${parentSlug}/subcategories?includeInactive=${includeInactive}`
    );
  }
  getAllCategories(includeInactive: boolean = false): Observable<Categories[]> {
  const params = new HttpParams().set('includeInactive', includeInactive.toString());

  return this.http.get<Categories[]>(
    `${this.baseUrl}categories`,
    { params }
  );
}



}
