using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketManagementApp.Services.Abstract;

namespace TicketManagementApp.Pages.Ticket
{
    public class ReviewTicketViewModel
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Assignee { get; set; }
        public DateTime ReviewDate { get; set; }


    }
    public class ReviewTicketsModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly ITicketDetailService _ticketDetailService;
        public List<ReviewTicketViewModel> ViewModels { get; set; } = new();
        [BindProperty] public List<string> TicketIds { get; set; } = new();

        public ReviewTicketsModel(ITicketService ticketService, ITicketDetailService ticketDetailService)
        {
            _ticketService = ticketService;
            _ticketDetailService = ticketDetailService;
        }
        public void OnGet()
        {
            fillList();
        }

        public void OnPostCloseTicket(int index)
        {
            if (index >= 0)
            {
                _ticketService.CloseTicket(TicketIds[index]);
            }

        }
        private void fillList()
        {
            var tickets = _ticketService.GetReviewTickets();
            tickets.ForEach(ticket =>
            {
                ViewModels.Add(new ReviewTicketViewModel
                {
                    Id = ticket.Id,
                    ReviewDate = _ticketDetailService.GetReviewTicketDetailByTicketId(ticket.Id).Date,
                    Assignee = ticket.Employee.Name,
                    Description = ticket.Description,
                    Subject = ticket.Subject
                });
                if (TicketIds.Count < ViewModels.Count)
                {
                    TicketIds.Add("");
                }
            });
            ViewModels = ViewModels.OrderByDescending(x => x.ReviewDate).ToList();
        }
    }
}
