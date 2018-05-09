using System;
using HotelAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Data
{
    public class HotelContext : DbContext
    {

        public DbSet<Region> Regions { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server = (localdb)\\mssqllocaldb; Database = EfHotelApi; Trusted_Connection = True; ");
            }
    }
}
