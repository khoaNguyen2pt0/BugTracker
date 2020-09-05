using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BugTracker.ViewModels
{
    public class DashboardViewModel
    {
        public int TicketCount { get; set; }
        
        public int HighPriorityTicketCount { get; set; }

        public int OpenTicketCount { get; set; }

        public int TotalComments { get; set; }


        public List<Ticket> AllTickets { get; set; }

        public ProjectsViewModel ProjectVM { get; set; }

        public List<ApplicationUser> AllProjectManagers { get; set; }


        public DashboardViewModel()
        {
            AllTickets = new List<Ticket>();
            AllProjectManagers = new List<ApplicationUser>();
            ProjectVM = new ProjectsViewModel();
        }

    }
}