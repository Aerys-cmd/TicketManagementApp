using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketManagementApp.Services.Abstract;

namespace TicketManagementApp.Pages.Ticket
{
    public class AssignedTicketsViewModel
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Assignee { get; set; }
        public DateTime AssignedDate { get; set; }

    }
    public class AssignedTicketsModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly ITicketDetailService _ticketDetailService;

        public AssignedTicketsModel(ITicketService ticketService, ITicketDetailService ticketDetailService)
        {
            _ticketService = ticketService;
            _ticketDetailService = ticketDetailService;
        }
        public List<AssignedTicketsViewModel> ViewModels { get; set; } = new();
        [BindProperty] public List<string> TicketIds { get; set; } = new();

        public void OnGet()
        {
            fillList();
        }

        private void fillList()
        {
            var tickets = _ticketService.GetAssignedTickets();
            tickets.ForEach(ticket =>
            {
                ViewModels.Add(new AssignedTicketsViewModel
                {
                    Id = ticket.Id,
                    AssignedDate = _ticketDetailService.GetAssignedTicketDetailByTicketId(ticket.Id).Date,
                    Assignee = ticket.Employee.Name,
                    Description = ticket.Description,
                    Subject = ticket.Subject
                });
                if (TicketIds.Count < ViewModels.Count)
                {
                    TicketIds.Add("");
                }
            });
            ViewModels = ViewModels.OrderByDescending(x => x.AssignedDate).ToList();

        }

        public void OnPostCloseTicket(int index)
        {
            if (index >= 0)
            {
                _ticketService.CloseTicket(TicketIds[index]);
            }
        }
    }
}
