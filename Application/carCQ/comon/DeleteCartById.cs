using Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.carCQ.comon
{
    public class DeleteCartById:IRequest<Delete_Response>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCartByIdHandler: IRequestHandler<DeleteCartById, Delete_Response>
    {
        public readonly IcarDB _context;

        public DeleteCartByIdHandler(IcarDB context)
        {
            _context = context;
        }

        public async Task<Delete_Response> Handle(DeleteCartById request, CancellationToken cancellationToken)
        {
            try
            {

                var cars = await _context.CartTable.Where(c => c.Id == request.Id).FirstOrDefaultAsync();

                if (cars == null)
                {
                    return new Delete_Response()
                    {
                        State = 400,
                        Message = "unsuccess",
                        Error = null
                    };

                }
                else
                {

                    _context.CartTable.Remove(cars);
                    await _context.SaveChangesAsync();
                    return new Delete_Response()
                    {
                        State = 200,
                        Message = "successfully removed",
                        Error = null
                    };
                }


            }catch(Exception ex)
            {
                return new Delete_Response()
                {
                    State = 400,
                    Message = ex.Message,
                    Error = ex.Message
                };
            }
        }
    }
}
