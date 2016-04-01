using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.Collections;

namespace Meteogy.Engine
{
    public class MapBuilder
    {
        private double[,] map;
        private GeoCoordinate[] corners;
        private Dictionary<Tuple<int, int>, List<double>> points = new Dictionary<Tuple<int, int>, List<double>>();


        private int MIN_WIDTH = 1;
        private int MIN_HEIGHT = 1;


        public double[,] GetMap()
        {
            return map;
        }

        public MapBuilder(int width, int height, GeoCoordinate leftBot, GeoCoordinate rightTop)
        {
            width = (width < MIN_WIDTH) ? MIN_WIDTH : width;
            height = (width < MIN_HEIGHT) ? MIN_HEIGHT : height;
            
            map = new double[width, height];
            
            corners = new GeoCoordinate[2];
            corners[0] = leftBot;
            corners[1] = rightTop;
        }

        public bool Insert(GeoCoordinate coordinate, double value)
        {
            if (coordinate.Latitude < corners[0].Latitude ||
                coordinate.Latitude > corners[1].Latitude ||
                coordinate.Longitude < corners[0].Longitude ||
                coordinate.Longitude > corners[1].Longitude)
            {
                return false;
            }
            
            double deltaX = corners[1].Latitude - corners[0].Latitude;
            double deltaY= corners[1].Longitude - corners[0].Longitude;

            double startX = coordinate.Latitude - corners[0].Latitude;
            double startY = coordinate.Longitude - corners[0].Longitude;

            deltaX += deltaX / map.GetLength(1);
            deltaY += deltaY / map.GetLength(0);

            double stepX = deltaX / map.GetLength(1);
            double stepY = deltaY / map.GetLength(0);

            int positionX = (int)Math.Round((startX / stepX), MidpointRounding.AwayFromZero);
            int positionY = (int)Math.Round((map.GetLength(0) - (startY / stepY)) - 1, MidpointRounding.AwayFromZero);
            
            Tuple<int, int> key = new Tuple<int, int>(positionY, positionX);
            
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
    }
}

