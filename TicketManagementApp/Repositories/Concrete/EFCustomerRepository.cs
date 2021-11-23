using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagementApp.Models;
using TicketManagementApp.Repositories.Abstract;

namespace TicketManagementApp.Repositories.Concrete
{
    public class EFCustomerRepository : ICustomerRepository
    {
        private TicketManagementDbContext _db;
        public EFCustomerRepository(TicketManagementDbContext db)
        {
            _db = db;
        }
        public Customer Find(string Id)
        {
            var customer = _db.Customers.Find(Id);
            return customer;

        }

        public List<Customer> List()
        {
            return _db.Customers.ToList();
        }

        public void Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
