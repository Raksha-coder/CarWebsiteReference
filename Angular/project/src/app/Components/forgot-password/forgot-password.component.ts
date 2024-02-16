import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { HttpService } from 'src/app/Services/http/http.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent {

  //use flag : jab tak otp ka ho nahi jata .
  flag:boolean = true;
  btnClicked:boolean = true;  // for loader 
  activeresetbutton: boolean =true;

   constructor(private api:HttpService,private fb:FormBuilder,private toastr: ToastrService){}
   reset!:FormGroup;
   ngOnInit(){
    this.reset = this.fb.group({
        email:['',[Validators.email,Validators.required]]
    });
}
timer(){
  setTimeout(() => {
    this.activeresetbutton = false;    //jab tak timer stop nahi hota, disable button
  }, 30000);
}


//otp
displayTimer:boolean = false;
userId:any;
GenerateOtp(phoneNum:string){
    this.btnClicked = false;  //loader
    this.api.postOtpGeneration(phoneNum).subscribe(
      (res)=>{
        this.btnClicked = true; //loader

        console.log(res);
        this.userId = res.item2;
        console.log(this.userId);
        this.toastr.success("successfully sent the Otp")
        this.displayTimer = true; 
        this.timer();
      },
      (error)=>{
        console.log(error);
        
      }
      
    )
}


VerifyOtp(getOtp:any){
  
    this.api.postValidateOtp(this.userId,getOtp).subscribe(
      (res)=>{
        this.flag = false;
        console.log(res);
        //after the flag , i want to design the disabled button.
        this.toastr.success("Success");
        
      },
      (error)=>{
        console.log(error);
      }
    )
}



//otp end
submit(email:any){
  console.log(email);
  let convert = email.email;
  this.api.getResetPassword(convert).subscribe(
    (res)=>{
      console.log(res);
      this.toastr.success("successfully sended the email");
    },
    (error)=>{
      this.toastr.warning(error);
    }
  )
 }
}
