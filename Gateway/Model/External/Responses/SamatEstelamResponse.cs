using System.Collections.Generic;

namespace Gateway.Model.External.Responses
{
    public class SamatEstelamResponse
    {
        public string InquiryDate { get; set; }
        public string InquiryId { get; set; }
        public string CustomerType { get; set; }
        public string RealPersonNationalCode { get; set; }
        public string LegalPersonNaionalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string BadHesabiDate { get; set; }
        public string ResponseId { get; set; }
        public List<TashilatRow> TashilatRows { get; set; }
    }

    public class TashilatRow
    {
        public string ContractDate { get; set; }
        public string RequestNumber { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public int? RequstType { get; set; }
        public string DateSarResid { get; set; }
        public string CurrencyCode { get; set; }
        public long? AmountOriginal { get; set; }
        public long? AmountBenefit { get; set; }
        public long? AmountTotalDebt { get; set; }
        public long? AmountSarResid { get; set; }
        public long? AmountMoavagh { get; set; }
        public long? AmountMashkuk { get; set; }
        public long? AmountSukht { get; set; }
        public long? AmountEltezam { get; set; }
        public long? AmountDirkard { get; set; }
        public long? AmountTahod { get; set; }
        public string CreditType { get; set; }
        public string CreditResource { get; set; }
        public string Estemhal { get; set; }
        public string DateEstemhal { get; set; }
        public string HadafAzDaryaft { get; set; }
        public string MasrafPlaceCode { get; set; }
        public string DasteBandi { get; set; }
        public string RealPersonName { get; set; }
        public string LegalPersonName { get; set; }
        public string RealPersonNationalCode { get; set; }
        public string LegalPersonNaionalId { get; set; }
        public string Type { get; set; }
    }
}
