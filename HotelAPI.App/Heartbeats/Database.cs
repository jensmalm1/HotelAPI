using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Presentation.Heartbeats
{
    [Route("Check")]
    public class Database:Controller
    {
        private RegionDbManager _regionDbManager;

        public Database(HotelContext hotelContext)
        {
            _regionDbManager = new RegionDbManager(hotelContext);

        }


        public IActionResult CheckIfDatabaseIsOnline() =>
            _regionDbManager.CheckIfDatabaseIsOnline() ? Ok() : StatusCode(503);


        [HttpGet("WesternHotel")]
        public IActionResult CheckIfWesternHotelsImported()
        {
            return _regionDbManager.CheckIfWesternHotelImportedToday() ? Ok() : StatusCode(503);
        }
    }
}
