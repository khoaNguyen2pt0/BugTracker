﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketNotification
    {
        public int Id { get; set; }

        #region Parents/Children
        public int TicketId { get; set; }
        public string UserId { get; set; }
        #endregion

        #region Actual Properties
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public bool IsRead { get; set; }
        #endregion

        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }


    }
}