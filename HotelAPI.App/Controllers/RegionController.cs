using System.Collections.Generic;
using HotelAPI.Data;
using HotelAPI.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.App.Controllers
{
    [Produces("application/json")]
    [Route("api/Region")]
    public class RegionController : Controller
    {
        private readonly RegionDbManager _regionDbManager = new RegionDbManager();

        [HttpPost]
        public void AddRegion(Region region)
        {
            _regionDbManager.CreateRegion(region);
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

        [HttpDelete("{id}")]
        public void DeleteRegion(int id)
        {

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

        public List<Region> ReturnBasicRegions()
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