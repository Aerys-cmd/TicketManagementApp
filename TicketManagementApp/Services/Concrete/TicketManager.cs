using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagementApp.Models;
using TicketManagementApp.Repositories.Abstract;
using TicketManagementApp.Repositories.Concrete;
using TicketManagementApp.Services.Abstract;

namespace TicketManagementApp.Services.Concrete
{
    public class TicketManager : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketDetailService _ticketDetailService;
        private readonly IEmployeeService _employeeService;
        private readonly ICustomerService _customerService;
        public TicketManager(ITicketRepository ticketRepository, ITicketDetailService ticketDetailService, IEmployeeService employeeService, ICustomerService customerService)
        {
            this._ticketRepository = ticketRepository;
            this._ticketDetailService = ticketDetailService;
            this._employeeService = employeeService;
            this._customerService = customerService;
        }
        /// <summary>
        /// Veritabanında yeni bir ticket oluşturur. Status'u Open yapar. Bu statusu ve Bugünün tarihini ticketdetail tablosuna ekler
        /// </summary>
        /// <param name="ticket">
        ///Eklenmesi istenen ticket buraya gönderilir.
        /// </param>
        public void CreateTicket(Ticket ticket, string customerId)
        {
            var customer = _customerService.GetCustomerById(customerId);
            ticket.Customer = customer;
            ValidateTicketInfo(ticket);
            ticket.Status = TicketStatus.Open.ToString();
            _ticketRepository.Add(ticket);
            _ticketDetailService.SetTicketStatusOpen(ticket);
        }

        /// <summary>
        /// Ticket'ın Difficulty ve Priority değerlerini set eder.
        /// Bu veriler set edildikten sonra ticketdetails tablosuna bugünün tarihini ve atanan statusu günceller.
        /// </summary>
        /// <param name="ticket"></param>
        public void SetTechnicalDetails(string ticketId, short priority, short difficulty)
        {
            var ticket = _ticketRepository.Find(ticketId);
            ValidateTicketInfo(ticket);
            ValidateTechnicalDetails(priority, difficulty);
            ticket.Status = TicketStatus.ReadyForAssignment.ToString();
            ticket.Difficulty = difficulty;
            ticket.Priority = priority;
            _ticketRepository.Update(ticket);
            _ticketDetailService.SetTicketStatusReadyForAssignment(ticket);
        }

        public void AssignTicketToEmployee(string employeeId, string ticketId)
        {
            var ticket = _ticketRepository.Find(ticketId);
            ValidateTicketInfo(ticket);
            IsTicketAssigned(ticket);
            var employee = _employeeService.GetEmployeeById(employeeId);
            ticket.Employee = employee;
            ticket.Status = TicketStatus.Assigned.ToString();
            _ticketDetailService.SetTicketStatusAssigned(ticket);
        }

        public void CloseTicket(string Id)
        {
            var ticket = _ticketRepository.Find(Id);
            ValidateTicketInfo(ticket);
            ticket.Status = TicketStatus.Closed.ToString();
            _ticketDetailService.SetTicketStatusClosed(ticket);

        }

        public void CompleteTicket(string Id)
        {
            var ticket = _ticketRepository.Find(Id);
            ValidateTicketInfo(ticket);
            ticket.Status = TicketStatus.Completed.ToString();
            _ticketDetailService.SetTicketStatusCompleted(ticket);

        }

        public void SendTicketToReview(string Id)
        {
            var ticket = _ticketRepository.Find(Id);
            ValidateTicketInfo(ticket);
            ticket.Status = TicketStatus.Review.ToString();
            _ticketDetailService.SetTicketStatusReview(ticket);
        }

        private List<Ticket> getAllTickets()
        {
            return _ticketRepository.List();
        }
        public Ticket GetTicketById(string Id)
        {
            return _ticketRepository.Find(Id);
        }

        public List<Ticket> GetOpenTickets()
        {
            return getAllTickets().Where(x => x.Status == TicketStatus.Open.ToString()).ToList();
        }

        public List<Ticket> GetReadyForAssignmentTickets()
        {
            return getAllTickets().Where(x => x.Status == TicketStatus.ReadyForAssignment.ToString()).ToList();
        }

        public List<Ticket> GetAssignedTickets()
        {
            return getAllTickets().Where(x => x.Status == TicketStatus.Assigned.ToString()).ToList();
        }


        private void IsTicketAssigned(Ticket ticket)
        {
            if (ticket.Employee != null)
            {
                throw new Exception("Daha önce bu bilete personel ataması gerçekleştirildi.");
            }
        }
        private void ValidateTechnicalDetails(short priority, short difficulty)
        {
            if (priority < 1 || priority > 5)
            {
                throw new Exception("Önem derecesi 1 ile 5 arasında olmalıdır. Farklı bir değer girilemez");
            }

            if (difficulty < 1 || difficulty > 5)
            {
                throw new Exception("Zorluk derecesi 1 ile 5 arasında olmalıdır. Farklı bir değer girilemez");

            }
        }

        private void ValidateTicketInfo(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new Exception("Boş bir ticket gönderilemez.");
            }

            if (ticket.Description == "")
            {
                throw new Exception("Ticketin açıklaması boş olamaz");
            }

            if (ticket.Subject == "")
            {
                throw new Exception("Ticket konusu boş olamaz");

            }

            if (ticket.Customer == null)
            {
                throw new Exception("Ticketi gönderen müşteri alanı boş olamaz");
            }
        }


    }
}
