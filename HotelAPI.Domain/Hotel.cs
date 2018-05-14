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

        [JsonProperty(PropertyName = "LedigaRum")]
        public int Rooms { get; set; }

        [JsonProperty(PropertyName = "Reg")]
        public int RegionValue { get; set; }

    }
}
