using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagementApp.Models;
using TicketManagementApp.Repositories.Abstract;
using TicketManagementApp.Services.Abstract;

namespace TicketManagementApp.Services.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public Customer GetCustomerById(string Id)
        {
            Customer customer = _customerRepository.Find(Id);
            if (customer == null)
            {
                throw new Exception("Böyle bir müşteri bulunamamıştır.");
            }
            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.List();
        }
    }
}
