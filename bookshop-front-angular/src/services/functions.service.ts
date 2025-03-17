import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { BookComment } from '../models/BookComment';

@Injectable({
  providedIn: 'root'
})
export class FunctionsService {
  constructor(private http: HttpClient) { }

  async updateAllComments(bookId: number): Promise<BookComment[]> {
    const getAllCommentsResponse = await lastValueFrom(
      this.http.post<string>(
        "https://localhost:7001/getAllCommentsOfABook",
        { bookId: bookId },
        { responseType: 'text' as 'json' }
      )
    );
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
      window.alert("Added to cart successfully.");
    }
    catch (err) {
      console.log(err);
      window.alert("Could not add to cart.");
    }
  }
}
