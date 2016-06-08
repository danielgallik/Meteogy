using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Meteogy.Models
{
    public class ObjectConvertor
    {
        public static object FromColorMatrix(Color[,] map)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            int[][][] result = new int[height][][];
            for (int i = 0; i < height; i++)
            {
                result[i] = new int[width][];
                for (int j = 0; j < width; j++)
                {
                    int x = width - j - 1;
                    int y = height - i - 1;
                    result[i][j] = new int[3] { map[x, y].R, map[x, y].G, map[x, y].B };
                }
            }
            return result;
        }

        public static object FromMeasurements(List<Measurement> measuremets)
        {
            object[] result = new object[measuremets.Count];
            for(int i = 0; i < measuremets.Count; i++)
            {
                result[i] = new
                {
                    Latitude = measuremets[i].Location.Latitude.ToString(),
                    Longitude = measuremets[i].Location.Longitude.ToString(),
                    Humidity = measuremets[i].Humidity.ToString(),
                    Temperature = measuremets[i].Temperature.ToString(),
                    Smog = measuremets[i].Smog.ToString(),
                    WindSpeed = measuremets[i].WindSpeed.ToString()
                };
            }

            return result;
        }
    }
}