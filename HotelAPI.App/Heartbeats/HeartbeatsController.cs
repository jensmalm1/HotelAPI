using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelAPI.Data;
using HotelAPI.Domain;
using HotelAPI.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Presentation.Heartbeats
{
    [Route("Check")]
    public class HeartbeatsController : Controller
    {
        private readonly Parser _parser;
        private HeartbeatDbManager _heartbeatDbManager;

        public HeartbeatsController(HotelContext hotelContext, Parser parser)
        {
            _parser = parser;
            _heartbeatDbManager = new HeartbeatDbManager(hotelContext);
        }


        public IActionResult CheckIfDatabaseIsOnline() =>
            _heartbeatDbManager.CheckIfDatabaseIsOnline() ? Ok() : StatusCode(503);

        [HttpGet("Nothing")]
        public IActionResult SiteIsOnline()
        {
            return Ok("Hemsidan är online");
        }

        [HttpGet("WesternHotel")]
        public IActionResult CheckIfWesternHotelsImported()
        {
            return CheckIfWesternHotelImportedToday() ? Ok() : StatusCode(503);
        }


        [HttpGet("ScandicHotel")]
        public IActionResult CheckIfScandicHotelIsImported()
        {
            return CheckIfScandicHotelImportedToday() ? Ok() : StatusCode(503);
        }

        public bool CheckIfWesternHotelImportedToday()
        {
            var today = DateTime.Now.Date.ToString("yyyy-MM-dd");
            if (_parser.SortJsonFilesByDate()[0].Contains(today.ToString()))
                return true;
            return false;
        }

        public bool CheckIfScandicHotelImportedToday()
        {
            var today = DateTime.Now.Date.ToString("yyyy-MM-dd");
            if (_parser.SortTextFilesByDate()[0].Contains(today))
                return true;
            return false;
        }
    }
}
