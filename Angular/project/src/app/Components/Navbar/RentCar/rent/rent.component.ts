import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { HttpService } from 'src/app/Services/http/http.service';

@Component({
  selector: 'app-rent',
  templateUrl: './rent.component.html',
  styleUrls: ['./rent.component.css']
})
export class RentComponent {

  constructor(private api:HttpService){}

  cars:any[]=[];
  private SubCars:Subscription | undefined;

  ngOnInit(){
    this.SubCars =  this.api.getCars().subscribe(
      (res)=>{
          this.cars = res;
      },
      (error)=>{
        console.log(error);
        
      }
    )
  }



  ngOnDestroy(){
    if(this.SubCars){
        this.SubCars.unsubscribe();
    }
  }

  
  addToCart(cars:any){

    let cart = {
      "name": cars.name,
      "contactDetails": cars.contactDetails,
      "price": cars.price,
      "category": cars.category
    }

    console.log(cart);
    

    this.api.postCart(cart).subscribe(
      (res)=>{
          console.log(res);
          alert("added successfully");
          
      },
      (error)=>{
          console.log(error);
          
      }
    )
  }


}
