using System;
using System.Device.Location;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meteogy.Engine;

namespace Meteogy.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void TestGpsToMap1()
        {
            Calculator calculator = new Calculator();
            int[,] expected = new int[1, 3];
            int[,] actual = calculator.GpsToMap(1, 3, null);
            Assert.AreEqual(expected.Length, actual.Length);
            for (var i = 0; i < expected.GetLength(0); i++)
            {
                for (var j = 0; j < expected.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestGpsToMap2()
        {
            Calculator calculator = new Calculator();
            int[,] expected = new int[5, 2];
            int[,] actual = calculator.GpsToMap(5, 2, null);
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
