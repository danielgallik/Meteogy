using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meteogy.Models;
using Meteogy.Engine;

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
            //List<Location> locations = DbQueries.GetLocations(form);

            var result = new double[3][];
            result[0] = new double[5] { 0, 1, 2, 3, 4 };
            result[1] = new double[5] { 5, 6, 7, 8, 9 };
            result[2] = new double[5] { 10, 11, 12, 13, 14 };
            return Json(new { status = 200, message = "Success", data = result });
        }
    }
}