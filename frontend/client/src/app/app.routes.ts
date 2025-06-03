import { Routes } from '@angular/router';
import { ProductDetailsComponent } from './features/shop/product-details/product-details.component';
import { HomeComponent } from './features/home/home.component';

export const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'shop/:id', component: ProductDetailsComponent},
  {path: '**', redirectTo: '', pathMatch: 'full'},


];
