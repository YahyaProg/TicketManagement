namespace Gateway.Model.External.Responses
{
    public class CbBgloanlcResponse
    {
        public long? AccountNo { get; set; }
        public long? Id { get; set; }
        public long? CustomerId { get; set; }
        public long? Balance { get; set; }
        public string BalanceDate { get; set; }
        public long? TotalBankPayment { get; set; }
        public long? ToTpenal { get; set; }
        public bool? NpaStatusCode { get; set; }
        public string NpastatusDesc { get; set; }
        public string OpenDate { get; set; }
        public string ReviewDate { get; set; }
        public long? LoanInTerest { get; set; }
        public long? CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public long? OutStandingBal { get; set; }
        public long? TajdidAmt { get; set; }
        public long? InStallCnt { get; set; }
        public long? InStallAmt { get; set; }
        public bool? LnBgStatus { get; set; }
        public string BgBenefName { get; set; }
        public string SchemeDesc { get; set; }
        public int? Margin { get; set; }
    }
}
