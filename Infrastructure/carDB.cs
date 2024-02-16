using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Internal;

namespace Infrastructure
{
    public class carDB:DbContext,IcarDB
    {
        public carDB(DbContextOptions<carDB> dbContextOptions) : base(dbContextOptions) { }

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }


        //conversion of dateTime to timeOnly
        public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
        {
            public TimeOnlyConverter() : base(
                timeOnly => timeOnly.ToTimeSpan(),
                timeSpan => TimeOnly.FromTimeSpan(timeSpan))
            { }
        }


        public DbSet<Register> RegisterTable => Set<Register>();

        public DbSet<Login> LoginTable => Set<Login>();

        public DbSet<Car> CarTable => Set<Car>();
        public DbSet<AddToCart> CartTable => Set<AddToCart>();

        public DbSet<OtpVerification> OtpVerificationTable => Set<OtpVerification>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //fluent configuration 
            modelBuilder.Entity<Register>()
                 .HasKey(p => p.Id);

            modelBuilder.Entity<Login>()
               .HasKey(p => p.Id);

            modelBuilder.Entity<Car>()
               .HasKey(p => p.Id);

            modelBuilder.Entity<OtpVerification>()
             .HasKey(o => o.User_Id);




            //conversion of datetime to timeonly
            modelBuilder.Entity<OtpVerification>()
                .Property(o => o.CurrentTime)
                .HasConversion<TimeOnlyConverter>();
      
                  



            //seeding
            modelBuilder.Entity<Car>().HasData(

                new Car { Id =Guid.NewGuid(),Name ="fordOne",ContactDetails = "111111",Price=250000,Category="sell"},
                new Car { Id = Guid.NewGuid(), Name = "fordTwo", ContactDetails = "222222", Price = 50000, Category = "buy" }

            );

        }

    }
}
