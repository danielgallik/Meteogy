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
        public void CreateMap()
        {
            GeoCoordinate leftBot = new GeoCoordinate(0, 0);
            GeoCoordinate rightTop = new GeoCoordinate(1, 1);
            MapBuilder builder = new MapBuilder(3, 3, leftBot, rightTop);
            double[,] actual = builder.GetMap();

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

        [TestMethod]
        public void CreateLengthMap()
        {
            GeoCoordinate leftBot = new GeoCoordinate(0, 0);
            GeoCoordinate rightTop = new GeoCoordinate(1, 1);
            MapBuilder builder = new MapBuilder(3, 4, leftBot, rightTop);
            double[,] actual = builder.GetMap();

            double[,] expected = new double[3, 4]
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
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

        [TestMethod]
        public void InsertOnPosition()
        {
            GeoCoordinate leftBot = new GeoCoordinate(1, 1);
            GeoCoordinate rightTop = new GeoCoordinate(4, 4);
            MapBuilder builder = new MapBuilder(9, 9, leftBot, rightTop);

            GeoCoordinate point1 = new GeoCoordinate(5, 5);
            Assert.AreEqual(builder.Insert(point1, 0), false);

            GeoCoordinate point2 = new GeoCoordinate(4, 4);
            Assert.AreEqual(builder.Insert(point2, -20), true);

            GeoCoordinate point3 = new GeoCoordinate(1, 8);
            Assert.AreEqual(builder.Insert(point3, 0), false);

            GeoCoordinate point4 = new GeoCoordinate(6, 1);
            Assert.AreEqual(builder.Insert(point4, 0), false);

            GeoCoordinate point5 = new GeoCoordinate(6, 2);
            Assert.AreEqual(builder.Insert(point5, 0), false);
        }
        
        [TestMethod]
        public void TestMap1()
        {
            GeoCoordinate leftBot = new GeoCoordinate(4,4);
            GeoCoordinate rightTop = new GeoCoordinate(6,6);
            MapBuilder builder = new MapBuilder(3, 3, leftBot, rightTop);
            builder.Insert(new GeoCoordinate(4, 6), 5);
            builder.Insert(new GeoCoordinate(6, 5), 2);

            double[,] actual = builder.GetMap();

            double[,] expected = new double[3, 3]
            {
                { 5, 0, 0 },
                { 0, 0, 2 },
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

        [TestMethod]
        public void TestScaleMap1()
        {
            GeoCoordinate leftBot = new GeoCoordinate(4, 4);
            GeoCoordinate rightTop = new GeoCoordinate(6, 6);
            MapBuilder builder = new MapBuilder(6, 6, leftBot, rightTop);
            builder.Insert(new GeoCoordinate(4, 6), 5);
            builder.Insert(new GeoCoordinate(6, 5), 2);
            double[,] actual = builder.GetMap();

            double[,] expected = new double[6, 6]
            {
                { 5, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 2 },
                { 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0 }
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


        
        [TestMethod]
        public void TestMorePointsScaleMap1()
        {
            GeoCoordinate leftBot = new GeoCoordinate(4, 4);
            GeoCoordinate rightTop = new GeoCoordinate(6, 6);
            MapBuilder builder = new MapBuilder(6, 6, leftBot, rightTop);
            builder.Insert(new GeoCoordinate(4, 4), 1);
            builder.Insert(new GeoCoordinate(4, 6), 2);
            builder.Insert(new GeoCoordinate(6, 4), 3);
            builder.Insert(new GeoCoordinate(6, 6), 4);
            builder.Insert(new GeoCoordinate(5, 5), 5);
            double[,] actual = builder.GetMap();

            double[,] expected = new double[6, 6]
            {
                { 2, 0, 0, 0, 0, 4 },
                { 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 5, 0, 0 },
                { 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0, 3 }
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


        [TestMethod]
        public void TestMorePointsScaleMap2()
        {
            GeoCoordinate leftBot = new GeoCoordinate(4, 4);
            GeoCoordinate rightTop = new GeoCoordinate(6, 6);
            MapBuilder builder = new MapBuilder(7, 7, leftBot, rightTop);
            builder.Insert(new GeoCoordinate(5, 5), 5);
            builder.Insert(new GeoCoordinate(5, 5), 10);
            double[,] actual = builder.GetMap();

            double[,] expected = new double[7, 7]
            {
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 7.5, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 }
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



        [TestMethod]
        public void TestMapInLength()
        {
            GeoCoordinate leftBot = new GeoCoordinate(4, 4);
            GeoCoordinate rightTop = new GeoCoordinate(6, 6);
            MapBuilder builder = new MapBuilder(2, 10, leftBot, rightTop);
            builder.Insert(new GeoCoordinate(4, 6), 4);
            builder.Insert(new GeoCoordinate(6, 4), 7);
            builder.Insert(new GeoCoordinate(6, 6), 3);
            builder.Insert(new GeoCoordinate(4, 4), 2);
            builder.Insert(new GeoCoordinate(5, 5), 1);


            double[,] actual = builder.GetMap();

            double[,] expected = new double[2, 10]
            {
                { 4, 0, 0, 0, 0, 1, 0, 0, 0, 3},
                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 7}
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

        [TestMethod]
        public void TestMapInLength2()
        {
            GeoCoordinate leftBot = new GeoCoordinate(1, 1);
            GeoCoordinate rightTop = new GeoCoordinate(8, 5);
            MapBuilder builder = new MapBuilder(5, 8, leftBot, rightTop);
            builder.Insert(new GeoCoordinate(1, 1), 4);
            builder.Insert(new GeoCoordinate(8, 5), 5);
            builder.Insert(new GeoCoordinate(3, 3), 8);
            builder.Insert(new GeoCoordinate(4.5, 3), 6);
            builder.Insert(new GeoCoordinate(4.4, 3), 2);

            double[,] actual = builder.GetMap();

            double[,] expected = new double[5, 8]
            {
                { 0, 0, 0, 0, 0, 0, 0, 5 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 8, 2, 6, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 4, 0, 0, 0, 0, 0, 0, 0 },
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

        [TestMethod]
        public void Avg()
        {
            GeoCoordinate leftBot = new GeoCoordinate(1, 1);
            GeoCoordinate rightTop = new GeoCoordinate(8, 5);
            MapBuilder builder = new MapBuilder(5, 8, leftBot, rightTop);
            builder.Insert(new GeoCoordinate(1, 1), 4);
            builder.Insert(new GeoCoordinate(8, 5), 5);

            builder.Insert(new GeoCoordinate(3, 3), 8);
            builder.Insert(new GeoCoordinate(3, 3), 4);
            builder.Insert(new GeoCoordinate(3, 3), 3);

            builder.Insert(new GeoCoordinate(4.5, 3), 6);
            builder.Insert(new GeoCoordinate(4.5, 3), 1);

            builder.Insert(new GeoCoordinate(4.4, 3), 2);
            builder.Insert(new GeoCoordinate(4.4, 3), 1);
            builder.Insert(new GeoCoordinate(4.4, 3), 8);

            double[,] actual = builder.GetMap();

            double[,] expected = new double[5, 8]
            {
                { 0, 0, 0, 0, 0, 0, 0, 5 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 5, 3.67, 3.5, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 4, 0, 0, 0, 0, 0, 0, 0 },
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
