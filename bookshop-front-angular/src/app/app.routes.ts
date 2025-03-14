import { Routes } from '@angular/router';
import { HomeComponent } from '../pages/home/home.component';
import { LoginComponent } from '../pages/login/login.component';
import { RegisterComponent } from '../pages/register/register.component';
import { CartComponent } from '../pages/cart/cart.component';
import { OrdersComponent } from '../pages/orders/orders.component';
import { PaymentComponent } from '../pages/payment/payment.component';
import { ProductPageComponent } from '../pages/product-page/product-page.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'productPage', component: ProductPageComponent },
    { path: 'cart', component: CartComponent },
    { path: 'orders', component: OrdersComponent },
    { path: 'payment', component: PaymentComponent }
];
