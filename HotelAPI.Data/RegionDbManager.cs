using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Data
{
    public class RegionDbManager : IDisposable
    {
        private readonly HotelContext _context;

        public RegionDbManager(HotelContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void CreateRegion(Region region)
        {
            _context.Add(region);
            _context.SaveChanges();
        }

        public void CreateRegion(List<Region> regionList)
        {
            _context.AddRange(regionList);
            _context.SaveChanges();
        }

        public List<Region> ReturnAllRegions()
        {
            return _context.Regions.Include(a=>a.Hotels).ToList();
        }

        public void RecreateDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        public void EnsureDatabaseCreated()
        {
            _context.Database.EnsureCreated();
        }

        public void DeleteRegion(int removeId)
        {
            var region = _context.Regions.Single(x => x.Id == removeId);

            _context.Regions.Remove(region);
            _context.SaveChanges();
        }
    }
}
