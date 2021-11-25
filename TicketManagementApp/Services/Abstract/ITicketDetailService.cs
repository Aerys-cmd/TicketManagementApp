using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagementApp.Models;

namespace TicketManagementApp.Services.Abstract
{
    public interface ITicketDetailService
    {
        public void SetTicketStatusOpen(Ticket ticket);


        public void SetTicketStatusReadyForAssignment(Ticket ticket);


        public void SetTicketStatusAssigned(Ticket ticket);


        public void SetTicketStatusClosed(Ticket ticket);


        public void SetTicketStatusReview(Ticket ticket);


        public void SetTicketStatusCompleted(Ticket ticket);

        public TicketDetail GetOpenTicketDetailByTicketId(string Id);
        public TicketDetail GetAssignedTicketDetailByTicketId(string Id);
        public TicketDetail GetClosedTicketDetailByTicketId(string Id);
        public TicketDetail GetCompletedTicketDetailByTicketId(string Id);
        public TicketDetail GetReviewTicketDetailByTicketId(string Id);




    }
}
