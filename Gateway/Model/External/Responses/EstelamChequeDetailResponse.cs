using System.Collections.Generic;

namespace Gateway.Model.External.Responses
{
    public class EstelamChequeDetailResponse
    {
        public bool? IsValid { get; set; }
        public string RequestDateTime { get; set; }
        public Cheque EstelamChequeDetailCheque { get; set; }
        public List<Customers> ChequeDetailCustomer { get; set; }
    }

    public class Cheque
    {
        public string BouncedBranchName { get; set; }
        public string IdCheque { get; set; }
        public string OriginBranchName { get; set; }
        public string LetterDate { get; set; }
        public string LetterNumber { get; set; }
        public double? Amount { get; set; }
        public string BankCode { get; set; }
        public double? BouncedAmount { get; set; }
        public string BouncedDate { get; set; }
        public string CurrencyCode { get; set; }
        public string Iban { get; set; }
        public string Serial { get; set; }
    }

    public class Customers
    {
        public int? CustomerType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
    }
}
