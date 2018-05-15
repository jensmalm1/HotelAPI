using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HotelAPI.Data;
using HotelAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace HotelAPI.App.Controllers
{
    [Produces("application/json")]
    [Route("api/Region")]
    public class RegionController : Controller
    {
        private readonly RegionDbManager _regionDbManager;
        private readonly Validation _validation;
        private readonly Parser _parser;

        public RegionController(HotelContext hotelContext, AppConfiguration appConfiguration)
        {
            _parser = new Parser(appConfiguration);
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
        public IActionResult GetAllRegions() =>
            Ok(_regionDbManager.ReturnAllRegions());

        [HttpDelete("{value}")]
        public IActionResult DeleteRegion(int value)
        {

            if (_validation.RegionExists(value))
            {
                _regionDbManager.DeleteRegion(value);
                return Ok($"The region with value {value} is successfully removed");
            }
            return NotFound("There is no region with that value");
        }

        [HttpGet("Hotels")]
        public IActionResult GetAllHotels()
        {
            var regions = _regionDbManager.ReturnAllRegions();

            var scandicHotels = _parser.SortTextFilesByDate();
            var bestWesternHotels = _parser.SortJsonFilesByDate();

            var scandicTextFile = _parser.SplitStringByLines(scandicHotels[0]);

            var hotels = new List<Hotel>();

            HotelAdder.AddScandicHotelsToHotelList(hotels, scandicTextFile);
            HotelAdder.AddWesternHotelsToHotelList(hotels, bestWesternHotels);

            AddHotelToCorrespondingRegion(regions, hotels);
            return Ok(regions);
        }






        private static void AddHotelToCorrespondingRegion(List<Region> regions, List<Hotel> hotels)
        {
            foreach (var hotel in hotels)
            {
                var region = regions.SingleOrDefault(r => r.Value == hotel.RegionValue);
                region?.Hotels.Add(hotel);
            }
        }
        private static void AddHotelToCorrespondingRegion(List<Region> regions, Hotel hotel)
        {
            var region = regions.SingleOrDefault(r => r.Value == hotel.RegionValue);
            region?.Hotels.Add(hotel);
        }



        [HttpGet("{regionValue:int}")]
        public IActionResult GetAllHotelsInRegion(int regionValue)
        {
            var regions = _regionDbManager.ReturnAllRegions();

            var scandicHotels = _parser.SortTextFilesByDate();
            var bestWesternHotels = _parser.SortJsonFilesByDate();

            var scandicTextFile = _parser.SplitStringByLines(scandicHotels[0]);

            var hotels = new List<Hotel>();

            HotelAdder.AddScandicHotelsToHotelList(hotels, scandicTextFile);
            ReadHotelsFromJson(hotels, bestWesternHotels);

            foreach (var hotel in hotels)
            {
                if (hotel.RegionValue != regionValue)
                {
                    regions.RemoveAll(x => x.Value == hotel.RegionValue);
                    continue;
                }

                AddHotelToCorrespondingRegion(regions, hotel);
            }

            return Ok(regions);
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