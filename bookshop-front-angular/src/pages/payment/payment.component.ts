import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { lastValueFrom } from 'rxjs';
import { SharedStateService } from '../../services/shared-state.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment',
  imports: [FormsModule],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent {
  name: string = "";
  surname: string = "";

  expireMonth: string = "";
  expireYear: string = "";
  cardNumber: string = "";
  cardHolderName: string = "";
  cvc: string = "";

  address: string = "";
  country: string = "";
  city: string = "";

  constructor(
    private http: HttpClient,
    private sharedState: SharedStateService,
    private router: Router
  ) { }

  async confirmPayment() {
    try {
      const paymentResponse = await lastValueFrom(
        this.http.post<string>(
          "https://localhost:5001/createOrder",
          {
            Email: this.sharedState.email,
            ExpireMonth: this.expireMonth,
            ExpireYear: this.expireYear,
            CardNumber: this.cardNumber,
            Name: this.name,
            Surname: this.surname,
            CardHolderName: this.cardHolderName,
            CVC: this.cvc,
            AddressDescription: this.address,
            Country: this.country,
            City: this.city
          },
          { responseType: 'text' as 'json' }
        )
      );
      window.alert("Ödeme işlemi başarılı.");
      this.router.navigate(["/"]);
    }
    catch (err) {
      console.log(err);
    }
  }
}
