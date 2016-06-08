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
            try
            {
                int width = 200;
                int height = 100;
                GeoCoordinate leftBot = new GeoCoordinate(form.Bounds.GetSouthDouble, form.Bounds.GetWestDouble);
                GeoCoordinate rightTop = new GeoCoordinate(form.Bounds.GetNorthDouble, form.Bounds.GetEastDouble);
                MapBuilder builder = new MapBuilder(width, height, leftBot, rightTop);

                var measurements = DbQueries.GetMeasurements(form);

                if (measurements.Count == 0)
                {
                    return Json(new { status = 204, message = "No measurement values found." });
                }

                foreach (Measurement item in measurements)
                {
                    switch (form.Parameter)
                    {
                        case Parameter.Humidity:
                            builder.Insert(new GeoCoordinate(Convert.ToDouble(item.Location.Latitude), Convert.ToDouble(item.Location.Longitude)), item.Humidity.Value * 100);
                            break;
                        case Parameter.Smog:
                            builder.Insert(new GeoCoordinate(Convert.ToDouble(item.Location.Latitude), Convert.ToDouble(item.Location.Longitude)), item.Smog.Value * 100);
                            break;
                        case Parameter.Temperature:
                            builder.Insert(new GeoCoordinate(Convert.ToDouble(item.Location.Latitude), Convert.ToDouble(item.Location.Longitude)), item.Temperature.Value);
                            break;
                        case Parameter.WindSpeed:
                            builder.Insert(new GeoCoordinate(Convert.ToDouble(item.Location.Latitude), Convert.ToDouble(item.Location.Longitude)), item.WindSpeed.Value);
                            break;
                    }
                }

                builder.SpreadPoints();

                double max = 0;
                double min = 0;
                string units = "";
                switch (form.Parameter)
                {
                    case Parameter.Humidity:
                    case Parameter.Smog:
                        max = 100;
                        min = 0;
                        units = "%";
                        break;
                    case Parameter.Temperature:
                        max = 50;
                        min = -30;
                        units = "°C";
                        break;
                    case Parameter.WindSpeed:
                        max = 200;
                        min = 0;
                        units = "km/h";
                        break;
                }

                var map = builder.GetColorMap(255, max, min);
                return Json(new
                {
                    status = 200,
                    message = "Success",
                    map = ObjectConvertor.FromColorMatrix(map),
                    legend = new
                    {
                        Parameter = form.Parameter.ToString(),
                        Min = String.Format("{0}{1}", min, units),
                        Max = String.Format("{0}{1}", max, units)
                    }
                });
            }
            catch
            {
                return Json(new { status = 400, message = "Undefined error." });
            }
        }
    }
}