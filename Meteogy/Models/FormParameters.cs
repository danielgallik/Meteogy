using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Meteogy.Models
{
    public class FormParameters
    {
        public Bounds Bounds { get; set; }
        public Parameter Parameter { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public DateTime GetStartDate { get { return DateTime.Parse(StartDate, CultureInfo.InvariantCulture); } }
        public DateTime GetEndDate { get { return DateTime.Parse(EndDate, CultureInfo.InvariantCulture); } }
    }
}