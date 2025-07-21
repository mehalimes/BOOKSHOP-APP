import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SharedStateService } from '../../services/shared-state.service';
import { lastValueFrom } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(
    private router: Router,
    private http: HttpClient,
    public sharedState: SharedStateService
  ) {
    this.sharedState.email = "";
  }

  async login() {
    try {
      let response: any = await lastValueFrom(
        this.http.post<string>(
          "https://localhost:5001/login",
          { Email: this.sharedState.email, Password: this.sharedState.password },
          { responseType: 'text' as 'json' }
        )
      );
      response = JSON.parse(response);
      localStorage.setItem("username", response.username);
      localStorage.setItem("email", response.email);
      window.alert("Login Successfull");
      this.router.navigate(["/"]);
    }
    catch (err) {
      localStorage.removeItem("email");
      window.alert("Login Unsuccessfull");
      this.sharedState.email = "";
      this.sharedState.password = "";
      console.log(err);
    }
  }
}
