﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OtpVerification
    {
        public Guid User_Id { get; set; }

        public string OTP { get; set; }

        public  TimeOnly CurrentTime { get; set; }

    }
}
