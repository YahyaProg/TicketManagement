namespace Gateway.Model.External.Responses
{
    public class CbLatePaymentResponse
    {
        public long? CustomerId { get; set; }
        public string NameLong { get; set; }
        public double? Score { get; set; }
        public double? Cnt { get; set; }
    }
}
