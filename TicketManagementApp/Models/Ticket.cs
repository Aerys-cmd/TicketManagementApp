using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagementApp.Models
{
    public enum TicketStatus
    {
        Open,
        ReadyForAssignment,
        Assigned,
        Closed,
        Review,
        Completed

    }
    public class Ticket
    {
        public Ticket()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public short Difficulty { get; set; }
        public short Priority { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }

    }
}
