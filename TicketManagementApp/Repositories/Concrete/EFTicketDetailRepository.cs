using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagementApp.Models;
using TicketManagementApp.Repositories.Abstract;

namespace TicketManagementApp.Repositories.Concrete
{
    public class EFTicketDetailRepository : ITicketDetailRepository
    {
        private readonly TicketManagementDbContext _db;
        public EFTicketDetailRepository(TicketManagementDbContext db)
        {
            _db = db;
        }
        public TicketDetail Find(string Id)
        {
            var ticketDetail = _db.TicketDetails.Include(x => x.Ticket).FirstOrDefault(x => x.Id == Id);
            return ticketDetail;
        }

        public List<TicketDetail> List()
        {
            return _db.TicketDetails.Include(x => x.Ticket).ToList();
        }

        public void Add(TicketDetail entity)
        {
            _db.TicketDetails.Add(entity);
            _db.SaveChanges();
        }

        public void Update(TicketDetail entity)
        {
            _db.TicketDetails.Update(entity);
            _db.SaveChanges();
        }

        public void Delete(string Id)
        {
            var removedEntity = _db.TicketDetails.Find(Id);
            _db.TicketDetails.Remove(removedEntity);
            _db.SaveChanges();
        }
    }
}
