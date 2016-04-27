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
                { -10, 2.14, 7.41 },
                { 6, 9.27, 11 },
                { 17.27, 14, 12.32 },
                { 33, 21.58, 16.67 }
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
                { -10, 2.14, 7.41 },
                { 6, 9.27, 11 },
                { 17.27, 14, 12.32 },
                { 33, 21.58, 16.67 }
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
                { -10, 2.14, 7.41 },
                { 6, 9.27, 11 },
                { 17.27, 14, 12.32 },
                { 33, 21.58, 16.67 }
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
                { 26.56, 26.46, 26.18, 25.88, 25.97, 26.03, 26.06, 26.07, 26.07, 26.04, 26, 26.05, 26.08, 26.11, 26.13, 26.14, 26.15, 26.16, 26.15, 26.19, 26.23, 26.26, 26.3, 26.33, 26.36, 26.38, 26.41, 26.44, 26.46, 26.48,  },
                { 26.46, 26.34, 26.04, 25.72, 25.84, 25.92, 25.97, 25.99, 25.99, 25.97, 25.94, 25.99, 26.03, 26.06, 26.08, 26.1, 26.11, 26.12, 26.11, 26.15, 26.19, 26.23, 26.26, 26.3, 26.33, 26.36, 26.38, 26.41, 26.44, 26.46,  },
                { 26.34, 26.2, 25.87, 25.51, 25.67, 25.78, 25.85, 25.89, 25.91, 25.9, 25.87, 25.93, 25.97, 26.01, 26.03, 26.05, 26.07, 26.07, 26.07, 26.11, 26.15, 26.19, 26.23, 26.26, 26.3, 26.33, 26.36, 26.38, 26.41, 26.44,  },
                { 26.2, 26.03, 25.67, 25.26, 25.47, 25.62, 25.72, 25.78, 25.81, 25.82, 25.8, 25.86, 25.91, 25.95, 25.98, 26, 26.02, 26.03, 26.03, 26.07, 26.11, 26.15, 26.19, 26.23, 26.26, 26.3, 26.33, 26.36, 26.38, 26.41,  },
                { 26.03, 25.82, 25.41, 24.93, 25.22, 25.43, 25.57, 25.66, 25.7, 25.72, 25.71, 25.78, 25.84, 25.89, 25.92, 25.95, 25.97, 25.98, 25.98, 26.03, 26.07, 26.11, 26.15, 26.19, 26.23, 26.26, 26.3, 26.33, 26.36, 26.38,  },
                { 25.82, 25.55, 25.07, 24.5, 24.9, 25.18, 25.38, 25.5, 25.58, 25.62, 25.62, 25.7, 25.77, 25.82, 25.86, 25.89, 25.91, 25.93, 25.93, 25.98, 26.03, 26.07, 26.11, 26.15, 26.19, 26.23, 26.26, 26.3, 26.33, 26.36,  },
                { 25.55, 25.21, 24.62, 23.9, 24.48, 24.87, 25.14, 25.32, 25.43, 25.49, 25.51, 25.61, 25.68, 25.75, 25.8, 25.83, 25.86, 25.87, 25.87, 25.93, 25.98, 26.03, 26.07, 26.11, 26.15, 26.19, 26.23, 26.26, 26.3, 26.33,  },
                { 25.21, 24.73, 23.98, 23, 23.89, 24.46, 24.84, 25.09, 25.25, 25.35, 25.39, 25.5, 25.59, 25.67, 25.72, 25.77, 25.79, 25.81, 25.82, 25.87, 25.93, 25.98, 26.03, 26.07, 26.11, 26.15, 26.19, 26.23, 26.26, 26.3,  },
                { 25.71, 25.36, 24.75, 23.97, 24.58, 24.98, 25.24, 25.4, 25.5, 25.54, 25.54, 25.63, 25.7, 25.76, 25.8, 25.83, 25.85, 25.85, 25.84, 25.9, 25.96, 26.01, 26.06, 26.1, 26.14, 26.18, 26.22, 26.26, 26.29, 26.32,  },
                { 26.14, 25.89, 25.37, 24.73, 25.14, 25.41, 25.57, 25.66, 25.7, 25.7, 25.66, 25.73, 25.79, 25.83, 25.86, 25.88, 25.88, 25.88, 25.85, 25.92, 25.97, 26.03, 26.07, 26.12, 26.16, 26.2, 26.24, 26.28, 26.31, 26.35,  },
                { 26.52, 26.33, 25.88, 25.33, 25.6, 25.76, 25.85, 25.88, 25.87, 25.83, 25.75, 25.81, 25.86, 25.89, 25.91, 25.91, 25.91, 25.89, 25.85, 25.92, 25.98, 26.03, 26.08, 26.13, 26.18, 26.22, 26.26, 26.3, 26.33, 26.36,  },
                { 26.85, 26.72, 26.3, 25.82, 25.98, 26.06, 26.09, 26.07, 26.01, 25.93, 25.82, 25.87, 25.91, 25.93, 25.94, 25.93, 25.91, 25.88, 25.83, 25.9, 25.97, 26.03, 26.09, 26.14, 26.18, 26.23, 26.27, 26.31, 26.34, 26.38,  },
                { 27.13, 27.05, 26.65, 26.23, 26.3, 26.31, 26.28, 26.22, 26.13, 26.01, 25.86, 25.91, 25.94, 25.95, 25.95, 25.94, 25.9, 25.86, 25.79, 25.87, 25.95, 26.01, 26.07, 26.13, 26.18, 26.23, 26.27, 26.31, 26.35, 26.38,  },
                { 27.39, 27.34, 26.95, 26.56, 26.57, 26.53, 26.45, 26.35, 26.22, 26.06, 25.89, 25.93, 25.95, 25.96, 25.95, 25.92, 25.88, 25.81, 25.72, 25.82, 25.91, 25.98, 26.05, 26.11, 26.17, 26.22, 26.27, 26.31, 26.35, 26.39,  },
                { 27.62, 27.58, 27.2, 26.84, 26.79, 26.71, 26.59, 26.45, 26.28, 26.1, 25.89, 25.93, 25.95, 25.95, 25.93, 25.89, 25.83, 25.74, 25.62, 25.75, 25.85, 25.94, 26.02, 26.09, 26.15, 26.2, 26.25, 26.3, 26.34, 26.38,  },
                { 27.81, 27.8, 27.4, 27.08, 26.98, 26.86, 26.71, 26.53, 26.33, 26.11, 25.87, 25.91, 25.93, 25.92, 25.9, 25.84, 25.76, 25.64, 25.49, 25.64, 25.77, 25.88, 25.97, 26.05, 26.12, 26.18, 26.23, 26.29, 26.33, 26.38,  },
                { 27.98, 27.97, 27.55, 27.27, 27.14, 26.99, 26.8, 26.59, 26.36, 26.1, 25.83, 25.87, 25.89, 25.88, 25.84, 25.77, 25.66, 25.5, 25.29, 25.5, 25.66, 25.79, 25.9, 25.99, 26.07, 26.14, 26.21, 26.26, 26.31, 26.36,  },
                { 28.12, 28.09, 27.64, 27.41, 27.27, 27.09, 26.87, 26.63, 26.36, 26.07, 25.75, 25.81, 25.83, 25.81, 25.76, 25.66, 25.51, 25.3, 25, 25.3, 25.51, 25.67, 25.81, 25.92, 26.01, 26.09, 26.17, 26.23, 26.29, 26.34,  },
                { 28.24, 28.18, 27.67, 27.54, 27.41, 27.22, 26.98, 26.71, 26.4, 26.06, 25.7, 25.79, 25.85, 25.86, 25.84, 25.78, 25.68, 25.53, 25.31, 25.53, 25.69, 25.83, 25.94, 26.03, 26.12, 26.19, 26.25, 26.31, 26.37, 26.41,  },
                { 28.3, 28.15, 27.52, 27.57, 27.5, 27.32, 27.07, 26.76, 26.41, 26.02, 25.58, 25.73, 25.82, 25.88, 25.89, 25.87, 25.8, 25.7, 25.54, 25.71, 25.84, 25.96, 26.05, 26.14, 26.21, 26.28, 26.34, 26.39, 26.44, 26.48,  },
                { 28.23, 27.86, 27, 27.45, 27.51, 27.37, 27.12, 26.79, 26.39, 25.93, 25.4, 25.61, 25.76, 25.85, 25.9, 25.91, 25.89, 25.82, 25.71, 25.85, 25.96, 26.06, 26.15, 26.22, 26.29, 26.35, 26.41, 26.46, 26.5, 26.54,  },
                { 28.85, 28.69, 27.79, 27.91, 27.8, 27.54, 27.19, 26.75, 26.25, 25.67, 25, 25.34, 25.57, 25.73, 25.84, 25.89, 25.91, 25.88, 25.82, 25.94, 26.04, 26.13, 26.21, 26.28, 26.35, 26.41, 26.46, 26.51, 26.55, 26.6,  },
                { 29.53, 29.56, 28.55, 28.38, 28.1, 27.7, 27.22, 26.67, 26.03, 25.29, 24.43, 24.94, 25.3, 25.55, 25.72, 25.83, 25.89, 25.9, 25.88, 25.99, 26.09, 26.18, 26.26, 26.33, 26.4, 26.46, 26.51, 26.56, 26.6, 26.64,  },
                { 30.29, 30.52, 29.33, 28.89, 28.41, 27.85, 27.22, 26.5, 25.68, 24.72, 23.56, 24.36, 24.9, 25.27, 25.53, 25.71, 25.82, 25.89, 25.9, 26.02, 26.12, 26.21, 26.29, 26.37, 26.43, 26.49, 26.55, 26.6, 26.64, 26.68,  },
                { 31.17, 31.67, 30.2, 29.46, 28.74, 27.97, 27.13, 26.2, 25.13, 23.84, 22.22, 23.5, 24.32, 24.87, 25.25, 25.52, 25.7, 25.82, 25.89, 26.01, 26.12, 26.22, 26.31, 26.39, 26.46, 26.52, 26.58, 26.63, 26.68, 26.72,  },
                { 32.24, 33.1, 31.23, 30.11, 29.09, 28.04, 26.93, 25.69, 24.23, 22.43, 20, 22.17, 23.45, 24.28, 24.84, 25.24, 25.51, 25.7, 25.82, 25.97, 26.1, 26.21, 26.3, 26.39, 26.47, 26.54, 26.6, 26.65, 26.7, 26.75,  },
                { 34.03, 35.48, 33.1, 31.62, 30.41, 29.27, 28.14, 26.96, 25.67, 24.19, 22.38, 23.74, 24.6, 25.17, 25.57, 25.84, 26.02, 26.14, 26.21, 26.33, 26.44, 26.53, 26.61, 26.68, 26.74, 26.8, 26.85, 26.89, 26.93, 26.97,  },
                { 36.27, 38.64, 35.48, 33.48, 31.96, 30.63, 29.41, 28.21, 26.98, 25.66, 24.16, 25.04, 25.61, 25.99, 26.25, 26.42, 26.53, 26.59, 26.62, 26.71, 26.8, 26.87, 26.93, 26.98, 27.03, 27.07, 27.11, 27.14, 27.17, 27.2,  },
                { 39.15, 43, 38.64, 35.82, 33.82, 32.2, 30.81, 29.52, 28.27, 27, 25.65, 26.2, 26.55, 26.78, 26.93, 27.01, 27.05, 27.06, 27.04, 27.11, 27.16, 27.21, 27.26, 27.29, 27.33, 27.35, 27.38, 27.4, 27.42, 27.44,  },
                { 36.89, 39.15, 36.27, 34.32, 32.83, 31.57, 30.44, 29.38, 28.35, 27.31, 26.21, 26.57, 26.81, 26.97, 27.06, 27.12, 27.14, 27.13, 27.11, 27.16, 27.21, 27.26, 27.29, 27.33, 27.35, 27.38, 27.4, 27.42, 27.44, 27.45,  },

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
                { -10, -2.57, 3.53 },
                { -2.25, 3.27, 11 },
                { -0.73, 1.63, 5.37 },
                { 0, 0.74, 2.92 }
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