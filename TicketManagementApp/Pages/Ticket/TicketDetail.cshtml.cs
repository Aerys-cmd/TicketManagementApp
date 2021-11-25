using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketManagementApp.Services.Abstract;

namespace TicketManagementApp.Pages.Ticket
{
    public class TicketViewModel
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime OpenDate { get; set; }
        public string CustomerName { get; set; }
    }
    public class TicketInputModel
    {
        public string Id { get; set; }
        [Required] public string Difficulty { get; set; }
        [Required] public short Priority { get; set; }


    }
    public class TicketDetailModel : PageModel
    {
        [BindProperty] public TicketInputModel TicketInputModel { get; set; }
        [BindProperty] public TicketViewModel TicketViewModel { get; set; } = new();




        public string[] Difficulties = { "Çok Kolay", "Kolay", "Orta", "Zor", "Çok Zor" };

        public int[] Priorities = { 5, 4, 3, 2, 1 };
        private readonly ITicketService _ticketService;
        private readonly ITicketDetailService _ticketDetailService;

        public TicketDetailModel(ITicketService ticketService, ITicketDetailService ticketDetailService)
        {
            _ticketService = ticketService;
            _ticketDetailService = ticketDetailService;
        }

        public void OnGet(string? id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var ticket = _ticketService.GetTicketById(id);
                if (ticket != null)
                {
                    TicketViewModel.Subject = ticket.Subject;
                    TicketViewModel.Description = ticket.Description;
                    TicketViewModel.Id = ticket.Id;
                    TicketViewModel.OpenDate = _ticketDetailService.GetOpenTicketDetailByTicketId(id).Date;
                    TicketViewModel.CustomerName = ticket.Customer.Name;
                    ViewData["Id"] = TicketViewModel.Id;
                }
            }

        }

        public void OnPostUpdateTicket()
        {
            if (ModelState.IsValid)
            {
                var difficulties = Difficulties.ToList();
                int difficultyRank = difficulties.IndexOf(TicketInputModel.Difficulty);
                difficultyRank++;
                _ticketService.SetTechnicalDetails(TicketInputModel.Id, TicketInputModel.Priority, (short)difficultyRank);
            }
        }
    }
}
