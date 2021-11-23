using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataAnnotationsExtensions;

namespace TicketManagementApp.Models
{
    public enum TicketStatus
    {
        Open,
        ReadyForAssignment,
        Assigned,
        Closed,
        Review,
        Completed

    }
    public class Ticket
    {
        public Ticket()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        [Required (ErrorMessage = "Ticket konusunu boş geçmeyiniz.")]
        [StringLength(50, ErrorMessage = "Ticket Konusu max 50 karakter girilebilir")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Ticket açıklamasını boş geçmeyiniz.")]
        [StringLength(500, ErrorMessage = "Ticket Açıklaması max 500 karakter girilebilir")]

        public string Description { get; set; }
        public string Status { get; set; }
        [Min(0, ErrorMessage = "En az 0 değeri girebilirsiniz.")]
        [Max(5,ErrorMessage = "En fazla 5 değeri girebilirsiniz.")]
        public short Difficulty { get; set; }
        [Min(0, ErrorMessage = "En az 0 değeri girebilirsiniz.")]
        [Max(5, ErrorMessage = "En fazla 5 değeri girebilirsiniz.")]
        public short Priority { get; set; }
        [Required(ErrorMessage = "Müşteri bilgileri girilmelidir.")]
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }

    }
}
