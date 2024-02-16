import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginRegisterComponent } from './Components/login/login-register/login-register.component';
import { HomeComponent } from './Components/Navbar/Home/home/home.component';
import { NavbarComponent } from './Components/Navbar/navbar/navbar.component';
import { BuyComponent } from './Components/Navbar/BuyCar/buy/buy.component';
import { RentComponent } from './Components/Navbar/RentCar/rent/rent.component';
import { CartComponent } from './Components/Navbar/AddToCart/cart/cart.component';
import { NotFoundComponent } from './Components/NotFound/not-found/not-found.component';
import { MoreDetailsComponent } from './Components/Navbar/more-details/more-details.component';
import { AddCarsComponent } from './Components/Navbar/add-cars/add-cars.component';
import { ForgotPasswordComponent } from './Components/forgot-password/forgot-password.component';
import { ChangePasswordComponent } from './Components/change-password/change-password.component';

const routes: Routes = [
  {path:'',component:LoginRegisterComponent},
  {path:'navbar',component:NavbarComponent,
children:[
  {path:'',redirectTo:'home',pathMatch:'full'},
  {path:'home',component:HomeComponent,children:[
    {path:'moredetails/:id',component:MoreDetailsComponent}
  ]},
  {path:'buy',component:BuyComponent},
  {path:'rent',component:RentComponent},
  {path:'cart',component:CartComponent},
  {path:'addCar',component:AddCarsComponent}

]},
  {path:'passwordReset',component:ForgotPasswordComponent},
  {path:'changePassword/:id',component:ChangePasswordComponent},
//  {path:'**',component:NotFoundComponent}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
