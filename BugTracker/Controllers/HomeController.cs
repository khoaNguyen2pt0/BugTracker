using BugTracker.Helpers;
using BugTracker.Models;
using BugTracker.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectHelper projectHelper = new ProjectHelper();
        private TicketHelper ticketHelper = new TicketHelper();
        private RoleHelper roleHelper = new RoleHelper();

        [Authorize]
        public ActionResult Index()
        {
            return View(db.Users.Find(User.Identity.GetUserId()));
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            var allTickets = db.Tickets.ToList();
            // load up a dashboard viewmodel to feed to the view
            var dashboardVM = new DashboardViewModel()
            {
                TicketCount = allTickets.Count,
                HighPriorityTicketCount = allTickets.Where(t => t.TicketPriority.Name == "High").Count(),
                OpenTicketCount = allTickets.Where(t => t.TicketStatus.Name == "Open").Count(),
                TotalComments = db.TicketComments.Count(),

                AllTickets = allTickets


            };


            dashboardVM.ProjectVM.ProjectCount = db.Projects.Count();
            dashboardVM.ProjectVM.AllProjects = db.Projects.ToList();
            dashboardVM.ProjectVM.AllProjectManagers = roleHelper.UsersInRole("Project Manager").ToList();


            return View(dashboardVM);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UserProfile()
        {
            var model = new UserProfileViewModel();
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            model.AvatarPath = user.AvatarPath;
            model.Email = user.Email;
            model.FullName = user.FullName;
            model.Id = userId;
            model.ProjectIn = projectHelper.ListUserProjects(userId);
            model.TicketIn = ticketHelper.GetMyTickets();
            model.Role = roleHelper.ListUserRoles(userId).FirstOrDefault();

            return View(model);
        }

        public PartialViewResult _SideNav()
        {
            var userId = User.Identity.GetUserId();
            var model = db.Users.Find(userId);

            return PartialView(model);
        }
    }
}