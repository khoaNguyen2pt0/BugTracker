using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Helpers;
using BugTracker.Models;
using BugTracker.ViewModels;
using Microsoft.AspNet.Identity;


namespace BugTracker.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TicketHelper ticketHelper = new TicketHelper();
        private ProjectHelper projectHelper = new ProjectHelper();
        private HistoryHelper historyHelper = new HistoryHelper();

        // GET: Tickets     
        public ActionResult Index()
        {
            var myTicketVM = new MyTicketViewModel();
            myTicketVM.AllTickets = db.Tickets.ToList();
            myTicketVM.MyTickets = ticketHelper.GetMyTickets();
            return View(myTicketVM);
        }

        public ActionResult GetProjectTickets()
        {
            var myTicketVM = new MyTicketViewModel();
            myTicketVM.AllTickets = db.Tickets.ToList();
            myTicketVM.MyTickets = ticketHelper.GetMyTickets();

            //var userId = User.Identity.GetUserId();
            //var user = db.Users.Find(userId);
            //var ticketList = user.Projects.SelectMany(p => p.Tickets).ToList();
            //return View("Index", ticketList);

            return View("Index", myTicketVM);
        }

        public ActionResult GetUserTickets()
        {
            //var userId = User.Identity.GetUserId();
            //var user = db.Users.Find(userId);
            //var ticketList = user.Projects.SelectMany(p => p.Tickets).ToList();
            var myTicketVM = new MyTicketViewModel();


            if (User.IsInRole("Developer"))
            {
                //ticketList = db.Tickets.Where(t => t.DeveloperId == userId).ToList();
                //return View("Index", ticketList);

                myTicketVM.MyTickets = ticketHelper.GetMyTickets();
                return View("Index", myTicketVM);
            }
            if (User.IsInRole("Submitter"))
            {
                //ticketList = db.Tickets.Where(t => t.SubmitterId == userId).ToList();
                //return View("Index", ticketList);

                myTicketVM.MyTickets = ticketHelper.GetMyTickets();
                return View("Index", myTicketVM);
            }
            else
            {
                //return RedirectToAction("GetProjectTickets");
                return RedirectToAction("Index");
            }
        }

        // GET: Tickets/Details/5
        public ActionResult Dashboard(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Details/5


        // GET: Tickets/Create
        [Authorize(Roles = "Submitter")]
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.ProjectId = new SelectList(projectHelper.ListUserProjects(userId), "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Submitter")]
        public ActionResult Create([Bind(Include = "ProjectId,TicketPriorityId,TicketTypeId,DeveloperId,Title,Issue,IssueDescription")] Ticket ticket, bool onPage)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                ticket.TicketStatusId = db.TicketStatuses.Where(ts => ts.Name == "Open").FirstOrDefault().Id;
                ticket.Created = DateTime.Now;
                ticket.SubmitterId = userId;
                db.Tickets.Add(ticket);
                db.SaveChanges();
                if (onPage)
                {
                    return RedirectToAction("Details", "Projects", new { id = ticket.ProjectId });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.ProjectId = new SelectList(projectHelper.ListUserProjects(userId), "Id", "Name");
            ViewBag.DeveloperId = new SelectList(db.Users, "Id", "FullName", ticket.DeveloperId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeveloperId = new SelectList(db.Users, "Id", "FullName", ticket.DeveloperId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        //POST: Tickets/Edit/5     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,TicketPriorityId,TicketStatusId,TicketTypeId,DeveloperId,Issue,IssueDescription")] Ticket ticket)
        {

            if (ModelState.IsValid)
            {
                var oldTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();

                var newTicket = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
                historyHelper.ManageHistories(oldTicket, newTicket);            
                ticketHelper.ManageTicketNotifications(oldTicket, newTicket); 

                return RedirectToAction("Index");
            }
            ViewBag.DeveloperId = new SelectList(db.Users, "Id", "FullName", ticket.DeveloperId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing); // this is here for garbage collection
        }
    }
}
