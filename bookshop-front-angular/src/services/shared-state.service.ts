import { Injectable } from '@angular/core';
import { Comment } from '../models/Comment';
import { Cart } from '../models/Cart';
import { Order } from '../models/Order';

@Injectable({
  providedIn: 'root'
})
export class SharedStateService {
  username: string = localStorage.getItem("username") ?? "";
  email: string = localStorage.getItem("email") ?? "";
  password: string = "";

  adminEmail: string = "";
  adminPassword: string = "";
  adminUsername: string = "";

  isAuthenticated: boolean = false;

  productPageId: number = 0;
  allComments: Comment[] = [];

  cart: Cart = new Cart(-1, -1, []);

  orders: Order[] = [];
  adminOrders: Order[] = [];
}
