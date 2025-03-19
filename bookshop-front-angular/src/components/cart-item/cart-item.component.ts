import { HttpClient } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { SharedStateService } from '../../services/shared-state.service';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-cart-item',
  imports: [],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.css'
})
export class CartItemComponent {
  @Input() id: number = 0;
  @Input() isbn_13: string = "";
  @Input() publicId: string = "";
  @Input() author: string = "";
  @Input() price: number = 0;
  @Input() title: string = "";
  @Input() subTitle?: string = "";
  @Input() quantity: number = 0;

  constructor(private http: HttpClient, private sharedState: SharedStateService) { }

  async removeCartItem() {
    let response = await lastValueFrom(this.http.post<string>(
      "https://localhost:7001/removeCartItem",
      { "CartItemId": this.id, "Email": this.sharedState.email },
      { responseType: 'text' as 'json' }
    ));
    console.log(response);
  }
}
