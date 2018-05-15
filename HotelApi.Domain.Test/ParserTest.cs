using System;
using System.Collections.Generic;
using HotelAPI.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HotelApi.Domain.Test
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void Name()
        {
            var appConfiguration = new AppConfiguration();
            appConfiguration.ImportPath = "C:\\project\\HotelApi\\HotelApi.Domain.Test";
            var parser = new Parser(appConfiguration);

            var jsonFiles = parser.SortJsonFilesByDate();

            var actual = jsonFiles[0];
            var expected = "C:\\project\\HotelApi\\HotelApi.Domain.Test\\BestWestern-2018-05-15.json";

            Assert.AreEqual(expected, actual);


        }
    }
}
