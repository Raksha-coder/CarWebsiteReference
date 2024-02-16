import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from 'src/app/Services/http/http.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent {

  chngPass!:FormGroup;

  constructor(private http: HttpService, private fb:FormBuilder,private route: ActivatedRoute){}

  //use param snap:email mai link click ki , wahi backend se id mil rhi h .
  testId!: string;
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.testId = params['id']; // Access the 'id' parameter from the URL
      console.log('Test ID:', this.testId);
    });



    this.chngPass = this.fb.group({
      password:['',Validators.required]
    })
  }



  submit(changedPass:any){

    let newpass = 
      {
        "id": this.testId,
        "password": changedPass.password
      }
    

      this.http.putPasswordById(newpass).subscribe(
        (res)=>{
            console.log(res);
            
        },
        (error)=>{
          console.log(error);
          
        }
      )
  }
}
