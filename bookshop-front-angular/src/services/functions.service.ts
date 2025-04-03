import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Comment } from '../models/Comment';
import { Cart } from '../models/Cart';
import { CartItem } from '../models/CartItem';
import { SharedStateService } from './shared-state.service';

@Injectable({
  providedIn: 'root'
})
export class FunctionsService {

  constructor(private http: HttpClient, private sharedState: SharedStateService) { }

  async updateAllComments(bookId: number): Promise<Comment[]> {
    const getAllCommentsResponse = await lastValueFrom(
      this.http.post<string>(
        "https://localhost:7001/getAllCommentsOfABook",
        { bookId: bookId },
        { responseType: 'text' as 'json' }
      )
    );
    console.log(JSON.parse(getAllCommentsResponse));
    return JSON.parse(getAllCommentsResponse);
  }

  async addToCart(bookId: number, email: string, event: Event) {
    try {
      event.stopPropagation();
      const addToCartResponse = await lastValueFrom(
        this.http.post<string>(
          "https://localhost:7001/addToCart",
          { BookId: bookId, Email: email },
          { responseType: 'text' as 'json' }
        )
      );
      console.log(addToCartResponse);
      window.alert("Sepete eklendi.");
    }
    catch (err) {
      console.log(err);
      window.alert("Sepete eklenemedi.");
    }
  }

  async updateCart(email: string): Promise<void> {
    let getCartResponse = await lastValueFrom(
      this.http.post<string>(
        "https://localhost:7001/getCartOfAUser",
        { Email: email },
        { responseType: 'text' as 'json' }
      )
    );
    let cart = JSON.parse(getCartResponse);
    let id = cart.id;
    let totalPrice = cart.totalPrice;
    let cartItems: CartItem[] = [];
    cart.cartItems.map((item: any) => {
      cartItems.push(new CartItem(item.id, item.quantity, item.book));
    });
    this.sharedState.cart = new Cart(id, totalPrice, cartItems);
  }
}
