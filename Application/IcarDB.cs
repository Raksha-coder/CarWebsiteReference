using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public interface IcarDB
    {
        DbSet<Register> RegisterTable { get; }

        DbSet<Login> LoginTable { get; }

        DbSet<Car> CarTable { get; }

        DbSet<AddToCart> CartTable { get; }
        DbSet<OtpVerification> OtpVerificationTable { get; }
        Task SaveChangesAsync();
    }
}
