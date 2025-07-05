import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { SharedStateService } from '../../services/shared-state.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-button',
  imports: [],
  templateUrl: './button.component.html',
  styleUrl: './button.component.css'
})
export class ButtonComponent {
  @Input() buttonName: string = "";
  @Input() event!: () => Promise<void>;

  constructor(
    private router: Router,
    private http: HttpClient,
    private sharedState: SharedStateService
  ) {

  }
}
