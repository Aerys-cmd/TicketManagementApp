using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagementApp.Models
{
    public class TicketDetail
    {
        public TicketDetail()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public Ticket Ticket { get; set; }


    }
}
