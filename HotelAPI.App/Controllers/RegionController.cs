﻿using System;
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

        [HttpPost("AddTest")]
        public IActionResult AddRegionTest(string name, int value)
        {
            var region = new Region();
            region.Name = name;
            region.Value = value;
            _regionDbManager.CreateRegion(region);
            return Ok(region);
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

        [HttpGet("count")]
        public IActionResult CountRegions()
        {
            var count = _regionDbManager.ReturnAllRegions().Count();
            return Ok(count);
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Hejsan");
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

        [HttpGet("Hotels")]
        public IActionResult GetAllHotels()
        {

            var regions = _regionDbManager.ReturnAllRegions();

            var scandicHotels = new List<string>();
            var bestWesternHotels = new List<string>();

            scandicHotels = System.IO.Directory.GetFiles(_appConfiguration.ImportPath, "*.txt").OrderByDescending(x => x).ToList();
            bestWesternHotels = System.IO.Directory.GetFiles(_appConfiguration.ImportPath, "*.json").OrderByDescending(x => x).ToList();

            var scandicTextFile = System.IO.File.ReadAllText($"{scandicHotels[0]}").Split('\n').ToList();

            var hotels = new List<Hotel>();
            ReadHotelsFromStringList(hotels, regions, scandicTextFile);
            ReadHotelsFromStringList(hotels, regions, bestWesternHotels);

            return Ok(regions);
        }

        private static void ReadHotelsFromStringList(List<Hotel> hotels, List<Region> regions, List<string> scandicTextFile)
        {
            foreach (var line in scandicTextFile)
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