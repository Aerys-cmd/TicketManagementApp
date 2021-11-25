using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TicketManagementApp.Models;
using TicketManagementApp.Services.Abstract;

namespace TicketManagementApp.Pages.Ticket
{
    public class CreateTicketModel : PageModel
    {


        public List<SelectListItem> Customers { get; set; } = new List<SelectListItem>();
        private readonly ICustomerService _customerService;
        private readonly ITicketService _ticketService;

        [BindProperty]
        public string SelectedCustomerId { get; set; }
        [BindProperty] public Models.Ticket Ticket { get; set; }

        public CreateTicketModel(ICustomerService customerService, ITicketService ticketService)
        {
            this._customerService = customerService;
            _ticketService = ticketService;

        }

        public void OnGet()
        {

            FillList();
        }

        private void FillList()
        {
            var Customers = _customerService.GetAllCustomers();

            this.Customers = Customers.Select(a =>
                new SelectListItem
                {
                    Value = a.Id,
                    Text = a.Name
                }).ToList();
        }

        public void OnPostSave()
        {
            if (ModelState.IsValid)
            {
                _ticketService.CreateTicket(ticket: Ticket, SelectedCustomerId);
            }
            FillList();
        }
    }
}
