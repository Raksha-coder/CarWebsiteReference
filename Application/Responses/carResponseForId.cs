using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class carResponseForId
    {
        public int Status { get; set; }

        public string Message { get; set; }
        public Car Response { get; set; }
        public string Error { get; set; }
    }
}
