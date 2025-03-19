import { Component } from '@angular/core';
import { CartItemComponent } from "../../components/cart-item/cart-item.component";
import { CartItem } from '../../models/CartItem';
import { lastValueFrom } from 'rxjs';
import { SharedStateService } from '../../services/shared-state.service';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart',
  imports: [CartItemComponent, CommonModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  id: number = 0;
  cartItems: CartItem[] = [];
  totalPrice: number = 0;

  constructor(
    public sharedState: SharedStateService,
    private http: HttpClient
  ) { }

  async updateCart() {
    let getCartResponse = await lastValueFrom(
      this.http.post<string>(
        "https://localhost:7001/getCartOfAUser",
        { Email: this.sharedState.email },
        { responseType: 'text' as 'json' }
      )
    );
    let cart = JSON.parse(getCartResponse);
    this.id = cart.id;
    this.totalPrice = cart.totalPrice;
    this.cartItems = [];
    cart.cartItems.map((item: any) => {
      this.cartItems.push(new CartItem(item.id, item.quantity, item.book));
    });
  }

  async ngOnInit() {
    this.updateCart();
  }

  async makePayment() {

  }
}
