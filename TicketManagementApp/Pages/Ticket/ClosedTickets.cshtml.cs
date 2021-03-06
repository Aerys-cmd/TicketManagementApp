using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketManagementApp.Services.Abstract;

namespace TicketManagementApp.Pages.Ticket
{
    public class ClosedTicketViewModel
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Assignee { get; set; }
        public DateTime ClosedDate { get; set; }

    }
    public class ClosedTicketsModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly ITicketDetailService _ticketDetailService;

        public ClosedTicketsModel(ITicketService ticketService, ITicketDetailService ticketDetailService)
        {
            _ticketService = ticketService;
            _ticketDetailService = ticketDetailService;
        }

        public List<ClosedTicketViewModel> ViewModels { get; set; } = new();
        [BindProperty] public List<string> TicketIds { get; set; } = new();


        public void OnGet()
        {
            fillList();
        }

        public void OnPostReviewTicket(int index)
        {
            if (index >= 0)
            {
                _ticketService.SendTicketToReview(TicketIds[index]);
            }
            TicketIds.Clear();
            fillList();
        }
        public void OnPostCompleteTicket(int index)
        {
            if (index >= 0)
            {
                _ticketService.CompleteTicket(TicketIds[index]);
            }
            TicketIds.Clear();
            fillList();
        }

        private void fillList()
        {
            var tickets = _ticketService.GetClosedTickets();
            tickets.ForEach(ticket =>
            {
                ViewModels.Add(new ClosedTicketViewModel
                {
                    Id = ticket.Id,
                    ClosedDate = _ticketDetailService.GetClosedTicketDetailByTicketId(ticket.Id).Date,
                    Assignee = ticket.Employee.Name,
                    Description = ticket.Description,
                    Subject = ticket.Subject
                });
                if (TicketIds.Count < ViewModels.Count)
                {
                    TicketIds.Add("");
                }
            });
            ViewModels = ViewModels.OrderByDescending(x => x.ClosedDate).ToList();
        }
    }
}
