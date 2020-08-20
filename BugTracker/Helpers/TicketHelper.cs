﻿using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BugTracker.Helpers
{

    public class TicketHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelper roleHelper = new RoleHelper();

        public List<Ticket> GetMyTickets()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var tickets = new List<Ticket>();
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Admin":
                    tickets.AddRange(db.Tickets);
                    break;
                case "Project Manager":
                    tickets.AddRange(db.Users.Find(userId).Projects.SelectMany(p => p.Tickets));
                    break;
                case "Developer":
                    tickets.AddRange(db.Tickets.Where(t => t.DeveloperId == userId));
                    break;
                case "Submitter":
                    tickets.AddRange(db.Tickets.Where(t => t.SubmitterId == userId));
                    break;
                default:
                    break;
            }

            return tickets;
        }


        public bool CanEditTicket(int ticketId)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Admin":
                    return true;
                case "Project Manager":
                    var user = db.Users.Find(userId);
                    return user.Projects.SelectMany(p => p.Tickets).Any(t => t.Id == ticketId);
                case "Developer":
                case "Submitter":
                    var ticket = db.Tickets.Find(ticketId);
                    if (ticket.DeveloperId == userId || ticket.SubmitterId == userId)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }

        }

        public bool CanMakeComment(int ticketId)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Admin":
                    return true;
                case "Project Manager":
                    var user = db.Users.Find(userId);
                    return user.Projects.SelectMany(p => p.Tickets).Any(t => t.Id == ticketId);
                case "Developer":
                case "Submitter":
                    var ticket = db.Tickets.Find(ticketId);
                    if (ticket.DeveloperId == userId || ticket.SubmitterId == userId)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }

        }

        //public void ManageTicketNotifications(Ticket oldTicket, Ticket newTicket)
        //{
        //    if (oldTicket.DeveloperId != newTicket.DeveloperId && newTicket.DeveloperId != null)
        //    {
        //        var newNotification = new TicketNotification()
        //        {
        //            TicketId = newTicket.Id,
        //            UserId = newTicket.DeveloperId,
        //            Created = DateTime.Now,
        //            Subject = $"You have been assigned Ticket Id: {newTicket.Id}",
        //            Body = $"Heads up {newTicket.Developer.FullName}, you have been assigned to Ticket Id {newTicket.Id} titled '{newTicket.IssueDescription}' on Project '{newTicket.Project.Name}'",
        //        };

        //        db.TicketNotifications.Add(newNotification);
        //        db.SaveChanges();




        //    };


        //}
    }
}