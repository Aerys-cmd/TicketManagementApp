using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TicketManagementApp.Pages.TicketProcess
{
    public class CreateTicketModel : PageModel
    {
        public List<SelectListItem> SelectListItems = new List<SelectListItem>();
        public void OnGet()
        {

        }
    }
}
