using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeachImageWebsite.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: Schedule
        public ActionResult Index()
        {

            List<DateTime> dates = GetDates(DateTime.Today);

            return View(dates);
        }

        public ActionResult PreviousSevenDays(DateTime currentDate)
        {

            DateTime previousDate = currentDate.AddDays(-7);
            List<DateTime> dates = GetDates(previousDate);

            return View("Index", dates);
        }

        public ActionResult NextSevenDays(DateTime currentDate)
        {

            DateTime nextDate = currentDate.AddDays(7);
            List<DateTime> dates = GetDates(nextDate);

            return View("Index", dates);
        }

        private List<DateTime> GetDates(DateTime startDate)
        {
            List<DateTime> dates = new List<DateTime>();


            for (int i = 0; i < 7; i++)
            {
                DateTime date = startDate.AddDays(i);
                dates.Add(date);
            }

            return dates;
        }
    }
}