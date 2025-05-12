import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { SharedStateService } from '../../services/shared-state.service';
import { lastValueFrom } from 'rxjs';
import { Order } from '../../models/Order';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-orders',
  imports: [CommonModule],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})
export class OrdersComponent {

  constructor(
    private http: HttpClient,
    public sharedState: SharedStateService
  ) { }

  async ngOnInit() {
    try {
      const getAllOrdersResponse = await lastValueFrom(
        this.http.post<string>(
          "https://localhost:5001/getAllOrders",
          { Email: this.sharedState.email },
          { responseType: 'text' as 'json' }
        )
      );
      JSON.parse(getAllOrdersResponse).map((order: any) => {
        this.sharedState.orders.push(new Order(order));
      });
      console.log(this.sharedState.orders);
    }
    catch (err) {
      console.log(err);
    }
  }

  async refund(orderId: number) {
    try {
      const refundResponse = await lastValueFrom(
        this.http.post<string>(
          "https://localhost:5001/cancelOrder",
          { OrderId: orderId },
          { responseType: 'text' as 'json' }
        )
      );
      console.log(refundResponse);
      window.alert("Geri İade Başarılı.");
      location.reload();
    }
    catch (err) {
      console.log(err);
    }
  }
}