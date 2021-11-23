using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagementApp.Models;

namespace TicketManagementApp.Services.Abstract
{
    public interface ICustomerService
    {
        Customer GetCustomerById(string Id);
        List<Customer> GetAllCustomers();
    }
}
