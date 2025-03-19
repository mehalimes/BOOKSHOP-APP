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
  ) { }

  async login() {
    try {
      const response = await lastValueFrom(
        this.http.post<string>(
          "https://localhost:7001/login",
          { Email: this.sharedState.email, Password: this.sharedState.password },
          { responseType: 'text' as 'json' }
        )
      );
      localStorage.setItem("isAuthenticated", "true");
      localStorage.setItem("email", this.sharedState.email.toString());
      this.router.navigate(["/"]);
    }
    catch (err) {
      localStorage.setItem("isAuthenticated", "false");
      localStorage.removeItem("email");
      window.alert("Login unsuccessfull.");
      this.sharedState.email = "";
      this.sharedState.password = "";
      console.log(err);
    }
  }
}
