using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelAPI.Domain;

namespace HotelAPI.Data
{
    public class RegionDbManager : IDisposable
    {
        private readonly HotelContext _context = new HotelContext();
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void CreateRegion(Region region)
        {
            _context.Add(region);
            _context.SaveChanges();
        }
    }
}
