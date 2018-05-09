using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelAPI.Data;
using HotelAPI.Domain;
using Microsoft.AspNetCore.Http;
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
            //var temp = new Region();
            //temp.Name = name;
            //temp.Id = id;
            _regionDbManager.CreateRegion(region);
        }

        [HttpGet]
        public void GetAllRegion()
        {

        }

        [HttpGet("{id}")]
        public void GetRegion(int id)
        {

        }

        [HttpDelete ("{id}")]
        public void DeleteRegion(int id)
        {

        }

        [HttpPost("recreate")]
        public void SeedDatabase()
        {

        }

        public void RecreateDatabase()
        {
            
        }

        public List<Region> ReturnBasicRegions()
        {
            var regions = new List<Region>();

            regions.Add(new Region()
            {
                Id = 50,
                Name = "Göteborg Centrum",
            });

            regions.Add(new Region()
            {
                Id = 60,
                Name = "Göteborg Centrum",
            });

            regions.Add(new Region()
            {
                Id = 70,
                Name = "Helsingborg",
            });

            return regions;
        }

    }
}