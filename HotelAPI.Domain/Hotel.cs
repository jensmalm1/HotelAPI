using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
//Todo: saker som gör proj unik, läsa in från fiö?
namespace HotelAPI.Domain
{
    public class Hotel
    {
        
        public string Name { get; set; }

        [JsonProperty(PropertyName = "LedigaRum")]
        public int Rooms { get; set; }

        [JsonProperty(PropertyName = "Reg")]
        [NotMapped]
        public int RegionValue { get; set; }

    }
}
