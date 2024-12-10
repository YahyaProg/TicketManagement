using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class MessageVM 
    {
        public long Id { get; set; }
        public string SenderName { get; set; }
        public DateTime CreateAt { get; set; }
        public string Text { get; set; }
    }
}
