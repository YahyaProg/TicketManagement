namespace Gateway.Model.External.Requests
{
    public class CbDepositedChequesRequest
    {
        public int? Accountno { get; set; }
        public string FromInstrumentDate { get; set; }
        public string ToInstrumentDate { get; set; } = string.Empty;
        public string Owclngstatus { get; set; }
    }
}
