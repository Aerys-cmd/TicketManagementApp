using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagementApp.Models;
using TicketManagementApp.Repositories.Abstract;
using TicketManagementApp.Services.Abstract;

namespace TicketManagementApp.Services.Concrete
{
    public class TicketDetailManager : ITicketDetailService
    {
        private readonly ITicketDetailRepository _ticketDetailRepository;
        private readonly IEmailSender _emailSender;
        private readonly ICustomerService _customerService;
        private readonly IEmployeeService _employeeService;

        public TicketDetailManager(ITicketDetailRepository ticketDetailRepository, IEmailSender emailSender, ICustomerService customerService, IEmployeeService employeeService)
        {
            _ticketDetailRepository = ticketDetailRepository;
            _emailSender = emailSender;
            _customerService = customerService;
            _employeeService = employeeService;
        }


        private void AddTicketDetail(Ticket ticket, TicketStatus ticketStatus)
        {
            var ticketDetail = new TicketDetail();
            ticketDetail.Date = DateTime.Now;
            ticketDetail.Status = ticketStatus.ToString();
            ticketDetail.Ticket = ticket;
            _ticketDetailRepository.Add(ticketDetail);
        }

        public void SetTicketStatusOpen(Ticket ticket)
        {
            AddTicketDetail(ticket, TicketStatus.Open);
            _emailSender.SendNotification(ticket.Customer.Email, "Ticket Başarılı bir şekilde ulaştı.", $"Ticket Numaranız  {ticket.Id}");
        }

        public void SetTicketStatusReadyForAssignment(Ticket ticket)
        {
            AddTicketDetail(ticket, TicketStatus.ReadyForAssignment);
        }

        public void SetTicketStatusAssigned(Ticket ticket)
        {
            AddTicketDetail(ticket, TicketStatus.Assigned);
            _emailSender.SendNotification(ticket.Employee.Email, "Size bir ticket atandı.", $"Size atanan ticketın Ticket Numarası: {ticket.Id}");

        }

        public void SetTicketStatusClosed(Ticket ticket)
        {
            AddTicketDetail(ticket, TicketStatus.Closed);
            var Manager = _employeeService.GetManagerCredentialsByEmployeeId(ticket.Employee.Id);

            _emailSender.SendNotification(Manager.Email, "Atadığınız bir ticket kapatıldı.", $"Ticket'ı atadığınız {ticket.Employee.Name} isimli çalışan ticket'ı kapandı olarak işaretledi.");

        }

        public void SetTicketStatusReview(Ticket ticket)
        {
            AddTicketDetail(ticket, TicketStatus.Review);
            _emailSender.SendNotification(ticket.Employee.Email, "Review isteği", $"Kapattığınız {ticket.Id} nolu ticket" +
                $"müdürünüz tarafından review yapıldı lütfen kontrol sağlayın.");

        }

        public void SetTicketStatusCompleted(Ticket ticket)
        {
            AddTicketDetail(ticket, TicketStatus.Completed);
            _emailSender.SendNotification(ticket.Customer.Email, "Açtığınız ticket sonuçlandı.", $"{ticket.Id} nolu ticket çözüme ulaşmıştır. Sorununuz devam ediyorsa yeni bir ticket açabilirsiniz.");

        }

        public TicketDetail GetOpenTicketDetailByTicketId(string Id)
        {
            return GetTicketDetailByTicketId(Id, TicketStatus.Open.ToString());
        }

        public TicketDetail GetAssignedTicketDetailByTicketId(string Id)
        {
            return GetTicketDetailByTicketId(Id, TicketStatus.Assigned.ToString());
        }

        public TicketDetail GetClosedTicketDetailByTicketId(string Id)
        {
            return GetTicketDetailByTicketId(Id, TicketStatus.Closed.ToString());

        }

        public TicketDetail GetCompletedTicketDetailByTicketId(string Id)
        {
            return GetTicketDetailByTicketId(Id, TicketStatus.Completed.ToString());

        }

        public TicketDetail GetReviewTicketDetailByTicketId(string Id)
        {
            return GetTicketDetailByTicketId(Id, TicketStatus.Review.ToString());

        }

        private TicketDetail GetTicketDetailByTicketId(string Id, string status)
        {
            var ticketDetails = _ticketDetailRepository.List();
            return ticketDetails.Where(x => x.Status == status && x.Ticket.Id == Id).OrderByDescending(x => x.Date)
                .FirstOrDefault();
        }


    }
}
