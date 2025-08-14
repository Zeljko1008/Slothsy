import { Component, OnInit } from '@angular/core';
import { OrdersService } from '../../../core/services/orders.service';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [],
  templateUrl: './order.component.html',
  styleUrl: './order.component.scss'
})
export class OrderComponent implements OnInit {

  ngOnInit(): void {
    this.fetchOrders();
  }

  constructor(private ordersService: OrdersService) {}

  fetchOrders() {
    this.ordersService.getOrders().subscribe({
      next: (orders) => {
        console.log('Orders fetched successfully:', orders);
      },
      error: (err) => {
        console.error('Error fetching orders:', err);
      }
    });
  }

}
