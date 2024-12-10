using Core.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Ticket
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
        public ETicket_Priority Priority { get; set; } // Low, Medium, High, Critical
        public ETicket_Status Status { get; set; }  // New, Assigned, InProgress, Awaiting, Resolved, Closed
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public long? SupportAgentId { get; set; }
        public SupportAgent SupportAgent { get; set; }

        public long? DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Message> Messages { get; set; }

    }

}
