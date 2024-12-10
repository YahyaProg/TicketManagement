using System;

namespace Core.Entities
{
    public class Message
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long TicketId { get; set; }
        public string Text { get; set; }
        public DateTime CreateAt { get; set; }
        public Ticket Ticket { get; set; }
        public User User { get; set; }
    }
}
