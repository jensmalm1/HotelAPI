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
        private readonly AppConfiguration _appConfiguration;
        private readonly RegionDbManager _regionDbManager;
        private readonly Validation _validation;

        public RegionController(HotelContext hotelContext, AppConfiguration appConfiguration)
        {
            _regionDbManager = new RegionDbManager(hotelContext);
            _validation = new Validation(_regionDbManager);
            _regionDbManager.EnsureDatabaseCreated();
            _appConfiguration = appConfiguration;
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

            var scandicHotels = SortTextFilesByDate();
            var bestWesternHotels = SortJsonFilesByDate();

            var scandicTextFile = SplitStringByLines(scandicHotels[0]);

            var hotels = new List<Hotel>();

            ReadHotelsFromStringList(hotels, scandicTextFile);
            ReadHotelsFromJson(hotels, bestWesternHotels);

            AddHotelToCorrespondingRegion(regions, hotels);
            return Ok(regions);
        }

        private List<string> SplitStringByLines(String textFile) =>
            System.IO.File.ReadAllText($"{textFile}").Split('\n').ToList();

        private List<string> SortJsonFilesByDate() =>
            Directory.GetFiles(_appConfiguration.ImportPath, "*.json").OrderByDescending(x => x).ToList();

        private List<string> SortTextFilesByDate() =>
            Directory.GetFiles(_appConfiguration.ImportPath, "*.txt").OrderByDescending(x => x).ToList();


        private void ReadHotelsFromJson(List<Hotel> hotels, List<string> jsonList)
        {
            using (StreamReader fi = System.IO.File.OpenText(jsonList[0]))
            {
                var fileContent = fi.ReadToEnd();
                var hotelsFromJson = JArray.Parse(fileContent).ToObject<List<Hotel>>().ToList();

                hotels.AddRange(hotelsFromJson); 
            }
        }

        private static void AddHotelToCorrespondingRegion(List<Region> regions, List<Hotel> hotels)
        {
            foreach (var hotel in hotels)
            {
                var region = regions.SingleOrDefault(r => r.Value == hotel.RegionValue);
                region?.Hotels.Add(hotel);
            }
        }

        private static void ReadHotelsFromStringList(List<Hotel> hotels, List<string> scandicTextFile)
        {
            foreach (var line in scandicTextFile)
            {
                var hotel = new Hotel();
                var test = line.Split(',');

                hotel.RegionValue = Convert.ToInt32(test[0]);
                hotel.Name = test[1];
                hotel.Rooms = Convert.ToInt32(test[2]);
                hotels.Add(hotel);
            }
        }

        private static void AddHotelToCorrespondingRegion_BasedOnRegion(List<Region> regions, List <Hotel> hotels)
        {
            foreach (var region in regions)
            {
                foreach (var hotel in hotels)

                if (hotel.RegionValue == region.Value)
                {
                    region.Hotels.Add(hotel);
                }
            }
        }

        [HttpGet("{regionValue:int}")]
        public IActionResult GetAllHotelsInRegion(int regionValue)
        {
            const string path = @"C:\Project\HotelAPI\HotelAPI.App";

            var hotels = new List<Hotel>();

            var regions = _regionDbManager.ReturnSpecificRegion(regionValue);

            var files = System.IO.Directory.GetFiles(path, "*.txt").OrderByDescending(x => x).ToList();

            var input = System.IO.File.ReadAllText($"{files[0]}").Split('\n').ToList();

            foreach (var line in input)
            {
                var hotel = new Hotel();
                var test = line.Split(',');

                var regionId = Convert.ToInt32(test[0]);
                if (regionId != regionValue)
                    continue;

                hotel.Name = test[1];
                hotel.Rooms = Convert.ToInt32(test[2]);
                if (regionValue == regionId)
                {
                    hotels.Add(hotel);
                }

                foreach (var region in regions)
                {
                    if (regionValue == region.Value)
                    {
                        region.Hotels.Add(hotel);
                    }
                }
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