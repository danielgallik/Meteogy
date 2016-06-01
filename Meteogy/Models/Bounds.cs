using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Meteogy.Models
{
    public class Bounds
    {
        public string North { get; set; }
        public string East { get; set; }
        public string South { get; set; }
        public string West { get; set; }
        public decimal GetNorth { get { return Convert.ToDecimal(North, CultureInfo.InvariantCulture); } }
        public decimal GetEast { get { return Convert.ToDecimal(East, CultureInfo.InvariantCulture); } }
        public decimal GetSouth { get { return Convert.ToDecimal(South, CultureInfo.InvariantCulture); } }
        public decimal GetWest { get { return Convert.ToDecimal(West, CultureInfo.InvariantCulture); } }
    }
}