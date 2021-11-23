using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagementApp.Models;
using TicketManagementApp.Repositories.Abstract;
using TicketManagementApp.Services.Abstract;

namespace TicketManagementApp.Services.Concrete
{
    public class TicketDetailManager : ITicketDetailService
    {
        private readonly ITicketDetailRepository _ticketDetailRepository;

        public TicketDetailManager(ITicketDetailRepository ticketDetailRepository)
        {
            _ticketDetailRepository = ticketDetailRepository;
        }

        private void AddTicketDetail(Ticket ticket, TicketStatus ticketStatus)
        {
            var ticketDetail = new TicketDetail();
            ticketDetail.Date = DateTime.Now;
            ticketDetail.Status = ticketStatus.ToString();
            ticketDetail.Ticket = ticket;
            _ticketDetailRepository.Add(ticketDetail);
        }

        public void SetTicketStatusOpen(Ticket ticket)
        {
            AddTicketDetail(ticket, TicketStatus.Open);
        }

        public void SetTicketStatusReadyForAssignment(Ticket ticket)
        {
            AddTicketDetail(ticket, TicketStatus.ReadyForAssignment);
        }

        public void SetTicketStatusAssigned(Ticket ticket)
        {
            AddTicketDetail(ticket, TicketStatus.Assigned);

        }

        public void SetTicketStatusClosed(Ticket ticket)
        {
            AddTicketDetail(ticket, TicketStatus.Closed);

        }

        public void SetTicketStatusReview(Ticket ticket)
        {
            AddTicketDetail(ticket, TicketStatus.Review);

        }

        public void SetTicketStatusCompleted(Ticket ticket)
        {
            AddTicketDetail(ticket, TicketStatus.Completed);

        }

    }
}
