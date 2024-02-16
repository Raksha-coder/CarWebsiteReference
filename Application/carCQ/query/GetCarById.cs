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
    public class GetCarById : IRequest<carResponseForId>
    {
        public Guid Id { get; set; }
    }

    public class GetCarByIdHandler: IRequestHandler<GetCarById, carResponseForId>
    {
        //public readonly IcarDB _context;
        public readonly DapperContext _dapper;

        public GetCarByIdHandler(DapperContext dapper)
        {
            //_context = context;
            _dapper = dapper;
        }

        public async Task<carResponseForId> Handle(GetCarById request, CancellationToken cancellationToken)
        {
            try
            {


                string getCarByIdQuery = "SELECT * FROM CarTable WHERE Id = @Id";

                var carid = await _dapper.QuerySingleAsync(getCarByIdQuery, new { Id = request.Id });

                if (carid != null)
                {


                    return new carResponseForId()
                    {
                        Status = 200,
                        Message = "succesfull",
                        Response = carid, //car data 
                        Error = null

                    };

                }
                else
                {
                    return new carResponseForId()
                    {
                        Status = 400,
                        Message = "did not get the id",
                        Response = null,
                        Error = null

                    };
                }
            }catch(Exception ex)
            {
                return new carResponseForId()
                {
                    Status = 400,
                    Message = "error",
                    Response = null,
                    Error = ex.Message

                };
            }
          
       

       

          




        }
    }
}
