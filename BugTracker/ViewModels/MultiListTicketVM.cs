using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.ViewModels
{
    public class MultiListTicketVM
    {
        public List<Ticket> AllTickets { get; set; }

        public List<Ticket> ProjectTickets { get; set; }

        public List<Ticket> MyTickets { get; set; }


        public MultiListTicketVM()
        {
            AllTickets = new List<Ticket>();
            ProjectTickets = new List<Ticket>();
            MyTickets = new List<Ticket>();

        }
    }
}