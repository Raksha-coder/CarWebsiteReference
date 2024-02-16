using Application.Responses;
using Azure;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.carCQ.comon
{
    public class postCart:IRequest<string>
    {
        public string Name { get; set; }

        public string ContactDetails { get; set; }

        public double Price { get; set; }

        public string Category { get; set; }
    }

    public class postCartHandler : IRequestHandler<postCart,string>
    {

        public readonly IcarDB _context;

        public postCartHandler(IcarDB context)
        {
            _context = context;
        }

        public async Task<string> Handle(postCart response, CancellationToken cancellationToken)
        {

            try
            {
                var carAdd = new AddToCart()
                {
                    Id = Guid.NewGuid(),
                    Name = response.Name,
                    ContactDetails = response.ContactDetails,
                    Price = response.Price,
                    Category = response.Category,
                };

                await _context.CartTable.AddAsync(carAdd);
                await _context.SaveChangesAsync();

                return "Added to cart";


            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
