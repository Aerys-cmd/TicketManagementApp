﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagementApp.Models;

namespace TicketManagementApp.Services.Abstract
{
    public interface ITicketService
    {
        public void CreateTicket(Ticket ticket);


        /// <summary>
        /// Ticket'ın Difficulty ve Priority değerlerini set eder.
        /// Bu veriler set edildikten sonra ticketdetails tablosuna bugünün tarihini ve atanan statusu günceller.
        /// </summary>
        /// <param name="ticket"></param>
        public void SetTechnicalDetails(string ticketId, short priority, short difficulty);



        public void AssignTicketToEmployee(string employeeId, string ticketId)
        {

        }

        public void CloseTicket(string Id);
        public void CompleteTicket(string Id);

        public void SendTicketToReview(string Id);



    }
}
