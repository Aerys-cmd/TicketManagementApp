using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagementApp.Models;

namespace TicketManagementApp.Services.Abstract
{
    public interface IEmployeeService
    {
        Employee GetEmployeeById(string Id);
        List<Employee> GetAllEmployees();


    }
}
