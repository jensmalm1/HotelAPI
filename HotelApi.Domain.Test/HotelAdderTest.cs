using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelAPI.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HotelApi.Domain.Test
{
    [TestClass]

    public class HotelAdderTest
    {

        [TestMethod]
        public void AddScandicHotelsToHotelList_AddsHotelFromString_HotelAdded()
        {
            var hotelAdder = new HotelAdder();

            var scandicTextFile = "50,Scandic Rubinen,15\n" +
                                  "50,Scandic Opalen,20\n" +
                                  "60,Scandic Backadal,5\n" +
                                  "70,Scandic Helsingborg North,2\n" +
                                  "70,Scandic Skee North,1";

            var hotelList = new List<Hotel>();
            hotelAdder.AddScandicHotelsToHotelList(hotelList, scandicTextFile);
            var expected = new Hotel
            {
                Name="Scandic Rubinen",
                RegionValue = 50,
                Rooms = 15
            };

            var actual = hotelList[0];

            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.RegionValue, actual.RegionValue);
            Assert.AreEqual(expected.Rooms, actual.Rooms);
            Assert.AreEqual(5, hotelList.Count);
        }
    }
}
