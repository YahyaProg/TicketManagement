namespace Gateway.Model.External.Requests
{
    public class CbBgloanlcRequest
    {
        public int? CustomerId { get; set; }
        public int? Balance { get; set; }
        public string BalanceDate { get; set; }
        public bool NpaStatusCode { get; set; }
        public string ReviewDate { get; set; }
        public int? AccountNo { get; set; }
    }
}
