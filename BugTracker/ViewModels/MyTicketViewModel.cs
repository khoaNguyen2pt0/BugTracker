using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BugTracker.Helpers;

namespace BugTracker.ViewModels
{
    public class MyTicketViewModel
    {
        public List<Ticket> AllTickets { get; set; }
        public List<Ticket> MyTickets { get; set; }

        public MyTicketViewModel()
        {
            AllTickets = new List<Ticket>();
            MyTickets = new List<Ticket>();

        }
    }
}
