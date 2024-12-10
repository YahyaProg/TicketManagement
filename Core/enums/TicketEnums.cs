using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.enums
{
    public enum ETicket_Status
    {
        New = 0,
        Assigned = 1,
        InProgress = 2,
        Awaiting = 3,
        Resolved = 4,
        Closed = 5
        //        ۱. جدید (New)
        //۲. تخصیص داده شده (Assigned)
        //۳. در حال پیگیری (In Progress)
        //۴. در انتظار پاسخ (Awaiting)
        //۵. حل شده (Resolved)
        //۶. بسته شده (Closed)
    }
    public enum ETicket_Priority
    {
        Low = 0,
        Medium= 1,
        High= 2,
        Critical = 3
        //        کم (Low)
        //متوسط (Medium)
        //بالا (High)
        //بحرانی (Critical)
    }
}
