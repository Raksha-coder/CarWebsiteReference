using Application.mail;
using Application.Responses;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.carCQ.comon
{
    public class postRegister:IRequest<registerResponse>
    {
        public string ?Username { get; set; }

        public string ?Password { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string ?Role { get; set; }
    }

    public class postRegisterHandler : IRequestHandler<postRegister,registerResponse>
    {
        //dependency injection
        public readonly IcarDB _context;
        public readonly IMailService _email;

        public postRegisterHandler(IcarDB context, IMailService email)
        {
            _context = context;
            _email = email;
        }


        public async Task<registerResponse> Handle(postRegister response,CancellationToken token)
        {
           
            try
            {

                var newUser = new Register()
                {
                    Id = Guid.NewGuid(),
                    Username = response.Username,
                    Password = response.Password,
                    Email = response.Email,
                    PhoneNum = response.PhoneNumber,
                    Role = response.Role,
                };

       
                var _find = await _context.RegisterTable.FirstOrDefaultAsync(u => u.Username == newUser.Username);


                if (_find != null)
                {
                    return new registerResponse()
                    {

                        Status = 400,
                        Message = "user already registered",
                        Response = null,
                        Error = null
                    };
                }
                else
                {
                    await _context.RegisterTable.AddAsync(newUser);
                    await _context.SaveChangesAsync();

                    //mail
                    var maildata = new EmailData()
                    {
                        //take email from the user 
                        EmailToId = "ku.rakshadilipjaiswal@gmail.com",
                        //user name
                        EmailToName = "Raksha Jaiswal",
                        //subject default
                        EmailSubject = "You Have Successfully Registered to Our Website",
                        EmailBody = "Thank You!!:>",
                    };

                    var sended = _email.sendData(maildata);

                    return new registerResponse()
                    {
                        Status = 200,
                        Message = "Successfully Added",
                        Response = new List<Register> { newUser },
                        Error = null,
                        MailSended = sended
                    };
                }

               

               

            }catch(Exception ex)
            {
                return new registerResponse()
                {

                    Status = 400,
                    Message = "Error while adding the details",
                    Response = null,
                    Error = ex.Message,
                    MailSended = "No error"
                };
            }



        }
    }


}
