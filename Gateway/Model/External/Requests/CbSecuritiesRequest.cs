namespace Gateway.Model.External.Requests
{
    public class CbSecuritiesRequest
    {
        public int? CustomerNo { get; set; }
        public int? SecurityCode { get; set; }
        public bool Dummy { get; set; }
        public bool status { get; set; }
    }
}
