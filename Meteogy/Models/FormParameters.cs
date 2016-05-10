using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meteogy.Models
{
    public class FormParameters
    {
        public Bounds Bounds { get; set; }
        public Parameter Parameter { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}