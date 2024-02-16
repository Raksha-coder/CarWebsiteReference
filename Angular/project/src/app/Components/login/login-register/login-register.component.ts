import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpService } from 'src/app/Services/http/http.service';

@Component({
  selector: 'app-login-register',
  templateUrl: './login-register.component.html',
  styleUrls: ['./login-register.component.css']
})
export class LoginRegisterComponent {

    switch:boolean = true;
     registerform!:FormGroup;
     loginform!:FormGroup;
    Switch(){
      this.switch = !this.switch;
    }

    constructor(private fb:FormBuilder,
                private route:Router,
                private api:HttpService,
                private toast:ToastrService){}


    ngOnInit(){
        this.registerform = this.fb.group({
            username:['',Validators.required],
            email:['',Validators.email],
            phoneNumber: ['',Validators.required],
            password:['',Validators.required],
            role:['',Validators.required]
        });


        this.loginform = this.fb.group({
          username:['',Validators.required],
          password:['',Validators.required],
      });
    }


    //additional task in this ---->add captcha and otp
    OnRegister(formData:any){ 
      let register = {
        "username": formData.username,
        "password": formData.password,
        "email":formData.email,
        "phoneNumber": formData.phoneNumber,
        "role": formData.role
      }

        this.api.postRegister(register).subscribe(
          (Response)=>{
            this.toast.success("You have successfully registered ");
            localStorage.setItem("role",formData.role);
            
            this.route.navigate(['/navbar']);
            console.log("successfully registered");
            
          },
          (error)=>{
              this.toast.warning(error);
              console.log("error in registration");
              
              
          }
        );
        //redirect to login 
       
    }


    OnLogin(formdata:any){

      let login = {
        "username": formdata.username,
        "password": formdata.password
      }

      this.api.postLogin(login).subscribe(
        (response)=>{
            console.log("you have successfully login");
            localStorage.setItem("token",response.token.token);
            localStorage.setItem("role",response.role);   //login mai role hi nahi h, backend se data aaega
            this.route.navigate(['/navbar']);
            
        },
        (error)=>{
          console.log(error);
          alert("You are not registered,please register first");
        }
      );

    
    }

}
