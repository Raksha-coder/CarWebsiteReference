using Dapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UpdateOTP
{
    public class Update_Otp
    {
      
        public readonly IcarDB _context;

        public Update_Otp(IcarDB context)
        {
          
            _context = context;
        }


        public async Task<bool> Update(Guid Id)
        {

            var userExist = await _context.OtpVerificationTable.FirstOrDefaultAsync(o => o.User_Id == Id);

            if(userExist != null)
            {
              
                return true;
            }
            else
            {
                return false;
            }

         

            
        }




    }
}
