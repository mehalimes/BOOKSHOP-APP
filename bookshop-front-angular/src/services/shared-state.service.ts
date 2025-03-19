import { Injectable } from '@angular/core';
import { BookComment } from '../models/BookComment';

@Injectable({
  providedIn: 'root'
})
export class SharedStateService {
  email: string = localStorage.getItem("email") ?? "";
  password: string = "";
  productPageId: number = 0;
  isAuthenticated: boolean = localStorage.getItem("isAuthenticated") == "true" ? true : false;
  allComments: BookComment[] = [];
}
