import { Routes } from '@angular/router';
import { ProductDetailsComponent } from './features/products/pages/product-details/product-details.component';
import { AdminLayoutComponent } from './layout/admin-layout/admin-layout.component';
import { PublicLayoutComponent } from './layout/public-layout/public-layout.component';


export const routes: Routes =  [
  // USER (public) layout
  {
    path: '',
    component: PublicLayoutComponent,
    children: [
      {
        path: '',
        loadComponent: () => import('./features/home/home.component').then((m) => m.HomeComponent),
      },
      {
        path: 'shop/:slug',
        loadComponent: () =>
          import('./features/products/pages/category-page/category-page.component').then((m) => m.CategoryPageComponent),
      },
      {
        path: 'product/:slug',
        loadComponent: () =>
          import('./features/products/pages/product-details/product-details.component').then((m) => m.ProductDetailsComponent),
      },
      {
        path: 'product/details/:colorVariantSlug',
        loadComponent: () =>
          import('./features/products/pages/product-details/product-details.component').then((m) => m.ProductDetailsComponent),
      },
    ],
  },

  // ADMIN layout
  {
    path: 'admin',
    component: AdminLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () =>
          import('./features/admin/admin.routes').then((m) => m.adminRoutes),
      },
    ],
  },

  // fallback
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
