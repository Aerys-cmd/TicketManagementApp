using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketManagementApp.Services.Abstract;

namespace TicketManagementApp.Pages.Ticket
{
    public class OpenTicketModel
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime OpenDate { get; set; }
    }
    public class OpenTicketsModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly ITicketDetailService _ticketDetailService;

        [BindProperty]
        public List<OpenTicketModel> OpenTicketModels { get; set; } = new();

        public OpenTicketsModel(ITicketService ticketService, ITicketDetailService ticketDetailService)
        {
            _ticketService = ticketService;
            _ticketDetailService = ticketDetailService;
        }
        public void OnGet()
        {
            var tickets = _ticketService.GetOpenTickets();
            tickets.ForEach(x =>
            {
                OpenTicketModels.Add(new OpenTicketModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    OpenDate = _ticketDetailService.GetOpenTicketDetailByTicketId(x.Id).Date,
                    Subject = x.Subject
                });
            });
        }
    }
}
