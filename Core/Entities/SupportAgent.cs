using System.Collections.Generic;

namespace Core.Entities
{
    public class SupportAgent
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long DepartmentId { get; set; }
        public ICollection<Ticket> AssignedTickets { get; set; }
        public Department Department { get; set; }
    }

}
