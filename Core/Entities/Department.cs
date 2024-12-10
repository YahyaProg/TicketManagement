using System.Collections.Generic;

namespace Core.Entities
{
    public class Department
    {
        public long Id { get; set; }
        public string Name { get; set; } // مثل فنی، مالی و غیره
        public bool? Deleted { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<SupportAgent> Agents { get; set; }
    }

}
