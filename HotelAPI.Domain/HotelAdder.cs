using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace HotelAPI.Domain
{
    public class HotelAdder
    {
        public static void AddScandicHotelsToHotelList(List<Hotel> hotels, string scandicTextFile)
        {
        
            foreach (var line in scandicTextFile.Split('\n'))
            {
                var hotel = new Hotel();
                var test = line.Split(',');

                hotel.RegionValue = Convert.ToInt32(test[0]);
                hotel.Name = test[1];
                hotel.Rooms = Convert.ToInt32(test[2]);
                hotels.Add(hotel);
            }
        }

        public static void AddWesternHotelsToHotelList(List<Hotel> hotels, string westernJsonFile)
        {
            var hotelsFromJson = JArray.Parse(westernJsonFile).ToObject<List<Hotel>>().ToList();
            hotels.AddRange(hotelsFromJson);
        }
    }
}
