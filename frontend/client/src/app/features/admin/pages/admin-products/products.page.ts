import { CommonModule, NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AdminProductService } from '../../services/admin-product.service';
import { Product } from '../../../../shared/models/product';
import { PaginationResult } from '../../../../shared/models/pagination-result';
import { PaginationParams } from '../../../../shared/models/pagination-params';
import { AdminProductColorVariantsComponent } from '../admin-product-color-variants/admin-product-color-variants.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [NgFor,NgIf, AdminProductColorVariantsComponent, CommonModule, RouterModule],
  templateUrl: './products.page.html',
  styleUrl: './products.page.scss'
})
export class ProductsPage implements OnInit {

 products: Product[] = [];
 pagination: PaginationResult<Product> | null = null;
 expandedProductId: string | null = null;

  params: PaginationParams = {
    pageNumber: 1,
    pageSize: 15,
    includeInactive: true,
  };

  isLoading = false;



  constructor(private adminProductService: AdminProductService) {}
    // Initialization logic can go here

  ngOnInit(): void {
    this.loadProducts();
  }



  loadProducts(): void {
    this.isLoading = true;
   this.adminProductService.getAllProducts(this.params).subscribe({
  next: (res) => {
    this.pagination = res;
    this.products = res.items;
  }
});
  }
   onPageChange(page: number): void {
    this.params.pageNumber = page;
    this.loadProducts();
  }



toggleExpanded(productId: string): void {
  this.expandedProductId = this.expandedProductId === productId ? null : productId;
}

  viewColorVariants(){}
  editProduct(){}
  createProduct(){}
}
