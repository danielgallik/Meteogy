using System;
using System.Device.Location;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meteogy.Engine;
using System.Drawing;

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
            GeoCoordinate leftBot = new GeoCoordinate(4, 4);
            GeoCoordinate rightTop = new GeoCoordinate(6, 6);
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


        [TestMethod]
        public void TestCalculPosition()
        {
            GeoCoordinate leftBot = new GeoCoordinate(1, 1);
            GeoCoordinate rightTop = new GeoCoordinate(8, 5);
            MapBuilder builder = new MapBuilder(5, 8, leftBot, rightTop);
            Tuple<int, int> actual = builder.CalculPosition(new GeoCoordinate(3, 3));


            Tuple<int, int> expected = new Tuple<int, int>(2, 2);

            Assert.AreEqual(actual.Item1, expected.Item1);
            Assert.AreEqual(actual.Item2, expected.Item2);

        }

        [TestMethod]
        public void TestPositionAverage1()
        {
            GeoCoordinate leftBot = new GeoCoordinate(0, 0);
            GeoCoordinate rightTop = new GeoCoordinate(2, 3);
            MapBuilder builder = new MapBuilder(4, 3, leftBot, rightTop);
            builder.Insert(new GeoCoordinate(0, 0), 33);
            builder.Insert(new GeoCoordinate(0, 3), -10);
            builder.Insert(new GeoCoordinate(2, 2), 11);


            builder.SpreadPoints();
            double[,] actual = builder.GetMap();

            double[,] expected = new double[4, 3]
            {
                { -10, -5.49, 9.89 },
                { -6.44, 7.61, 11 },
                { 29.76, 19.63, 11.52 },
                { 33, 31.75, 20.55 }
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
        public void TestPositionAverage2()
        {
            GeoCoordinate leftBot = new GeoCoordinate(1, 1);
            GeoCoordinate rightTop = new GeoCoordinate(3, 4);
            MapBuilder builder = new MapBuilder(4, 3, leftBot, rightTop);
            builder.Insert(new GeoCoordinate(1, 1), 33);
            builder.Insert(new GeoCoordinate(1, 4), -10);
            builder.Insert(new GeoCoordinate(3, 3), 11);


            builder.SpreadPoints();
            double[,] actual = builder.GetMap();

            double[,] expected = new double[4, 3]
            {
                { -10, -5.49, 9.89 },
                { -6.44, 7.61, 11 },
                { 29.76, 19.63, 11.52 },
                { 33, 31.75, 20.55 }
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
        public void TestPositionAverage3()
        {
            GeoCoordinate leftBot = new GeoCoordinate(0, 0);
            GeoCoordinate rightTop = new GeoCoordinate(10, 15);
            MapBuilder builder = new MapBuilder(4, 3, leftBot, rightTop);
            builder.Insert(new GeoCoordinate(0, 0), 33);
            builder.Insert(new GeoCoordinate(0, 15), -10);
            builder.Insert(new GeoCoordinate(10, 10), 11);


            builder.SpreadPoints();
            double[,] actual = builder.GetMap();

            double[,] expected = new double[4, 3]
            {
                { -10, -5.49, 9.89 },
                { -6.44, 7.61, 11 },
                { 29.76, 19.63, 11.52 },
                { 33, 31.75, 20.55 }
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
        public void TestPositionAverage4()
        {
            GeoCoordinate leftBot = new GeoCoordinate(0, 0);
            GeoCoordinate rightTop = new GeoCoordinate(29, 29);
            MapBuilder builder = new MapBuilder(30, 30, leftBot, rightTop);
            builder.Insert(new GeoCoordinate(18, 12), 25);
            builder.Insert(new GeoCoordinate(10, 4), 20);
            builder.Insert(new GeoCoordinate(2, 9), 27);
            builder.Insert(new GeoCoordinate(3, 22), 23);
            builder.Insert(new GeoCoordinate(1, 1), 43);


            builder.SpreadPoints();
            double[,] actual = builder.GetMap();

            double[,] expected = new double[30, 30]
            {
                { 23.18, 23.16, 23.14, 23.13, 23.14, 23.16, 23.18, 23.22, 23.28, 23.35, 23.44, 23.55, 23.68, 23.81, 23.95, 24.09, 24.22, 24.34, 24.45, 24.54, 24.62, 24.69, 24.75, 24.8, 24.84, 24.88, 24.91, 24.94, 24.97, 24.99 },
                { 23.13, 23.11, 23.09, 23.09, 23.09, 23.11, 23.13, 23.17, 23.23, 23.3, 23.4, 23.52, 23.65, 23.8, 23.95, 24.1, 24.24, 24.36, 24.47, 24.57, 24.64, 24.71, 24.76, 24.81, 24.85, 24.88, 24.91, 24.94, 24.97, 24.99 },
                { 23.09, 23.07, 23.05, 23.05, 23.05, 23.07, 23.09, 23.13, 23.18, 23.26, 23.36, 23.49, 23.63, 23.8, 23.96, 24.12, 24.27, 24.4, 24.51, 24.6, 24.67, 24.73, 24.78, 24.83, 24.86, 24.89, 24.92, 24.94, 24.97, 24.99 },
                { 23.06, 23.04, 23.03, 23.03, 23.03, 23.04, 23.06, 23.09, 23.15, 23.23, 23.33, 23.47, 23.63, 23.81, 23.99, 24.16, 24.32, 24.45, 24.55, 24.64, 24.71, 24.76, 24.81, 24.84, 24.87, 24.9, 24.92, 24.95, 24.97, 24.99 },
                { 23.04, 23.02, 23.01, 23.01, 23.01, 23.02, 23.04, 23.07, 23.12, 23.21, 23.32, 23.47, 23.65, 23.84, 24.04, 24.22, 24.38, 24.51, 24.61, 24.69, 24.75, 24.8, 24.83, 24.86, 24.89, 24.91, 24.93, 24.95, 24.97, 24.99 },
                { 23.02, 23.01, 23, 23, 23, 23.01, 23.02, 23.05, 23.11, 23.2, 23.32, 23.49, 23.69, 23.9, 24.11, 24.3, 24.45, 24.58, 24.67, 24.74, 24.79, 24.83, 24.86, 24.88, 24.9, 24.92, 24.94, 24.95, 24.97, 24.98 },
                { 23.02, 23, 23, 23, 23, 23, 23.02, 23.05, 23.11, 23.2, 23.35, 23.53, 23.75, 23.99, 24.21, 24.39, 24.54, 24.65, 24.73, 24.79, 24.83, 24.86, 24.88, 24.9, 24.91, 24.93, 24.94, 24.95, 24.97, 24.98 },
                { 23.02, 23, 23, 23, 23, 23, 23.02, 23.05, 23.12, 23.23, 23.4, 23.61, 23.85, 24.1, 24.32, 24.5, 24.63, 24.73, 24.79, 24.84, 24.87, 24.89, 24.9, 24.91, 24.93, 24.93, 24.94, 24.96, 24.97, 24.98 },
                { 23.03, 23.01, 23, 23, 23, 23.01, 23.03, 23.07, 23.16, 23.3, 23.49, 23.73, 23.99, 24.24, 24.46, 24.62, 24.73, 24.8, 24.85, 24.88, 24.9, 24.91, 24.92, 24.93, 24.94, 24.94, 24.95, 24.96, 24.97, 24.98 },
                { 23.07, 23.03, 23.01, 23.01, 23.01, 23.02, 23.06, 23.13, 23.24, 23.41, 23.63, 23.9, 24.16, 24.4, 24.59, 24.72, 24.81, 24.86, 24.89, 24.91, 24.93, 24.93, 24.94, 24.94, 24.94, 24.95, 24.95, 24.96, 24.96, 24.97 },
                { 23.17, 23.09, 23.06, 23.05, 23.05, 23.08, 23.14, 23.25, 23.4, 23.6, 23.84, 24.1, 24.35, 24.56, 24.71, 24.81, 24.87, 24.91, 24.93, 24.94, 24.95, 24.95, 24.95, 24.95, 24.95, 24.95, 24.95, 24.96, 24.96, 24.97 },
                { 23.44, 23.3, 23.23, 23.2, 23.21, 23.26, 23.35, 23.49, 23.66, 23.87, 24.1, 24.33, 24.53, 24.69, 24.8, 24.87, 24.92, 24.94, 24.95, 24.96, 24.96, 24.96, 24.96, 24.96, 24.96, 24.96, 24.96, 24.96, 24.96, 24.97 },
                { 24.02, 23.83, 23.71, 23.64, 23.64, 23.69, 23.78, 23.91, 24.06, 24.21, 24.37, 24.53, 24.67, 24.78, 24.87, 24.92, 24.95, 24.97, 24.97, 24.98, 24.98, 24.97, 24.97, 24.97, 24.96, 24.96, 24.96, 24.96, 24.96, 24.96 },
                { 24.99, 24.82, 24.67, 24.57, 24.5, 24.48, 24.49, 24.51, 24.54, 24.57, 24.61, 24.67, 24.75, 24.84, 24.9, 24.94, 24.97, 24.98, 24.99, 24.99, 24.99, 24.98, 24.98, 24.97, 24.97, 24.96, 24.96, 24.95, 24.95, 24.96 },
                { 26.04, 25.95, 25.86, 25.74, 25.61, 25.46, 25.32, 25.16, 25, 24.86, 24.76, 24.73, 24.78, 24.85, 24.92, 24.96, 24.98, 24.99, 24.99, 24.99, 24.99, 24.99, 24.98, 24.98, 24.97, 24.96, 24.95, 24.95, 24.95, 24.95 },
                { 26.73, 26.69, 26.64, 26.56, 26.43, 26.24, 25.99, 25.68, 25.34, 25.01, 24.78, 24.7, 24.74, 24.84, 24.92, 24.97, 24.99, 25, 25, 25, 25, 24.99, 24.99, 24.98, 24.97, 24.96, 24.95, 24.95, 24.95, 24.95 },
                { 27.01, 26.98, 26.95, 26.91, 26.83, 26.67, 26.4, 25.99, 25.49, 25, 24.66, 24.55, 24.64, 24.79, 24.91, 24.97, 24.99, 25, 25, 25, 25, 24.99, 24.99, 24.98, 24.97, 24.96, 24.95, 24.94, 24.94, 24.94 },
                { 27.08, 27.04, 27.02, 27, 26.97, 26.86, 26.6, 26.11, 25.44, 24.78, 24.34, 24.23, 24.4, 24.67, 24.87, 24.97, 24.99, 25, 25, 25, 25, 24.99, 24.99, 24.97, 24.96, 24.95, 24.94, 24.93, 24.93, 24.93 },
                { 27.06, 27.02, 27.01, 27.01, 27, 26.93, 26.67, 26.06, 25.16, 24.29, 23.75, 23.66, 23.96, 24.42, 24.77, 24.93, 24.99, 25, 25, 25, 25, 24.99, 24.98, 24.97, 24.95, 24.94, 24.93, 24.92, 24.92, 24.92 },
                { 27.05, 27.01, 27, 27, 27.01, 26.96, 26.64, 25.8, 24.57, 23.47, 22.87, 22.79, 23.18, 23.87, 24.49, 24.83, 24.95, 24.99, 24.99, 24.99, 24.99, 24.98, 24.97, 24.95, 24.94, 24.92, 24.91, 24.9, 24.9, 24.91 },
                { 27.05, 27, 27, 27, 27.01, 26.96, 26.5, 25.23, 23.58, 22.38, 21.81, 21.75, 22.13, 22.93, 23.87, 24.53, 24.82, 24.93, 24.96, 24.97, 24.96, 24.95, 24.94, 24.92, 24.91, 24.9, 24.89, 24.88, 24.89, 24.89 },
                { 27.14, 27.02, 27.01, 27.02, 27.05, 26.95, 26.13, 24.21, 22.32, 21.28, 20.88, 20.85, 21.12, 21.78, 22.82, 23.84, 24.46, 24.74, 24.85, 24.89, 24.9, 24.9, 24.89, 24.88, 24.87, 24.86, 24.85, 24.86, 24.86, 24.88 },
                { 27.67, 27.28, 27.16, 27.19, 27.26, 26.96, 25.41, 22.88, 21.17, 20.5, 20.3, 20.3, 20.47, 20.89, 21.7, 22.8, 23.75, 24.31, 24.59, 24.71, 24.77, 24.79, 24.8, 24.81, 24.81, 24.81, 24.81, 24.82, 24.84, 24.86 },
                { 30.06, 29.09, 28.57, 28.42, 28.26, 27.22, 24.53, 21.76, 20.49, 20.13, 20.06, 20.08, 20.17, 20.41, 20.94, 21.83, 22.84, 23.64, 24.14, 24.41, 24.55, 24.63, 24.68, 24.71, 24.73, 24.74, 24.76, 24.78, 24.81, 24.84 },
                { 35.99, 35.15, 34.03, 32.81, 31.2, 28.27, 24.08, 21.17, 20.2, 20.02, 20, 20.01, 20.06, 20.21, 20.57, 21.21, 22.07, 22.93, 23.58, 24, 24.25, 24.41, 24.51, 24.58, 24.63, 24.67, 24.71, 24.74, 24.78, 24.82 },
                { 41.09, 41.03, 40.34, 38.7, 35.66, 30.5, 24.48, 21.08, 20.14, 20.01, 20, 20, 20.04, 20.16, 20.43, 20.91, 21.61, 22.38, 23.06, 23.58, 23.93, 24.17, 24.33, 24.44, 24.53, 24.6, 24.65, 24.71, 24.75, 24.8 },
                { 42.69, 42.76, 42.56, 41.73, 39.27, 33.58, 25.96, 21.5, 20.24, 20.02, 20, 20.01, 20.06, 20.18, 20.42, 20.83, 21.4, 22.06, 22.7, 23.24, 23.64, 23.94, 24.16, 24.31, 24.43, 24.53, 24.6, 24.67, 24.73, 24.79 },
                { 42.97, 42.99, 42.95, 42.63, 41.15, 36.46, 28.44, 22.65, 20.64, 20.16, 20.07, 20.07, 20.14, 20.27, 20.52, 20.88, 21.37, 21.94, 22.51, 23.02, 23.44, 23.77, 24.02, 24.21, 24.35, 24.47, 24.57, 24.65, 24.72, 24.79 },
                { 42.99, 43, 42.99, 42.83, 41.84, 38.33, 31.25, 24.68, 21.61, 20.61, 20.32, 20.28, 20.34, 20.48, 20.72, 21.06, 21.49, 21.97, 22.47, 22.94, 23.34, 23.67, 23.93, 24.14, 24.3, 24.44, 24.55, 24.64, 24.73, 24.8 },
                { 42.98, 42.99, 42.98, 42.8, 41.96, 39.2, 33.51, 27.18, 23.28, 21.58, 20.94, 20.74, 20.73, 20.85, 21.06, 21.36, 21.72, 22.14, 22.56, 22.97, 23.34, 23.65, 23.91, 24.12, 24.29, 24.43, 24.55, 24.66, 24.75, 24.82 }
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
        public void TestPositionAverage5()
        {
            GeoCoordinate leftBot = new GeoCoordinate(0, 0);
            GeoCoordinate rightTop = new GeoCoordinate(10, 15);
            MapBuilder builder = new MapBuilder(4, 3, leftBot, rightTop);
            builder.Insert(new GeoCoordinate(0, 0), 0);
            builder.Insert(new GeoCoordinate(0, 15), -10);
            builder.Insert(new GeoCoordinate(10, 10), 11);


            builder.SpreadPoints();

            double[,] actual = builder.GetMap();

            double[,] expected = new double[4, 3]
            {
                { -10, -5.75, 9.71 },
                { -8.28, 6.59, 11 },
                { -0.17, 4.35, 10.27 },
                { 0, 0.32, 4.8 }
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
        public void ColorTest()
        {
            
            GeoCoordinate leftBot = new GeoCoordinate(0, 0);
            GeoCoordinate rightTop = new GeoCoordinate(20, 20);
            MapBuilder builder = new MapBuilder(3, 3, leftBot, rightTop);
            builder.Insert(new GeoCoordinate(0, 0), 10);
            builder.Insert(new GeoCoordinate(20, 20), 20);

            builder.Insert(new GeoCoordinate(0, 20), -20);
            builder.Insert(new GeoCoordinate(20, 0), -20);
            builder.Insert(new GeoCoordinate(10, 10), -20);
            builder.Insert(new GeoCoordinate(8, 5), -20);
            builder.Insert(new GeoCoordinate(6, 4), -20);
            builder.Insert(new GeoCoordinate(9, 7), -20);

            builder.SpreadPoints();

            Color[,] expected = new Color[3, 3]
            {
                { Color.FromArgb(255, 0, 170, 255), Color.FromArgb(255, 0, 255, 117), Color.FromArgb(255, 255, 170, 0) },
                { Color.FromArgb(255, 0, 255, 179), Color.FromArgb(255, 0, 170, 255), Color.FromArgb(255, 0, 255, 130) },
                { Color.FromArgb(255, 170, 255, 0), Color.FromArgb(255, 0, 170, 255), Color.FromArgb(255, 0, 170, 255) }
            };

            /*
            opacity - nulova priehlasnost je 255, to ani nie je treba meniť.
            maximalna hodnota - nemusi byt korektna s maximalnou hodnotou. Len urcuje stupen odtiena cervenej
            minimalna hodnota - nemusi byt korektna s minimalnou hodnotou. Len urcuje stupen odtiena modrej
            */
            Color[,] actual = builder.GetColorMap(255/* opacity */, 30/* maximalna hodnota */, -30/* minimalna hodnota */);

            for (var i = 0; i < actual.GetLength(0); i++)
            {
                for (var j = 0; j < actual.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j]);
                }
            }
            
        }
    }
}