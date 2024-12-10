using System;

namespace Gateway.Model.External.Requests
{
    public class CbBgRequest
    {
        public long? Customerid { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long? Accountno { get; set; }
    }
}
