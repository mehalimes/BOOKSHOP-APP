import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { SharedStateService } from '../../services/shared-state.service';
import { lastValueFrom } from 'rxjs';
import { Order } from '../../models/Order';
import { OrderItem } from '../../models/OrderItem';

@Component({
  selector: 'app-orders',
  imports: [],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})
export class OrdersComponent {

  constructor(
    private http: HttpClient,
    private sharedState: SharedStateService
  ) { }

  async ngOnInit() {
    try {
      const getAllOrdersResponse = await lastValueFrom(
        this.http.post<string>(
          "https://localhost:7001/getAllOrders",
          { Email: this.sharedState.email },
          { responseType: 'text' as 'json' }
        )
      );
      JSON.parse(getAllOrdersResponse).map((order: any) => {
        this.sharedState.orders.push(new Order(order));
      });
      this.sharedState.orders.map((order: any) => {
        order.items.map((orderItem: any) => {
          console.log(orderItem);
        })
      })
    }
    catch (err) {
      console.log(err);
    }
  }
}