import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { SharedStateService } from '../../services/shared-state.service';
import { lastValueFrom } from 'rxjs';
import { Order } from '../../models/Order';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin',
  imports: [CommonModule],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent {

  totalRevenue: number = 0;

  constructor(
    private http: HttpClient,
    public sharedState: SharedStateService,

  ) { }

  async ngOnInit() {
    try {
      this.sharedState.adminOrders = [];
      const getAllOrdersResponse = await lastValueFrom(
        this.http.get<string>(
          "https://localhost:5001/getOrdersOfAllUsers",
          { responseType: 'text' as 'json' }
        )
      );
      JSON.parse(getAllOrdersResponse).map((order: any) => {
        this.sharedState.adminOrders.push(new Order(order));
      });
      console.log(this.sharedState.adminOrders);
    }
    catch (err) {
      console.log("ERROR");
    }
  }
}
