using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HotelAPI.Domain
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Rooms { get; set; }

        public int RegionValue { get; set; }

    }
}
