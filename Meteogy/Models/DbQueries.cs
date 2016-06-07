using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Meteogy.Models
{
    public class DbQueries
    {
        public static List<Location> GetLocations()
        {
            var dbContext = new DatabaseEntities();
            var location = from l
                           in dbContext.Location
                           select l;
            return location.ToList();
        }

        public static List<Measurement> GetMeasurements(FormParameters form)
        {
            var dbContext = new DatabaseEntities();
            var measurements = from m in dbContext.Measurement
                               where m.DateTime >= form.GetStartDate && m.DateTime < form.GetEndDate
                               && m.Location.Latitude < form.Bounds.GetNorthDecimal && m.Location.Latitude > form.Bounds.GetSouthDecimal
                               && m.Location.Longitude < form.Bounds.GetEastDecimal && m.Location.Latitude > form.Bounds.GetWestDecimal
                               group m by m.Location into g
                               select new
                               {
                                   Latitude = g.Key.Latitude,
                                   Longitude = g.Key.Longitude,
                                   Humidity = g.Average(x => x.Humidity),
                                   Temperature = g.Average(x => x.Temperature),
                                   Smog = g.Average(x => x.Smog),
                                   WindSpeed = g.Average(x => x.WindSpeed)
                               };

            var result = new List<Measurement>();
            foreach (var item in measurements)
            {
                result.Add(new Measurement()
                {
                    Location =  new Location() { Latitude = item.Latitude, Longitude = item.Longitude },
                    Humidity = item.Humidity,
                    Temperature = item.Temperature,
                    Smog = item.Smog,
                    WindSpeed = item.WindSpeed
                });
            }

            return result;
        }
    }
}