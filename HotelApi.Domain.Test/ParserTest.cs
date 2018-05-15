using System.Collections.Generic;
using HotelAPI.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HotelApi.Domain.Test
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void SplitStringByLines_ParseText_GetCorrectText()
        {
            var appConfiguration = new AppConfiguration();
            var parser = new Parser(appConfiguration);

            var textFile = "50,Scandic Rubinen,15\n" +
                           "50,Scandic Opalen,2\n" +
                           "60,Scandic Backadal,5\n" +
                           "70,Scandic Helsingborg North,2\n" +
                           "70,Scandic Skee North,1";

            var actual = parser.SplitStringByLines(textFile);
            var expected = new List<string>()
            {
                "50, Scandic Rubinen, 15",
                "50,Scandic Opalen,2",
                "60,Scandic Backadal,5",
                "70,Scandic Helsingborg North,2",
                "70,Scandic Skee North,1"

            };

            Assert.AreEqual(5, actual.Count);

            //[TestMethod]
            //public void SplitStringByLines_ParseText_GetCorrectText()
            //{
            //    var appConfiguration = new AppConfiguration();
            //    appConfiguration.ImportPath =
            //}
        }
    }
}
