using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meteogy.Models;
using Meteogy.Engine;
using System.Device.Location;
using System.Globalization;

namespace Meteogy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetColorMap(FormParameters form)
        {
            int width = 200;
            int height = 100;
            double south = Convert.ToDouble(form.Bounds.South, CultureInfo.InvariantCulture);
            double west = Convert.ToDouble(form.Bounds.West, CultureInfo.InvariantCulture);
            double north = Convert.ToDouble(form.Bounds.North, CultureInfo.InvariantCulture);
            double east = Convert.ToDouble(form.Bounds.East, CultureInfo.InvariantCulture);
            GeoCoordinate leftBot = new GeoCoordinate(south, west);
            GeoCoordinate rightTop = new GeoCoordinate(north, east);
            MapBuilder builder = new MapBuilder(width, height, leftBot, rightTop);

            var measurements = DbQueries.GetMeasurements(form);

            measurements.ForEach(
                x => builder.Insert(
                    new GeoCoordinate(
                        Convert.ToDouble(x.Location.Latitude), 
                        Convert.ToDouble(x.Location.Longitude)), 
                    x.Temperature.Value));

            builder.SpreadPoints();

            var map = builder.GetColorMap(255, 30, -30);
            int[][][] result = new int[height][][];
            for(int i = 0; i < height; i++)
            {
                result[i] = new int[width][];
                for (int j = 0; j < width; j++)
                {
                    int x = width - j - 1;
                    int y = height - i - 1;
                    result[i][j] = new int[3] { map[x, y].R, map[x, y].G , map[x, y].B };
                }
            }
            return Json(new { status = 200, message = "Success", data = result });
        }
    }
}