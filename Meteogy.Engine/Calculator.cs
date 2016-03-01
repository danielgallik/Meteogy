using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace Meteogy.Engine
{
    public class Calculator
    {
        public int[,] GpsToMap(int height, int width, List<GeoCoordinate> coordinates)
        {
            return new int[height, width];
        }
    }
}
