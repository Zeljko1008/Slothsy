import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from '../../shared/models/login-request';
import { Observable, tap } from 'rxjs';
import { LoginResponse } from '../../shared/models/login-response';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl= environment.apiBaseUrl;

  constructor(private http:HttpClient) { }

  login(data:LoginRequest): Observable<LoginResponse>{
    return this.http.post<LoginResponse>(`${this.baseUrl}Auth/Login`, data).pipe(tap((response) =>{
      localStorage.setItem('accessToken', response.accessToken);
      localStorage.setItem('refreshToken', response.refreshToken);
    }))
  }

  logout() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  }
  getAccessToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  isLoggedIn(): boolean {
    return !!this.getAccessToken();
  }
}
