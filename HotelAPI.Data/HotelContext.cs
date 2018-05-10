using System;
using HotelAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Data
{
    public class HotelContext : DbContext
    {

        public DbSet<Region> Regions { get; set; }

        public HotelContext(DbContextOptions<HotelContext> context) : base(context)
        {
        }


    }
}
