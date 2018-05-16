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

        public Database(HotelContext hotelContext)
        {
            _regionDbManager = new RegionDbManager(hotelContext);

        }
        private RegionDbManager _regionDbManager;

        public IActionResult CheckIfDatabaseIsOnline() =>
            _regionDbManager.CheckIfDatabaseIsOnline() ? Ok() : StatusCode(503);

    }
}
