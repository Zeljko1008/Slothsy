// src/app/features/admin/admin.routes.ts
import { Routes } from '@angular/router';
import { ProductsPage } from './pages/admin-products/products.page';
import { AdminAddProductComponent } from './pages/admin-add-product/admin-add-product.component';
//import { CategoriesPage } from './pages/categories.page';

export const adminRoutes: Routes =  [
  {
    path: 'admin-products',
    component: ProductsPage,
  },
  {
    path: '',
    redirectTo: 'admin-products',
    pathMatch: 'full',
  },
  {
    path:'admin-add-product',
    loadComponent: () => import('./pages/admin-add-product/admin-add-product.component').then(m => m.AdminAddProductComponent)
  }
];
