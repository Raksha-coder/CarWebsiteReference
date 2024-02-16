using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.carCQ.comon
{
    public class postLogin:IRequest<Tuple<Tokens,string>>
    {
        public string? Username { get; set; }

        public string? Password { get; set; }
    }

    public class postLoginHandler : IRequestHandler<postLogin, Tuple<Tokens, string>>
    {
        //db
        public readonly IcarDB _context;
        //jwt
        private readonly IConfiguration _config;


        public postLoginHandler(IcarDB context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<Tuple<Tokens, string>> Handle(postLogin request, CancellationToken cancellationToken)
        {

            //find the user in register class
            var value = await _context.RegisterTable.FirstOrDefaultAsync(r => r.Username == request.Username
               && r.Password == request.Password);

          
            if (value != null)
            {
                var newLogin = new Login
                {
                    Id = Guid.NewGuid(),
                    Username = request.Username,
                    Password = request.Password,
                };
                //save to db
                await _context.LoginTable.AddAsync(newLogin);
                await _context.SaveChangesAsync();

                //now generate the token.
                var tokenHandle = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

                var tokenDescripter = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Name, newLogin.Username),
                            new Claim(ClaimTypes.Email, newLogin.Password),
                            //new Claim(JwtRegisteredClaimNames.Sub, _config([Jwt:"Subject"])),
                            //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            //new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                        }),
                    Expires = DateTime.UtcNow.AddMinutes(250),
                   
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandle.CreateToken(tokenDescripter);

                return Tuple.Create(new Tokens { Token = tokenHandle.WriteToken(token) }, value.Role);

            }
            else
            {
                return null;
            }




        }
        }

}
