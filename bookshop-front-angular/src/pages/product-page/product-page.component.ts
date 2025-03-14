import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { SharedStateService } from '../../services/shared-state.service';
import { Book } from '../home/home.component';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-product-page',
  imports: [CommonModule, RouterModule],
  templateUrl: './product-page.component.html',
  styleUrl: './product-page.component.css'
})
export class ProductPageComponent {
  constructor(
    private http: HttpClient,
    public sharedState: SharedStateService,
  ) {
    this.sharedState.email = localStorage.getItem("email") ?? "";
    this.sharedState.isAuthenticated = localStorage.getItem("isAuthenticated") == "true" ? true : false;
  }

  book?: Book = undefined;
  id: number = Number(sessionStorage.getItem("productId"));

  async ngOnInit() {
    try {
      const response = await lastValueFrom(
        this.http.post<string>(
          "https://localhost:7001/getBookById",
          { Id: this.id },
          { responseType: 'text' as 'json' }
        )
      );
      this.book = new Book(JSON.parse(response));
    }
    catch (err) {
      console.log(err);
    }
  }

  async signOut() {
    try {
      const response = await lastValueFrom(
        this.http.get<string>(
          "https://localhost:7001/signout",
          { responseType: 'text' as 'json' }
        )
      );
      localStorage.removeItem("email");
      localStorage.setItem("isAuthenticated", "false");
      this.sharedState.isAuthenticated = localStorage.getItem("isAuthenticated") == "true" ? true : false;
      this.sharedState.email = localStorage.getItem("email") ?? "";
      this.sharedState.password = "";
      console.log("User signed out.")
    }
    catch (err) {
      console.log("Could'nt sign out.");
    }
  }

  addToCart() {

  }
}
