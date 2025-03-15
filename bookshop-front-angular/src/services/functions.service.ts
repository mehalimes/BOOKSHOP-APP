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
}
