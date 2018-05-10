﻿using System;
using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Domain
{
    public class Region
    {
        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int Value { get; set; }

    }
}
