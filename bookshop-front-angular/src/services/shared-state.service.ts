import { Injectable } from '@angular/core';
import { Comment } from '../models/Comment';
import { Cart } from '../models/Cart';

@Injectable({
  providedIn: 'root'
})
export class SharedStateService {
  email: string = localStorage.getItem("email") ?? "";
  password: string = "";
  productPageId: number = 0;
  isAuthenticated: boolean = localStorage.getItem("isAuthenticated") == "true" ? true : false;
  allComments: Comment[] = [];
  cart: Cart = new Cart(-1, -1, []);
}
