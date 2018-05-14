using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HotelAPI.Domain
{
    public class Region
    {
        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int Value { get; set; }
        public List <Hotel> Hotels { get; set; }

    }
}
