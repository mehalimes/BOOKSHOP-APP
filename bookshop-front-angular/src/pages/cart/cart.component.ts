import { Component } from '@angular/core';
import { CartItemComponent } from "../../components/cart-item/cart-item.component";
import { SharedStateService } from '../../services/shared-state.service';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FunctionsService } from '../../services/functions.service';

@Component({
  selector: 'app-cart',
  imports: [CartItemComponent, CommonModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  constructor(
    public sharedState: SharedStateService,
    private functions: FunctionsService
  ) { }

  async ngOnInit() {
    await this.functions.updateCart(this.sharedState.email);
  }

  async makePayment() {

  }
}
