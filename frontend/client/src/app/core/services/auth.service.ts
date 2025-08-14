import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { LoginRequest } from '../../shared/models/login-request';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { LoginResponse } from '../../shared/models/login-response';
import { Register } from '../../shared/models/register';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl = environment.apiBaseUrl;

  private loginStatus = new BehaviorSubject<boolean>(false);

  loginStatus$ = this.loginStatus.asObservable();

  private currentUserName = new BehaviorSubject<string | null>(null);
  currentUserName$ = this.currentUserName.asObservable();

  private roles = new BehaviorSubject<string[]>([]);
roles$ = this.roles.asObservable();

  constructor(private http: HttpClient) {
    const token = localStorage.getItem('accessToken');
    if (token) this.loginStatus.next(true);
    const userName = localStorage.getItem('currentUserName');
    if (userName) this.currentUserName.next(userName);
    const roles = localStorage.getItem('roles');
  if (roles) this.roles.next(JSON.parse(roles));
  }

  login(model: LoginRequest): Observable<LoginResponse> {
    return this.http
      .post<LoginResponse>(this.baseUrl + 'account/login', model)
      .pipe(
        tap((response) => {
          localStorage.setItem('accessToken', response.accessToken);
          localStorage.setItem('refreshToken', response.refreshToken);
          localStorage.setItem('currentUserName', response.firstName || '');
            localStorage.setItem('roles', JSON.stringify(response.roles || []));
          this.loginStatus.next(true);
          this.currentUserName.next(response.firstName || null);
          this.roles.next(response.roles || []);
        })
      );
  }

  logout() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    this.loginStatus.next(false);
    this.currentUserName.next(null);
  }
  getAccessToken(): string | null {
    return localStorage.getItem('accessToken');
  }
  getRefreshToken(): string | null {
    return localStorage.getItem('refreshToken');
  }
  setTokens(accessToken: string, refreshToken: string) {
    localStorage.setItem('accessToken', accessToken);
    localStorage.setItem('refreshToken', refreshToken);
  }

 isLoggedIn(): boolean {
  const token = this.getAccessToken();
  if (!token) return false;

  try {
    const payload = JSON.parse(atob(token.split('.')[1]));
    const expiry = payload.exp * 1000;
    return Date.now() < expiry;
  } catch {
    return false;
  }
}
  register(model: Register) {
    return this.http.post(this.baseUrl + 'account/register', model);
  }

refreshToken(): Observable<any> {
  const refresh = this.getRefreshToken();
  if (!refresh) {
    this.logout();
    return throwError(() => 'No refresh token');
  }

  return this.http.post(this.baseUrl + 'account/refresh-token', { refreshToken: refresh })
    .pipe(
      tap((res: any) => {
        this.setTokens(res.accessToken, res.refreshToken);
        this.loginStatus.next(true);
      }),
      catchError(err => {
        this.logout();
        return throwError(() => err);
      })
    );
}
getRoles(): string[] {
  const roles = localStorage.getItem('roles');
  return roles ? JSON.parse(roles) : [];
}
isAdmin(): boolean {
  return this.getRoles().includes('Admin');
}
}
