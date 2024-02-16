import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginRegisterComponent } from './Components/login/login-register/login-register.component';
import { NavbarComponent } from './Components/Navbar/navbar/navbar.component';
import { HomeComponent } from './Components/Navbar/Home/home/home.component';
import { BuyComponent } from './Components/Navbar/BuyCar/buy/buy.component';
import { RentComponent } from './Components/Navbar/RentCar/rent/rent.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CartComponent } from './Components/Navbar/AddToCart/cart/cart.component';
import { NotFoundComponent } from './Components/NotFound/not-found/not-found.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from  '@angular/common/http';
import { MoreDetailsComponent } from './Components/Navbar/more-details/more-details.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { TokenAuthInterceptor } from './Authentication/token-auth.interceptor';
import { AddCarsComponent } from './Components/Navbar/add-cars/add-cars.component';
import { ForgotPasswordComponent } from './Components/forgot-password/forgot-password.component';
import { ChangePasswordComponent } from './Components/change-password/change-password.component';
import { CountdownModule } from 'ngx-countdown';
import { ToastrModule } from 'ngx-toastr';
import { provideAnimations } from '@angular/platform-browser/animations';

import { provideToastr } from 'ngx-toastr';
@NgModule({
  declarations: [
    AppComponent,
    LoginRegisterComponent,
    NavbarComponent,
    HomeComponent,
    BuyComponent,
    RentComponent,
    CartComponent,
    NotFoundComponent,
    MoreDetailsComponent,
    AddCarsComponent,
    ForgotPasswordComponent,
    ChangePasswordComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatInputModule,
    MatSlideToggleModule,
    CountdownModule,
    ToastrModule.forRoot(),
  ],
  //for JWT Token
  providers: [
    { 
      provide: HTTP_INTERCEPTORS, useClass: TokenAuthInterceptor, multi:true
    },
    provideAnimations(), 
    provideToastr(),
   
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
