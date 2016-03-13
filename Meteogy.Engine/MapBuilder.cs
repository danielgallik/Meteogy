using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace Meteogy.Engine
{
    public class MapBuilder
    {
        private double[,] map;
        private GeoCoordinate[] corners;

        public double[,] GetMap()
        {
            return map;
        }

        public MapBuilder(int width, int height, GeoCoordinate leftBot, GeoCoordinate rightTop)
        {
            // vytvori inicializaciu mapy
        }

        public void Insert(GeoCoordinate coordinate, double value)
        {
            // vlozi hodnotu do prislusneho policka
        }
    }
}
