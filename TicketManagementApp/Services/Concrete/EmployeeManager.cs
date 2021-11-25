using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagementApp.Models;
using TicketManagementApp.Repositories.Abstract;
using TicketManagementApp.Services.Abstract;

namespace TicketManagementApp.Services.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Employee GetEmployeeById(string Id)
        {
            Employee emp = _employeeRepository.Find(Id);
            if (emp == null)
            {
                throw new Exception("Böyle bir Çalışan bulunamadı");
            }
            return emp;
        }

        public List<Employee> GetAllEmployees()
        {
            return _employeeRepository.List();
        }

        public Employee GetManagerCredentialsByEmployeeId(string id)
        {
            return _employeeRepository.Find(id).Manager;
        }
    }
}
