namespace Gateway.Model.External.Requests
{
    public class GetSamatLoansInfoRequest
    {
        public string NationalCode { get; set; }
        public string ContractType { get; set; }
        public int DurationType { get; set; }
    }
}
