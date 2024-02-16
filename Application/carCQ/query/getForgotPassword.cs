using Application.Responses;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.carCQ.query
{
    public class getForgotPassword:IRequest<HelperResponse>
    {
        public string Email { get; set; }
    }

    public class getForgotPasswordHandler : IRequestHandler<getForgotPassword, HelperResponse>
    {

        public readonly IcarDB _context;
        public readonly IMailService _mailService;

        public getForgotPasswordHandler(IcarDB context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        public async Task<HelperResponse> Handle(getForgotPassword request, CancellationToken cancellationToken)
        {
            var _find = await _context.RegisterTable.Where(r => r.Email == request.Email).FirstOrDefaultAsync();
            
            if(_find != null)
            {
                _mailService.SendForgotPasswordLink(_find.Email,_find.Username,_find.Id.ToString());
                return new HelperResponse()
                {
                    Status = 200,
                    Message = "Successful",
                    Error = null
                };
            }
            else
            {
                return new HelperResponse()
                {
                    Status = 400,
                    Message = "Unsuccessful, you are not a member of our website",
                    Error = null
                };
            }


        }
    }
}
