using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketManagementApp.Services.Abstract;

namespace TicketManagementApp.Pages.Ticket
{
    public class CompletedTicketsViewModel
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Assignee { get; set; }
        public DateTime CompletedDate { get; set; }

    }
    public class CompletedTicketsModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly ITicketDetailService _ticketDetailService;

        public CompletedTicketsModel(ITicketService ticketService, ITicketDetailService ticketDetailService)
        {
            _ticketService = ticketService;
            _ticketDetailService = ticketDetailService;
        }
        public List<CompletedTicketsViewModel> ViewModels { get; set; } = new();
        public void OnGet()
        {
            fillList();
        }
        private void fillList()
        {
            var tickets = _ticketService.GetCompletedTickets();
            tickets.ForEach(ticket =>
            {
                ViewModels.Add(new CompletedTicketsViewModel
                {

                    CompletedDate = _ticketDetailService.GetCompletedTicketDetailByTicketId(ticket.Id).Date,
                    Assignee = ticket.Employee.Name,
                    Description = ticket.Description,
                    Subject = ticket.Subject
                });

            });
            ViewModels = ViewModels.OrderByDescending(x => x.CompletedDate).ToList();
        }
    }
}
