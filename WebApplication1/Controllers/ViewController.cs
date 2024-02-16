using Application;
using Application.carCQ.comon;
using Application.carCQ.query;
using Application.Responses;
using Application.UpdateOTP;
using Dapper;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace WebApplication1.Controllers
{
    
    public class ViewController : ApiController
    {
        //dapper
        public readonly DapperContext _dapper;
        public readonly IcarDB _context;

        public ViewController(DapperContext dapper, IcarDB context)
        {
            //_context = context;
            _dapper = dapper;
            _context = context;
        }


        [HttpPost("Registration")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] postRegister command)
        {
            var value = await Mediator.Send(command);

            if (value.Status == 200)
            {
                return Ok(value);
            }
            else
            {
                return BadRequest(value);
            }


        }

        [HttpPost("Login")]
        [AllowAnonymous]  //why we use this?

        public async Task<IActionResult> PostLogin([FromBody] postLogin login)
        {
            var token = await Mediator.Send(login);  //get the token
            try
            {
                if (token == null)
                {
                    var response = new LoginResponse<Login>()
                    {
                        Status = 400,
                        StatusDescription = "username and password are incorrect",
                        Role = null,
                        Response = null,
                        Error = null,
                        Token = null
                    };
                    return BadRequest(response);
                }
                else
                {
                    var response = new LoginResponse<Login>()
                    {
                        Status = 200,
                        StatusDescription = "successful",
                        Role = token.Item2,                    // role from registration
                        Response = new List<Login>
                     {
                         new Login
                         {
                             Username = login.Username,
                             Password = login.Password
                         }
                     },
                        Error = null,
                        Token = token.Item1
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                var response1 = new LoginResponse<Login>()
                {
                    Status = 400,
                    StatusDescription = "error",
                    Role= null,
                    Response = null,
                    Error = ex.Message,
                    Token = null
                };
                return BadRequest(response1);
            }
        }




     
        [HttpPost("car")]
      
        public async Task<IActionResult> PostCar([FromBody] postCar car)
        {
            var carValue = await Mediator.Send(car);

            if(carValue.Status == 200)
            {
                return Ok(carValue);
            }
            else
            {
                return BadRequest(carValue);
            }

          
        }



        [HttpPost("addtocart")]
        
        public async Task<IActionResult> PostCart([FromBody] postCart cart)
        {
            var cartValue = await Mediator.Send(cart);

            if (cartValue != null)
            {
                return Ok(cartValue);
            }
            else
            {
                return BadRequest(cartValue);
            }


        }






        [HttpGet("Cars")]
        [Authorize]
        public async Task<IActionResult> GetCars()
        {
            var cars = await Mediator.Send(new GetCars());

            if(cars != null)
            {
                return Ok(cars);
            }
            else
            {
                return BadRequest(cars);
            }
        }


        [HttpGet("Carts")]
 
        public async Task<IActionResult> GetCarts()
        {
            var cars = await Mediator.Send(new getCarts());

            if (cars != null)
            {
                return Ok(cars);
            }
            else
            {
                return BadRequest(cars);
            }
        }




        [HttpGet("Cars/id")]
    
        public async Task<IActionResult> GetCarById(Guid id)
        {
            var cars = await Mediator.Send(new GetCarById { Id = id});

            if (cars.Status == 200)
            {
                return Ok(cars);
            }
            else
            {
                return BadRequest(cars);
            }
        }




        [HttpPut("id")]
     
        public async Task<IActionResult> Update([FromBody] putCarById command)
        {
            var x = await Mediator.Send(command);

            if(x.Status == 200)
            {
                return Ok(x);
            }
            else
            {
                return BadRequest(x);
            }
        }



        [HttpDelete("Carts/id")]
       
        public async Task<IActionResult> Delete(Guid id)
        {
            var x = await Mediator.Send(new DeleteCartById { Id = id });

            if(x.State == 400)
            {
                return BadRequest(x);
            }else if(x.State == 200)
            {
                return Ok(x);
            }
            else
            {
                return BadRequest(x);
            }
         
        }


        //forgot password
        [HttpGet("PasswordForgot")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEmail(string email)
        {
            var resetpass = await Mediator.Send(new getForgotPassword { Email = email});

            if (resetpass.Status == 200)
            {
                return Ok(resetpass);
            }
            else
            {
                return BadRequest(resetpass);
            }
        }



        //Update password in registration

        [HttpPut("password/id")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdatePassword([FromBody] putPasswordById command)
        {
            var x = await Mediator.Send(command);

            if (x.Status == 200)
            {
                return Ok(x);
            }
            else
            {
                return BadRequest(x);
            }
        }





        //Otp generate : using this on forgot password
        [HttpPost("generateOTP")]
        [AllowAnonymous]
        public async Task<IActionResult> PostOtp(string Mobile_Number)
        {
            try
            {
                Guid UserId = Guid.Empty;
                /*I want the id from register table */

                //email send krte time , see that the number entered is registered or not ,
                //if no. is registered then take the id from register class and put it into otp class.
                //and then send otp.
                var getid = await _context.RegisterTable.Where(r => r.PhoneNum == Mobile_Number).FirstOrDefaultAsync();
                if(getid != null )
                {
                    UserId = getid.Id;


                    Random ran = new Random();
                    var otp = ran.Next(11111, 99999);


                    Update_Otp obj = new Update_Otp(_context);
                    var userExistOrNot = await obj.Update(UserId);  //async

                    if (userExistOrNot == true)  //user already exist,Update the otp only
                    {
                        var currentGenerateOtpTime = TimeOnly.FromDateTime(DateTime.Now);
                       



                        string queryUpdate = "UPDATE otpverificationtable SET OTP = @otp, CurrentTime=@CurrentTime  WHERE User_Id = @UserId";

                            var parameters = new DynamicParameters();
                            parameters.Add("OTP", otp, DbType.String);
                            parameters.Add("UserId", UserId, DbType.Guid);  // giving me error 
                            parameters.Add("CurrentTime", currentGenerateOtpTime.ToTimeSpan(),DbType.Time);
                        //dapper do not support time so I have to use datetime


                        await _dapper.ExecuteAsync(queryUpdate, parameters);
                    }
                    else
                    {
                        
                        var currentGenerateOtpTime = TimeOnly.FromDateTime(DateTime.Now);
                      


                        string query = "Insert into otpverificationtable values(@UserId, @otp,@CurrentTime)";

                        var otpParams = new DynamicParameters();
                        otpParams.Add("UserId", UserId, DbType.Guid);
                        otpParams.Add("OTP", otp, DbType.String);
                        otpParams.Add("CurrentTime",currentGenerateOtpTime.ToTimeSpan(),DbType.Time);

                        await _dapper.ExecuteAsync(query, otpParams);
                    }


                    //update the otp if same user request for otp.
                    //first check if user is in otpverificationtable or not 
                    //if exist ,  then just update the otp.

                    TwilioSMS sendOtp = new TwilioSMS(Mobile_Number, otp.ToString());

                    sendOtp.SendSms();
                    

                    var response = new Tuple<TwilioSMS,Guid>(sendOtp, UserId);
                    return Ok(response);


                }
                else
                {
                    return BadRequest("You are not a valid user");
                }


            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

         
        }




        //verify OTP.....

        //check that user have typed the coreect otp and then remove the whole entry from otpVerification.

        [HttpPost("VerfyOTP")]
        [AllowAnonymous]


        public async Task<IActionResult> VerifyOtp(Guid id,string otp)
        {
            var _verify = await _context.OtpVerificationTable.Where(o=> o.User_Id == id).FirstOrDefaultAsync();

            if(_verify != null )
            {

                if(_verify.OTP == otp)
                {
                    //remove otp from db.
                    //after validate.
                    var OtpGenerateTime = _verify.CurrentTime;
                    var endtime = TimeOnly.FromDateTime(DateTime.Now);

                    var timediff = endtime.ToTimeSpan() - OtpGenerateTime.ToTimeSpan();

                    bool isDifferenceGreaterThan30 = timediff.TotalSeconds > 30;

                    if(isDifferenceGreaterThan30)
                    {


                        //remove the record.
                        var userid = _verify.User_Id; 

                        string query = "DELETE FROM otpverificationtable WHERE User_Id = @userid";
                        var otpParams = new DynamicParameters();
                        otpParams.Add("UserId", userid, DbType.Guid);
                        await _dapper.ExecuteAsync(query, otpParams);

                        return StatusCode(StatusCodes.Status400BadRequest, new otpResponse { Message = "Expired", Error = null });


                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status200OK, new otpResponse { Message = "Successful", Error = null });
                    }

                
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new otpResponse { Message = "Wrong OTP,Please Type Again", Error = null });
                }
            
            }
            else
            {
              
                return StatusCode(StatusCodes.Status400BadRequest, new otpResponse { Message = "You are not a valid user", Error = null });

            }
        }







    }
}
