using Application.carCQ.query;
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
    public class putCarById:IRequest<CarResponses<Car>>
    {
        public Car Cars { get; set; }
    }

    public class putCarByIdHandler: IRequestHandler<putCarById, CarResponses<Car>>
    {
        private readonly IcarDB _context;

        public putCarByIdHandler(IcarDB context)
        {
            _context = context;
        }

        public async Task<CarResponses<Car>> Handle(putCarById request, CancellationToken cancellationToken)
        {
            try
            {
                var existingCar = await _context.CarTable.FirstOrDefaultAsync(r => r.Id == request.Cars.Id);

                if (existingCar == null)
                {
                    return new CarResponses<Car>
                    {
                        Status = 400,
                        Message = "Car not found",
                        Response = null,
                        Error = "Car not found with the specified ID"
                    };
                }

                //id milne ke baad
                // Update the existing car properties
                existingCar.Name = request.Cars.Name;
                existingCar.ContactDetails = request.Cars.ContactDetails;
                existingCar.Price = request.Cars.Price;
                existingCar.Category = request.Cars.Category;

                //_context.CarClass.Update(existingCar);
                await _context.SaveChangesAsync();

                var response = new CarResponses<Car>
                {
                    Status = 200,
                    Message = "Successfully updated car",
                    Response = new List<Car>
                    {
                        new Car
                        {
                            Id = existingCar.Id,
                            Name = existingCar.Name,
                            ContactDetails = existingCar.ContactDetails,
                            Price = existingCar.Price,
                            Category= existingCar.Category,
                        }
                    },
                    Error = null
                };

                return response;

            }
            catch (Exception ex)
            {
                return new CarResponses<Car>
                {
                    Status = 400,
                    Message = null,
                    Response = null,
                    Error = ex.Message
                };
            }


        }


    
}
}
