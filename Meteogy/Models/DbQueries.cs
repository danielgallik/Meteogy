using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meteogy.Models
{
    public class DbQueries
    {
        public static List<Location> GetLocations(FormParameters form)
        {
            var dbContext = new DatabaseEntities();
            var location = from l in dbContext.Location select l;
            return location.ToList();
        }
    }
}