using Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.carCQ.comon
{
    public class putPasswordById:IRequest<HelperResponse>
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
    }


    public class putPasswordByIdHandler: IRequestHandler<putPasswordById, HelperResponse>
    {
        private readonly IcarDB _context;

        public putPasswordByIdHandler(IcarDB context)
        {
            _context = context;
        }

        public async Task<HelperResponse> Handle(putPasswordById request, CancellationToken cancellationToken)
        {
            try
            {
                var _find = await _context.RegisterTable.FirstOrDefaultAsync(r => r.Id == request.Id);

                if (_find != null)
                {
                    
                    _find.Password = request.Password;
                    await _context.SaveChangesAsync();
                    return new HelperResponse()
                    {
                        Status = 200,
                        Message = "successfully updated or reset your Password",
                        Error = null
                    };
                }
                else
                {
                    return new HelperResponse()
                    {
                        Status = 400,
                        Message = "you are not registered or you are not valid user",
                        Error = null
                    };
                }
            }catch(Exception ex)
            {
                return new HelperResponse()
                {
                    Status = 400,
                    Message = "error",
                    Error = ex.Message
                };
            }
        }
    }
}
