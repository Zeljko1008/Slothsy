import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  baseUrl = environment.apiBaseUrl;

  constructor( private http : HttpClient) { }

  getOrders() {
   return this.http.get(`${this.baseUrl}orders/protected`);
}


}
