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
    public class getCarts :IRequest<List<AddToCart>>
    {

    }

    public class getCartsHandler : IRequestHandler<getCarts, List<AddToCart>>
    {
        public readonly IcarDB _context;

        public getCartsHandler(IcarDB context)
        {
            _context = context;
        }

        public async Task<List<AddToCart>> Handle(getCarts request, CancellationToken cancellationToken)
        {
            var allcarts = await _context.CartTable.ToListAsync();

            return allcarts;

            //if value is null
        }
    }

}
