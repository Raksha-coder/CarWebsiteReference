import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { HttpService } from 'src/app/Services/http/http.service';

@Component({
  selector: 'app-more-details',
  templateUrl: './more-details.component.html',
  styleUrls: ['./more-details.component.css']
})
export class MoreDetailsComponent {


  private routeid:Subscription | undefined;
  private carsall:Subscription | undefined;
  role = localStorage.getItem('role');

  //get by id
  constructor(private api:HttpService,private route:ActivatedRoute,private fb:FormBuilder){

  }

  carbyid:any={};  //getting single car details
  allcars:any[] =[]; //allcars
  clicked:boolean = true;
  //get id values
  editform!:FormGroup;

  ngOnInit(){
    //getting all car
       this.carsall =  this.api.getCars().subscribe(
        (response)=>{
          this.allcars = response;
        },
        (error)=>{
          console.log(error);
          
        }
      );


        //filtering the id from get all cars
    this.routeid = this.route.params.subscribe(params =>{
          //get the id from url
          console.log(params);
          console.log(params['id']);  //getting this value

          //now find from  all cars
          this.allcars.forEach((e:any) => {
            if(e.id === params['id']){
              // console.log(e.id);
              this.carbyid = e;
              console.log(this.carbyid);
              
           }
          });
          
          
        })




        this.editform = this.fb.group({
          name:['',Validators.required],
          price:['',Validators.required],
          contactDetails:['',Validators.required],
          category:['',Validators.required]
        })




  }



  //set the value of form
  edit(id:any){

    //form should automatically get filled
    this.api.getCarById(id).subscribe(
      (response) =>{
          this.editform = this.fb.group({
           name:[response.response.name],
           price:[response.response.price],
           contactDetails:[response.response.contactDetails],
           category:[response.response.category]
      })
      
      
      },
      (error)=>{
        console.log(error);
        console.log("edit error");
      }
    )
    this.clicked = !this.clicked;

  }

  GoBack(){
    this.clicked = !this.clicked;
  }




  //update the value now
  Update(updatedData:any){

    let updated = {
      "cars": {
        "id": this.carbyid.id,
        "name": updatedData.name,
        "contactDetails": updatedData.contactDetails,
        "price": Number(updatedData.price),
        "category": updatedData.category
      }
    }

    this.api.putCarById(updated).subscribe(
      (res)=>{
          console.log(res.message);
          
      },
      (error)=>{
          console.log("error while updating");
          console.log(error);
      }
    )



  }



  ngOnDestroy() {
    if(this.routeid){
        this.routeid.unsubscribe();
        this.carsall?.unsubscribe();
    }
  }


}
