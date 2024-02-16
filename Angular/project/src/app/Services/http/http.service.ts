import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http:HttpClient) { }

  private url ="https://localhost:7242/api/View";


  getCars():Observable<any>{
      return this.http.get(`${this.url}/Cars`);
  }

  getCarts():Observable<any>{
    return this.http.get(`${this.url}/Carts`);
}

  getCarById(carId:any):Observable<any>{  //using guid so that's why i havent use number datatype
      return this.http.get(`${this.url}/Cars/id?id=${carId}`,carId);
  }

  postRegister(data:any):Observable<any>{
    return this.http.post(`${this.url}/Registration`,data);
  }

  postLogin(data:any):Observable<any>{
    return this.http.post(`${this.url}/Login`,data);
  }

  postCar(data:any):Observable<any>{
    return this.http.post(`${this.url}/car`,data);
  }

  postCart(data:any):Observable<any>{
    return this.http.post(`${this.url}/addtocart`,data);
  }

  putCarById(carId:any):Observable<any>{
    return this.http.put(`${this.url}/id`,carId);
  }


  deleteCartById(id:any):Observable<any>{
    return this.http.delete(`${this.url}/Carts/id?id=${id}`,id);
  }
 

reset_urll ="https://localhost:7242/api/View/PasswordForgot?email";

  getResetPassword(email:any):Observable<any>{

    var res = email.split("@");
    console.log(res[0]);
    return this.http.get(`${this.reset_urll}=${res[0]}%40gmail.com`,email);
  }

  putPasswordById(newPassword:any):Observable<any>{
    return this.http.put(`${this.url}/password/id`,newPassword);
  }





  // Otp Generation : 
  postOtpGeneration(userPhoneNumber:string):Observable<any>{
      return this.http.post(`${this.url}/generateOTP?Mobile_Number=${userPhoneNumber}`,userPhoneNumber);
  }


  ///View/VerfyOTP?id=1d813f30-d63b-4cdd-b52d-0a26e5382a3f&otp=11609

  postValidateOtp(id:string,otp:string):Observable<any>{
     const body = { id:id,otp:otp}
      return this.http.post(`${this.url}/VerfyOTP?id=${id}&otp=${otp}`,body);
  }




}
