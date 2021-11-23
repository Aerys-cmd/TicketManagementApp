using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TicketManagementApp.Services.Abstract;

namespace TicketManagementApp.Services.Concrete
{

    public class NetSmptMailManager : IEmailSender
    {
        public void SendNotification(string to, string subject, string message)
        {
            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            msg.From = new MailAddress("nbuy.oglen@gmail.com");
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.IsBodyHtml = true; //to make message body as html  
            msg.Body = message;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("nbuy.oglen@gmail.com", "Nbuy12345");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(msg);
        }
    }

}
