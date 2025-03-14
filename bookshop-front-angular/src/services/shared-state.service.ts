import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SharedStateService {
  email: string = "";
  password: string = "";
  productPageId: number = 0;
  isAuthenticated: boolean = localStorage.getItem("isAuthenticated") == "true" ? true : false;
}
