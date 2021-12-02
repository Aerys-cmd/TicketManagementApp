using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagementApp.Models;
using TicketManagementApp.Repositories.Abstract;

namespace TicketManagementApp.Repositories.Concrete
{
    public class EFEmployeeRepository : IEmployeeRepository
    {
        private readonly TicketManagementDbContext _db;
        public EFEmployeeRepository(TicketManagementDbContext db)
        {
            _db = db;
        }
        public Employee Find(string Id)
        {
            var employee = _db.Employees.Include(x => x.Manager).FirstOrDefault(x => x.Id == Id);
            return employee;
        }

        public List<Employee> List()
        {
            return _db.Employees.Include(x => x.Manager).ToList();
        }

        public void Add(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
