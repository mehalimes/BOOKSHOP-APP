import { Component, Input } from '@angular/core';
import { SharedStateService } from '../../services/shared-state.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product',
  imports: [],
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

  constructor(
    private sharedState: SharedStateService,
    private router: Router
  ) { }

  navigateToProductPage() {
    this.router.navigate(["/productPage"]);
    this.sharedState.productPageId = this.id;
    sessionStorage.setItem("productId", this.id.toString());
  }
}
