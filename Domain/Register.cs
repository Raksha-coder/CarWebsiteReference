using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Register
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        //forgot password
        public string Email { get; set; }

        //otp generation
        public string PhoneNum { get; set; }

        public string Role { get; set; }


    }
}
