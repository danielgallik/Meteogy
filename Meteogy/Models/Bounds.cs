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

        public decimal GetNorthDecimal { get { return Convert.ToDecimal(North, CultureInfo.InvariantCulture); } }
        public decimal GetEastDecimal { get { return Convert.ToDecimal(East, CultureInfo.InvariantCulture); } }
        public decimal GetSouthDecimal { get { return Convert.ToDecimal(South, CultureInfo.InvariantCulture); } }
        public decimal GetWestDecimal { get { return Convert.ToDecimal(West, CultureInfo.InvariantCulture); } }

        public double GetNorthDouble { get { return Convert.ToDouble(North, CultureInfo.InvariantCulture); } }
        public double GetEastDouble { get { return Convert.ToDouble(East, CultureInfo.InvariantCulture); } }
        public double GetSouthDouble { get { return Convert.ToDouble(South, CultureInfo.InvariantCulture); } }
        public double GetWestDouble { get { return Convert.ToDouble(West, CultureInfo.InvariantCulture); } }
    }
}