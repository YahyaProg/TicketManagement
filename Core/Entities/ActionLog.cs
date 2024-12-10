using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ActionLog
    {
        public long Id { get; set; }
        public string Action { get; set; }
        public DateTime RecordDate { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
