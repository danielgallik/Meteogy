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
            GeoCoordinate leftBot = new GeoCoordinate(form.Bounds.GetSouthDouble, form.Bounds.GetWestDouble);
            GeoCoordinate rightTop = new GeoCoordinate(form.Bounds.GetNorthDouble, form.Bounds.GetEastDouble);
            MapBuilder builder = new MapBuilder(width, height, leftBot, rightTop);

            var measurements = DbQueries.GetMeasurements(form);

            foreach(Measurement item in measurements)
            {
                switch (form.Parameter)
                {
                    case Parameter.Humidity:
                        builder.Insert(
                            new GeoCoordinate(Convert.ToDouble(item.Location.Latitude), Convert.ToDouble(item.Location.Longitude)),
                            item.Humidity.Value);
                        break;
                    case Parameter.Smog:
                        builder.Insert(
                            new GeoCoordinate(Convert.ToDouble(item.Location.Latitude), Convert.ToDouble(item.Location.Longitude)),
                            item.Smog.Value);
                        break;
                    case Parameter.Temperature:
                        builder.Insert(
                            new GeoCoordinate(Convert.ToDouble(item.Location.Latitude), Convert.ToDouble(item.Location.Longitude)),
                            item.Temperature.Value);
                        break;
                    case Parameter.WindSpeed:
                        builder.Insert(
                            new GeoCoordinate(Convert.ToDouble(item.Location.Latitude), Convert.ToDouble(item.Location.Longitude)),
                            item.WindSpeed.Value);
                        break;
                    default:
                        return Json(new { status = 400, message = "Parameter is incorrect" });
                }
            }
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