import { Component } from '@angular/core';
import { SharedStateService } from '../../services/shared-state.service';
import { CommonModule } from '@angular/common';
import { ProductComponent } from "../../components/product/product.component";
import { Book } from '../../models/Book';
import { lastValueFrom } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-delete-book',
  imports: [CommonModule, ProductComponent],
  templateUrl: './delete-book.component.html',
  styleUrl: './delete-book.component.css'
})
export class DeleteBookComponent {
  allBooks: Book[] = [];

  constructor(
    public sharedState: SharedStateService,
    private http: HttpClient
  ) { }

  async ngOnInit(): Promise<void> {
    try {
      const allBooksResponse = await lastValueFrom(
        this.http.get<string>(
          "https://localhost:5001/getAllBooksToAdmin",
          { responseType: 'text' as 'json' }
        )
      );
      JSON.parse(allBooksResponse).map((item: any) => {
        this.allBooks.push(new Book(item));
      });
    } catch (err) {
      console.log(err);
    }
  }
}
