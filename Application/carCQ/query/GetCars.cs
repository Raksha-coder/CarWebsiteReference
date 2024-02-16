using Application.carCQ.comon;
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
    public class GetCars:IRequest<List<Car>>
    {

    }

    public class GetCarsHandler : IRequestHandler<GetCars,List<Car>>
    {
        public readonly IcarDB _context;

        public GetCarsHandler(IcarDB context)
        {
            _context = context;
        }

        public async Task<List<Car>> Handle(GetCars request, CancellationToken cancellationToken)
        {
            var allcars =  await _context.CarTable.ToListAsync();

            return allcars;
        }
    }
}
