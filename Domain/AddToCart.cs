using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AddToCart
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ContactDetails { get; set; }

        public double Price { get; set; }

        public string Category { get; set; }

    }
}
