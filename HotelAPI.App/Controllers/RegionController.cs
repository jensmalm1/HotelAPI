using System.Collections.Generic;
using HotelAPI.Data;
using HotelAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using static HotelAPI.App.Validation;

namespace HotelAPI.App.Controllers
{
    [Produces("application/json")]
    [Route("api/Region")]
    public class RegionController : Controller
    {
        private readonly RegionDbManager _regionDbManager = new RegionDbManager();
        private readonly Validation _validation = new Validation();

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