import { Component } from '@angular/core';
import { HttpService } from 'src/app/Services/http/http.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(private api :HttpService){

  }

  Cars:any[]=[];

  ngOnInit(){
      this.api.getCars().subscribe(
        (response) =>{
          console.log("get the cars data");
          this.Cars = response;
        },
        (error)=>{
          console.log("error while getting cars data");
          console.log(error);
        }
      )
  }

}
