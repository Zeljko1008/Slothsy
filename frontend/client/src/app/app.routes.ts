import { Routes } from '@angular/router';
import { ProductDetailsComponent } from './features/shop/product-details/product-details.component';
import { HomeComponent } from './features/home/home.component';
import { CategoryViewComponent } from './features/products/pages/category-view/category-view.component';
import { CategoryPageComponent } from './features/products/pages/category-page/category-page.component';

export const routes: Routes = [
  {path: '', component: HomeComponent},
   {path: 'shop/:slug',component: CategoryPageComponent },

  {path: '**', redirectTo: '', pathMatch: 'full'},


];
