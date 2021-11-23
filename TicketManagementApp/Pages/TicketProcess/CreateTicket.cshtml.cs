using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using TicketManagementApp.Models;
using TicketManagementApp.Services.Abstract;

namespace TicketManagementApp.Pages.TicketProcess
{
    public class CreateTicketModel : PageModel
    {


        public List<SelectListItem> SelectListItems { get; set; } = new List<SelectListItem>();
        private readonly ICustomerService _customerService;
        private readonly ITicketService _ticketService;

        [BindProperty]
        public string selectedCustomerId { get; set; }
        [BindProperty] public Ticket Ticket { get; set; }

        public CreateTicketModel(ICustomerService customerService, ITicketService ticketService)
        {
            this._customerService = customerService;
            _ticketService = ticketService;

        }

        public void OnGet()
        {
            var Customers = _customerService.GetAllCustomers();

            SelectListItems = Customers.Select(a =>
                new SelectListItem
                {
                    Value = a.Id,
                    Text = a.Name
                }).ToList();

        }

        public void OnPostSave()
        {
           
                _ticketService.CreateTicket(ticket: Ticket, selectedCustomerId);
           
            OnGet();
        }
    }
}
