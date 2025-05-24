import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-add-book',
  imports: [FormsModule],
  templateUrl: './add-book.component.html',
  styleUrl: './add-book.component.css'
})
export class AddBookComponent {
  ISBN_13: string = "";
  PublicId: string = "";
  Author: string = "";
  Price: string = "";
  Title: string = "";
  SubTitle: string | null = "";

  constructor(
    private http: HttpClient,
    private router: Router
  ) { }

  async addBook(): Promise<void> {
    try {
      const addBookResponse = await lastValueFrom(
        this.http.post<string>(
          "https://localhost:5001/addBookToDb",
          {
            ISBN_13: this.ISBN_13,
            PublicId: this.PublicId,
            Author: this.Author,
            Price: Number(this.Price),
            Title: this.Title,
            SubTitle: this.SubTitle
          },
          { responseType: 'text' as 'json' }
        )
      );
      console.log(addBookResponse);
      window.alert("Kitap başarıyla eklendi.");
      this.router.navigate(["/"]);
    }
    catch (err) {
      console.warn(err);
      window.alert("Kitap ekleme işlemi başarısız.");
    }
  }
}
