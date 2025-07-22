import { Component, Input, OnInit } from '@angular/core';
import { ProductVariant } from '../../../../shared/models/product-variant';
import { AdminProductService } from '../../services/admin-product.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-product-variant',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin-product-variant.component.html',
  styleUrl: './admin-product-variant.component.scss'
})
export class AdminProductVariantComponent implements OnInit{

  @Input() productColorVariantId!: string;
  productVariant:ProductVariant[] = [];
  loading = false;


  constructor(private adminProductService : AdminProductService) {}

  ngOnInit(): void {
    this.loading = true
    this.loadProductVariant();
  }

  loadProductVariant(): void {
    this.loading = false;
    this.adminProductService.getProductVariant(this.productColorVariantId).subscribe({
      next: (variants) => (this.productVariant = variants),
    error: (err) => console.error(err),
  });
  }

}
