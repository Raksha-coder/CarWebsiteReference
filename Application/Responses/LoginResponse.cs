using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class LoginResponse<T>
    {
        public int Status { get; set; }
        public string? StatusDescription { get; set; }

        public string? Role { get; set; }
        public List<T> Response { get; set; }
        public string? Error { get; set; }
        public Tokens Token { get; set; }
    }
}
