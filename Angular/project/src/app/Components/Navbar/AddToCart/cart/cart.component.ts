import { Component } from '@angular/core';
import { HttpService } from 'src/app/Services/http/http.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {

  constructor(private api:HttpService){}


    allCarts:any[]=[];
    ngOnInit(){
        this.api.getCarts().subscribe(
          (res)=>{
            this.allCarts = res;
          },
          (error)=>{
            console.log(error);
            
          }
        )
    }


    Remove(id:any){
      this.api.deleteCartById(id).subscribe(
        (res)=>{
          console.log("successfully removed");
          console.log(res);
          
          window.location.reload();
        },
        (error)=>{
          console.log(error);
          
        })
    }



}
