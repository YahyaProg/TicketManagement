using System.Collections.Generic;

namespace Gateway.Model.External.Responses
{
    public class EstelamChequeResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public string PersonType { get; set; }
        public List<BouncedCheques> BouncedCheques { get; set; }
    }

    public class BouncedCheques
    {
        public string BouncedBranchName { get; set; }
        public long? CustomerType { get; set; }
        public string ChequeId { get; set; }
        public string OriginBranchName { get; set; }
        public double? Amount { get; set; }
        public double? BouncedAmount { get; set; }
        public long? BankCode { get; set; }
    }

    public class EstelamChequeResponseWithDetail
    {
        public long? Count { get; set; }
        public double? Sum { get; set; }
    }
}
