using System;
using System.Device.Location;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meteogy.Engine;

namespace Meteogy.Tests
{
    [TestClass]
    public class MapBuilderTests
    {
        [TestMethod]
        public void TestConstructor1()
        {
            GeoCoordinate leftBot = new GeoCoordinate(0, 0);
            GeoCoordinate rightTop = new GeoCoordinate(1, 1);
            MapBuilder bulder = new MapBuilder(3, 3, leftBot, rightTop);
            double[,] actual = bulder.GetMap();

            double[,] expected = new double[3, 3] 
            { 
                { 0, 0, 0 }, 
                { 0, 0, 0 }, 
                { 0, 0, 0 }
            };

            Assert.AreEqual(expected.Length, actual.Length);
            for (var i = 0; i < expected.GetLength(0); i++)
            {
                for (var j = 0; j < expected.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j]);
                }
            }
        }
    }
}
