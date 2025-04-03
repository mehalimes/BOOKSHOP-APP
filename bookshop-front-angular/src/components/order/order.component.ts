import { Component, Input } from '@angular/core';
import { OrderItem } from '../../models/OrderItem';

@Component({
  selector: 'app-order',
  imports: [],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent {
  @Input() id: number = 0;
  @Input() items: OrderItem[] = [];
  @Input() totalPrice: number = 0;
  @Input() address: string = "";
  @Input() country: string = "";
  @Input() city: string = "";
  @Input() paymentId: string = "";
}