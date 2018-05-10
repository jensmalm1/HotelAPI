using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelAPI.Data;
using HotelAPI.Domain;

namespace HotelAPI.App
{
    public class Validation
    {
        public Validation(RegionDbManager regionDbManager)
        {
            _regionDbManager = regionDbManager;
        }

        private readonly RegionDbManager _regionDbManager;

        public bool CorrectValue(int value)
        {
            return value != 0;
        }

        public bool CorrectName(string name)
        {
            return !String.IsNullOrWhiteSpace(name);
        }

        public bool RegionExists(int value)
        {
            var list = _regionDbManager.ReturnAllRegions().Where(x=>x.Value == value);
            return list.Count() != 0;
        }

        public bool IsValidRegion(Region region)
        {
            return CorrectValue(region.Value) && CorrectName(region.Name);
        }

    }
}
