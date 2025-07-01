import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { PaginationResult } from '../../shared/models/pagination-result';
import { Product } from '../../shared/models/product';
import { Categories } from '../../shared/models/categories';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
   baseUrl= 'https://localhost:7053/api/';
    private http = inject(HttpClient);

   getProducts(){

    return this.http.get<PaginationResult<Product>>(this.baseUrl + 'products')
   }


}
