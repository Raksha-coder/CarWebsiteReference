using Application.Responses;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.carCQ.comon
{
    public class postCar:IRequest<CarResponses<Car>>
    {
        public string Name { get; set; }

        public string ContactDetails { get; set; }

        public double Price { get; set; }

        public string Category { get; set; }
    }

    public class postCarHandler:IRequestHandler<postCar, CarResponses<Car>>
    {
        public readonly IcarDB _context;

        public postCarHandler(IcarDB context)
        {
            _context = context;
        }

        public async Task<CarResponses<Car>> Handle(postCar response, CancellationToken token)
        {

            try
            {
                var newCar = new Car()
                {
                    Id = Guid.NewGuid(),
                    Name = response.Name,
                    ContactDetails = response.ContactDetails,
                    Price = response.Price,
                    Category = response.Category,
                };

                await _context.CarTable.AddAsync(newCar);
                await _context.SaveChangesAsync();

                return new CarResponses<Car>()
                {
                    Status = 200,
                    Message = "Successfully added",
                    Response = new List<Car> { newCar },
                    Error = null
                };


            }catch(Exception ex)
            {
                return new CarResponses<Car>()
                {
                    Status = 400,
                    Message = "Error while adding the car",
                    Response = null,
                    Error = ex.Message
                };
            }





        }
        }
}
