using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagementApp.Models;
using TicketManagementApp.Repositories.Abstract;

namespace TicketManagementApp.Repositories.Concrete
{
    public class EFTicketRepository : ITicketRepository
    {
        private TicketManagementDbContext _db;
        public EFTicketRepository(TicketManagementDbContext db)
        {
            _db = db;
        }
        public Ticket Find(string Id)
        {
            var ticket = _db.Tickets.Include(x => x.Customer).Include(x => x.Employee).FirstOrDefault(x => x.Id == Id);
            return ticket;
        }

        public List<Ticket> List()
        {
            return _db.Tickets.Include(x => x.Customer).Include(x => x.Employee).ToList();
        }

        public void Add(Ticket entity)
        {
            _db.Tickets.Add(entity);
            _db.SaveChanges();
        }

        public void Update(Ticket entity)
        {
            _db.Tickets.Update(entity);
            _db.SaveChanges();
        }

        public void Delete(string Id)
        {
            var removedEntity = _db.Tickets.Find(Id);
            _db.Tickets.Remove(removedEntity);
            _db.SaveChanges();
        }
    }
}
