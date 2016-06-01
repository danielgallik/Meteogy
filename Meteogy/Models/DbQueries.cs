using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Meteogy.Models
{
    public class DbQueries
    {
        public static List<Location> GetLocations(FormParameters form)
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
                               join l in dbContext.Location
                               on m.LocationId equals l.Id
                               where m.DateTime >= form.GetStartDate && m.DateTime < form.GetEndDate
                               && l.Latitude < form.Bounds.GetNorth && l.Latitude > form.Bounds.GetSouth
                               && l.Longitude < form.Bounds.GetEast && l.Latitude > form.Bounds.GetWest
                               select m;
            return measurements.ToList();
        }
    }
}