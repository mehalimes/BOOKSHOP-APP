import { Routes } from '@angular/router';
import { HomeComponent } from '../pages/home/home.component';
import { LoginComponent } from '../pages/login/login.component';
import { RegisterComponent } from '../pages/register/register.component';
import { CartComponent } from '../pages/cart/cart.component';
import { OrdersComponent } from '../pages/orders/orders.component';
import { PaymentComponent } from '../pages/payment/payment.component';
import { ProductPageComponent } from '../pages/product-page/product-page.component';
import { AdminLoginComponent } from '../pages/admin-login/admin-login.component';
import { AdminComponent } from '../pages/admin/admin.component';
import { AddBookComponent } from '../pages/add-book/add-book.component';
import { DeleteBookComponent } from '../pages/delete-book/delete-book.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'productPage', component: ProductPageComponent },
    { path: 'cart', component: CartComponent },
    { path: 'orders', component: OrdersComponent },
    { path: 'payment', component: PaymentComponent },
    { path: 'adminLogin', component: AdminLoginComponent },
    { path: 'admin', component: AdminComponent },
    { path: 'addBook', component: AddBookComponent },
    { path: 'deleteBook', component: DeleteBookComponent }
];
