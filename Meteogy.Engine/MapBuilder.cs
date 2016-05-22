using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.Drawing;
using System.Collections;

namespace Meteogy.Engine
{
    public class MapBuilder
    {
        private double[,] map;
        private Color[,] colorMap;
        private GeoCoordinate[] corners;
        private Dictionary<Tuple<int, int>, List<double>> points = new Dictionary<Tuple<int, int>, List<double>>();


        private int MIN_WIDTH = 1;
        private int MIN_HEIGHT = 1;


        public double[,] GetMap()
        {
            return map;
        }

        public Color[,] GetColorMap(int opacity, double max, double min)
        {
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    colorMap[i,j] = ConvertToColor(map[i, j], opacity, max, min);
                }
            }

            return colorMap;
        }

        public MapBuilder(int width, int height, GeoCoordinate leftBot, GeoCoordinate rightTop)
        {
            width = (width < MIN_WIDTH) ? MIN_WIDTH : width;
            height = (width < MIN_HEIGHT) ? MIN_HEIGHT : height;

            map = new double[width, height];
            colorMap = new Color[width, height];

            corners = new GeoCoordinate[2];
            corners[0] = leftBot;
            corners[1] = rightTop;
        }

        public bool Insert(GeoCoordinate coordinate, double value)
        {
            if (!IsValidEntry(coordinate))
            {
                return false;
            }
            Tuple<int, int> key = CalculPosition(coordinate);
            if (!points.ContainsKey(key))
            {
                points.Add(key, new List<double>());
            }
            points[key].Add(value);
            foreach (KeyValuePair<Tuple<int, int>, List<double>> point in points)
            {
                Tuple<int, int> position = point.Key;
                List<double> values = point.Value;
                int countOfValues = values.Count;
                double average = Math.Round(point.Value.Take(countOfValues).Sum() / countOfValues, 2, MidpointRounding.AwayFromZero);
                map[position.Item1, position.Item2] = average;
            }
            return true;
        }

        public bool IsValidEntry(GeoCoordinate coordinate)
        {
            return (coordinate.Latitude >= corners[0].Latitude &&
                    coordinate.Latitude <= corners[1].Latitude &&
                    coordinate.Longitude >= corners[0].Longitude &&
                    coordinate.Longitude <= corners[1].Longitude);
        }

        public Tuple<int, int> CalculPosition(GeoCoordinate coordinate)
        {
            double deltaX = corners[1].Latitude - corners[0].Latitude;
            double deltaY = corners[1].Longitude - corners[0].Longitude;

            double startX = coordinate.Latitude - corners[0].Latitude;
            double startY = coordinate.Longitude - corners[0].Longitude;

            deltaX += deltaX / map.GetLength(1);
            deltaY += deltaY / map.GetLength(0);

            double stepX = deltaX / map.GetLength(1);
            double stepY = deltaY / map.GetLength(0);

            int positionX = (int)Math.Round((startX / stepX), MidpointRounding.AwayFromZero);
            int positionY = (int)Math.Round((map.GetLength(0) - (startY / stepY)) - 1, MidpointRounding.AwayFromZero);

            return new Tuple<int, int>(positionY, positionX);
        }

        public void SpreadPoints()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = CalculCorrectPoint(i, j);
                }
            }
        }

        public double CalculCorrectPoint(int i, int j)
        {
            double count = 0;
            double average = 0;
            foreach (KeyValuePair<Tuple<int, int>, List<double>> point in points)
            {
                Tuple<int, int> position = point.Key;
                List<double> values = point.Value;

                double distinct = Math.Pow(Math.Pow(Math.Abs(i - position.Item1), 2) + Math.Pow(Math.Abs(j - position.Item2), 2), 2);

                if (distinct == 0)
                {
                    return AverageOfSensor(values);
                }
                distinct = 1 / distinct;
                average += AverageOfSensor(values) * distinct;
                count += distinct;
            }

            if (count == 0) {
                return 0;
            }
            return Math.Round((average / count), 2, MidpointRounding.AwayFromZero);
        }

        public double AverageOfSensor(List<double> values)
        {
            if (values.Count() == 0)
            {
                return 0;
            }
            double average = 0;
            for (int i = 0; i < values.Count(); i++)
            {
                average += values[i];
            }
            return average / values.Count();
        }

        public Color ConvertToColor(double value, int opacity, double MAX, double MIN)
        {
            Color color = Color.FromArgb(255, 255, 255);

            double QUAD = Math.Abs(MAX - MIN) / 4;
            double STEP = 255 / QUAD;
            double valueColor;

            if (value >= MAX - QUAD)
            {
                valueColor = (value - MIN) - (QUAD * 3);
                color = Color.FromArgb(opacity, 255, 255 - (int)Math.Round(valueColor * STEP), 0);
            }
            else if (value >= MAX - QUAD * 2)
            {
                valueColor = (value - MIN) - (QUAD * 2);
                color = Color.FromArgb(opacity, (int)Math.Round(valueColor * STEP), 255, 0);
            }
            else if (value >= MAX - QUAD * 3)
            {
                valueColor = (value - MIN) - QUAD;
                color = Color.FromArgb(opacity, 0, 255, 255 - (int)Math.Round(valueColor * STEP));
            }
            else
            {
                color = Color.FromArgb(opacity, 0, (int)Math.Round((value - MIN) * STEP), 255);
            }

            return color;
        }
    }
}
