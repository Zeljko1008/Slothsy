import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Product } from './shared/models/product';
import { PaginationResult } from './shared/models/paginationResult';
import { ShopService } from './core/services/shop.service';
import { HomeComponent } from './features/home/home.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, CommonModule, HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {

  private shopService = inject(ShopService)
  title = 'Slothsy';
  products: Product[] = [];

  ngOnInit(): void{

    this.shopService.getProducts().subscribe({
      next: response => this.products = response.items,

      error: error => console.error('Error fetching products:', error),
      complete: () =>console.log(this.products)
      }
    );
  }
}
