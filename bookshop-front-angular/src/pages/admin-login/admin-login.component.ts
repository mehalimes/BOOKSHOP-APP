import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SharedStateService } from '../../services/shared-state.service';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-login',
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-login.component.html',
  styleUrl: './admin-login.component.css'
})
export class AdminLoginComponent {

  constructor(
    public sharedState: SharedStateService,
    private http: HttpClient,
    private router: Router
  ) { }

  async adminLogin(): Promise<void> {
    try {
      let adminLoginResponse: any = await lastValueFrom(
        this.http.post<string>(
          "https://localhost:5001/adminLogin",
          { Email: this.sharedState.adminEmail, Password: this.sharedState.adminPassword },
          { responseType: 'text' as 'json' }
        )
      );
      window.alert("Admin Giriş Başarılı");

      localStorage.removeItem("email");
      localStorage.removeItem("username");

      localStorage.setItem("isAuthenticated", "true");

      localStorage.setItem("adminEmail", this.sharedState.adminEmail.toString());
      localStorage.setItem("adminUsername", this.sharedState.adminUsername.toString());

      this.router.navigate(["/admin"]);
    }
    catch (err) {
      window.alert("Admin Giriş Başarısız.");
    }
  }
}
