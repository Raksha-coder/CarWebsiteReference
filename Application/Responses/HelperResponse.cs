using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class HelperResponse
    {
        public int Status { get; set; }

        public string Message { get; set; }

        //public T Response { get; set; }
        public string Error { get; set; }
    }
}
