import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpService } from 'src/app/Services/http/http.service';

@Component({
  selector: 'app-add-cars',
  templateUrl: './add-cars.component.html',
  styleUrls: ['./add-cars.component.css']
})
export class AddCarsComponent {

  addCar!:FormGroup;

  constructor(private api:HttpService,private fb:FormBuilder){}


  ngOnInit(){
      this.addCar = this.fb.group({
        name:['',Validators.required],
        contactDetails:['',[Validators.required,
          Validators.pattern("^[0-9]*$"),
          Validators.min(10), 
          Validators.max(10)]],
        price:['',Validators.required],
        category:['',Validators.required]
      })
  }


  onSubmit(formdata:any){
    let data =
      {
        "name": formdata.name,
        "contactDetails": formdata.contactDetails,
        "price": formdata.price,
        "category": formdata.category
      }
    

    this.api.postCar(data).subscribe(
      (res)=>{
            console.log(res);
            console.log("successfully added");
      },
      (error)=>{
          console.log(error);
      }
    )
  }


}
