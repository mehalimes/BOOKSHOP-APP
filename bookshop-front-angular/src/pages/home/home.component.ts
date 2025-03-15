import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { ProductComponent } from "../../components/product/product.component";
import { SharedStateService } from '../../services/shared-state.service';
import { Book } from '../../models/Book';

@Component({
  selector: 'app-home',
  imports: [CommonModule, ProductComponent, RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  allBooks: Book[] = [];

  constructor(
    public sharedState: SharedStateService,
    private http: HttpClient
  ) {
    this.sharedState.email = localStorage.getItem("email") ?? "";
    this.sharedState.isAuthenticated = localStorage.getItem("isAuthenticated") == "true" ? true : false;
  }

  async ngOnInit() {
    try {
      const response = await lastValueFrom(
        this.http.get<string>(
          "https://localhost:7001/getAllBooks",
          { responseType: 'text' as 'json' }
        )
      );
      JSON.parse(response).map((item: any) => {
        this.allBooks.push(new Book(item));
      });
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
}
