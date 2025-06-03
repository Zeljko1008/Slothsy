import { HttpClient } from '@angular/common/http';
import { inject, Injectable, OnInit } from '@angular/core';
import { Categories } from '../../shared/models/categories';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

 baseUrl= 'https://localhost:7053/api/';
      private http = inject(HttpClient);





      getMainCategories() {
        return this.http.get<Categories[]>(this.baseUrl + 'categories/main')
      }


}


