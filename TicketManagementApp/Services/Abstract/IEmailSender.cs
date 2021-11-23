using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagementApp.Services.Abstract
{
    public interface IEmailSender
    {
        void SendNotification(string to, string subject, string message);

    }
}
