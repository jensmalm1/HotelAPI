using System;
using System.Collections.Generic;
using System.Linq;
using HotelAPI.Data;
using HotelAPI.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.App.Controllers
{
    [Produces("application/json")]
    [Route("api/Region")]
    public class RegionController : Controller
    {

        private readonly RegionDbManager _regionDbManager;
        private readonly Validation _validation;

        public RegionController(HotelContext hotelContext)
        {
            _regionDbManager = new RegionDbManager(hotelContext);
            _validation = new Validation(_regionDbManager);
            _regionDbManager.EnsureDatabaseCreated();
        }


        [HttpPost]
        public IActionResult AddRegion(Region region)
        {
            if (_validation.IsValidRegion(region))
            {
                _regionDbManager.CreateRegion(region);
                return Ok();
            }

            return BadRequest("Fel.");
        }


        [HttpGet]
        public IActionResult GetAllRegions()
        {
            return Ok(_regionDbManager.ReturnAllRegions());

        }

        [HttpGet("{id}")]
        public void GetRegion(int id)
        {

        }

        [HttpDelete("{value}")]
        public IActionResult DeleteRegion(int value)
        {

            if (_validation.RegionExists(value))
            {
                _regionDbManager.DeleteRegion(value);
                return Ok();
            }
            return NotFound("There is no region with that value");
        }

        [HttpPost("GetFromTextFile")]
        public IActionResult AddFromFile(string path)
        {
            var hotels = new List<Hotel>();

            var regions = _regionDbManager.ReturnAllRegions();

            var files = System.IO.Directory.GetFiles(@"C:\Project\HotelAPI\HotelAPI.App", "*.txt").OrderByDescending(x=>x).ToList();
            



            var input = System.IO.File.ReadAllText($"{files[0]}").Split('\n').ToList();

            foreach (var line in input)
            {
                var hotel = new Hotel();
                var test = line.Split(',');

                var regionId = Convert.ToInt32(test[0]);
                hotel.Name = test[1];
                hotel.Rooms = Convert.ToInt32(test[2]);
                hotels.Add(hotel);

                foreach (var region in regions)
                {
                    if (regionId == region.Value)
                    {
                        region.Hotels.Add(hotel);
                    }
                }
            }
            _regionDbManager.UpdateRegion(regions);
            return Ok();
        }

        [HttpPost("recreate")]
        public void SeedDatabase()
        {
            RecreateDatabase();
            _regionDbManager.CreateRegion(ReturnBasicRegions());
        }

        public void RecreateDatabase()
        {
            _regionDbManager.RecreateDatabase();
        }

        private List<Region> ReturnBasicRegions()
        {
            var regions = new List<Region>();

            regions.Add(new Region()
            {
                Value = 50,
                Name = "Göteborg Centrum",
            });

            regions.Add(new Region()
            {
                Value = 60,
                Name = "Göteborg Hisingen",
            });

            regions.Add(new Region()
            {
                Value = 70,
                Name = "Helsingborg",
            });

            return regions;
        }

    }
}