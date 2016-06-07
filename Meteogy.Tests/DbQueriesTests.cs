using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meteogy.Models;

namespace Meteogy.Tests
{
    /// <summary>
    /// Summary description for DbQueriesTests
    /// </summary>
    [TestClass]
    public class DbQueriesTests
    {
        public DbQueriesTests()
        {
            string appDataPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Meteogy\\App_Data");
            AppDomain.CurrentDomain.SetData("DataDirectory", appDataPath);
        }

        [TestMethod]
        public void SelectLocationsTest()
        {
            var actual = DbQueries.GetLocations();
            Assert.AreEqual(5, actual.Count);
        }

        [TestMethod]
        public void SelectAllMeasuremetsTest()
        {
            var form = new FormParameters()
            {
                Bounds = new Bounds() {  East = "90", North = "90", South = "0", West = "0"},
                Parameter = Parameter.Humidity,
                StartDate = "1/1/2015",
                EndDate = "1/1/2017"
            };
            var actual = DbQueries.GetMeasurements(form);
            Assert.AreEqual(5, actual.Count);
        }
    }
}
