import { Component, Input } from '@angular/core';
import { SharedStateService } from '../../services/shared-state.service';
import { Router } from '@angular/router';
import { FunctionsService } from '../../services/functions.service';
import { CommonModule } from '@angular/common';
import { combineLatestWith, lastValueFrom } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-product',
  imports: [CommonModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {
  @Input() id: number = 0;
  @Input() isbn_13: string = "";
  @Input() publicId: string = "";
  @Input() author: string = "";
  @Input() price: number = 0;
  @Input() title: string = "";
  @Input() subTitle?: string = "";
  @Input() isInAdmin?: boolean = false;

  constructor(
    public sharedState: SharedStateService,
    private router: Router,
    public functions: FunctionsService,
    private http: HttpClient
  ) { }

  navigateToProductPage() {
    this.router.navigate(["/productPage"]);
    this.sharedState.productPageId = this.id;
    sessionStorage.setItem("productId", this.id.toString());
  }

  async deleteBook(event: any): Promise<void> {
    event.stopPropagation();
    try {
      const deleteBookResponse = await lastValueFrom(
        this.http.post<string>(
          "https://localhost:5001/removeBook",
          { Id: this.id },
          { responseType: 'text' as 'json' }
        )
      );
      console.warn(deleteBookResponse);
      window.alert("Kitap silme işlemi başarılı.");
      location.reload();
    }
    catch (err) {
      console.warn(err);
      window.alert("Kitap silme işlemi başarısız.");
    }
  }
}
