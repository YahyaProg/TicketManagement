using Core.enums;
using System.Collections.Generic;

namespace Core.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public EUser_Role Role { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<ActionLog> ActionLogs { get; set; }
    }

}
