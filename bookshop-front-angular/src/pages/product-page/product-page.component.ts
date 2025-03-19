import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { SharedStateService } from '../../services/shared-state.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Book } from '../../models/Book';
import { CommentComponent } from "../../components/comment/comment.component";
import { FunctionsService } from '../../services/functions.service';

@Component({
  selector: 'app-product-page',
  imports: [CommonModule, RouterModule, FormsModule, CommentComponent],
  templateUrl: './product-page.component.html',
  styleUrl: './product-page.component.css'
})
export class ProductPageComponent {
  constructor(
    private http: HttpClient,
    public sharedState: SharedStateService,
    public functions: FunctionsService
  ) {
    this.sharedState.email = localStorage.getItem("email") ?? "";
    this.sharedState.isAuthenticated = localStorage.getItem("isAuthenticated") == "true" ? true : false;
  }

  book?: Book = undefined;
  id: number = Number(sessionStorage.getItem("productId"));
  commentTextArea: string = "";

  async ngOnInit() {
    try {
      const getBookByIdResponse = await lastValueFrom(
        this.http.post<string>(
          "https://localhost:7001/getBookById",
          { Id: this.id },
          { responseType: 'text' as 'json' }
        )
      );

      this.book = new Book(JSON.parse(getBookByIdResponse));
      this.sharedState.allComments = [];
      const serviceResponse = await this.functions.updateAllComments(this.id);
      this.sharedState.allComments = serviceResponse;
      console.log(serviceResponse)
      console.log(this.sharedState.allComments)
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
      this.sharedState.allComments = await this.functions.updateAllComments(this.id);
      console.log("User signed out.")
    }
    catch (err) {
      console.log("Could'nt sign out.");
    }
  }

  addToCart() {

  }

  async addComment() {
    try {
      const response = await lastValueFrom(
        this.http.post<string>(
          "https://localhost:7001/addCommentToABook",
          { email: this.sharedState.email, bookId: this.book?.id, content: this.commentTextArea },
          { responseType: 'text' as 'json' }
        )
      );
      console.log(response);
      this.sharedState.allComments = [];
      this.commentTextArea = "";
      const serviceResponse = (await this.functions.updateAllComments(this.id));
      this.sharedState.allComments = serviceResponse;
    }
    catch (err) {
      console.log(err);
    }
  }
}
