import { Component, Input } from '@angular/core';
import { Book } from '../../models/Book';

@Component({
  selector: 'app-order-item',
  imports: [],
  templateUrl: './order-item.component.html',
  styleUrl: './order-item.component.css'
})
export class OrderItemComponent {
  @Input() id: number = 0;
  @Input() book: Book | null = null;
  @Input() quantity: number = 0;
}
