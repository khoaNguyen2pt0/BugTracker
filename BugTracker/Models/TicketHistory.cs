using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class TicketHistory
    {
        public int Id { get; set; }

        #region Parents/Children
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        #endregion

        #region Actual Properties
        // the properof the ticket that were changed
        public string Property { get; set; }
        // the original value
        public string OldValue { get; set; }
        // new value
        public string NewValue { get; set; }
        public DateTime ChangedOn { get; set; }
        #endregion
    }
}