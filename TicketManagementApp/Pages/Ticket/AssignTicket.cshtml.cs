using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketManagementApp.Services.Abstract;

namespace TicketManagementApp.Pages.Ticket
{
    public class TicketAssignViewModel
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public short Difficulty { get; set; }
        public short Priority { get; set; }

    }

    public class TicketAssignInputModel
    {
        public string TicketId { get; set; }
        public string EmployeeId { get; set; }
    }


    public class AssignTicketModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly IEmployeeService _employeeService;
        [BindProperty] public List<TicketAssignInputModel> InputModels { get; set; }


        public List<SelectListItem> EmployeeList { get; set; } = new();

        public List<TicketAssignViewModel> TicketAssignViewModels { get; set; } = new();
        public AssignTicketModel(ITicketService ticketService, IEmployeeService employeeService)
        {
            _ticketService = ticketService;
            _employeeService = employeeService;
            InputModels = new();

        }
        public void OnGet()
        {
            fillSelect();
            fillTicketView();

        }

        private void fillSelect()
        {
            var employees = _employeeService.GetAllEmployees();
            EmployeeList = employees.Select(x =>
                new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name
                }
            ).ToList();


        }

        private void fillTicketView()
        {
            var tickets = _ticketService.GetReadyForAssignmentTickets();
            tickets.ForEach(ticket =>
              {
                  TicketAssignViewModels.Add(new TicketAssignViewModel
                  {
                      Id = ticket.Id,
                      Description = ticket.Description,
                      Subject = ticket.Subject,
                      Difficulty = ticket.Difficulty,
                      Priority = ticket.Priority
                  });
                  if (InputModels.Count < TicketAssignViewModels.Count)
                  {
                      InputModels.Add(new TicketAssignInputModel());
                  }


              });

        }


        public void OnPostAssignEmployee(int index)
        {
            if (index >= 0)
            {
                _ticketService.AssignTicketToEmployee(employeeId: InputModels[index].EmployeeId, ticketId: InputModels[index].TicketId);
            }

            fillTicketView();
            fillSelect();
        }
    }
}
