using BugTracker.Models;
using BugTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class ChartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public JsonResult GetAllTicketPriorityData()
        {

            var data = new ChartData();
            foreach (var priority in db.TicketPriorities.ToList())
            {
                data.Labels.Add(priority.Name);
                data.Data.Add(db.Tickets.Where(t => t.TicketPriorityId == priority.Id).Count());
            }
            return Json(data);

        }


        public JsonResult GetAllTicketTypeData()
        {

            var data = new ChartData();
            foreach (var type in db.TicketTypes.ToList())
            {
                data.Labels.Add(type.Name);
                data.Data.Add(db.Tickets.Where(t => t.TicketTypeId == type.Id).Count());
            }
            return Json(data);

        }
    }
}