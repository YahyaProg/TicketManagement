using System.Collections.Generic;

namespace Gateway.Model.External.Responses
{
    public class EstelamZamenResponse
    {
        public string CustomerType { get; set; }
        public string DateEstlm { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string LegalId { get; set; }
        public string NationalCd { get; set; }
        public string ShenaseEstlm { get; set; }
        public string ShenaseRes { get; set; }
        public List<EstelamZamenRow> EstelamZamenRows { get; set; }
    }

    public class EstelamZamenRow
    {
        public string AmBedehiKol { get; set; }
        public string AmBenefit { get; set; }
        public string AmDirkard { get; set; }
        public string AmEltezam { get; set; }
        public string AmMashkuk { get; set; }
        public string AmMoavagh { get; set; }
        public string AmOriginal { get; set; }
        public string AmSarResid { get; set; }
        public string AmSukht { get; set; }
        public string AmTahod { get; set; }
        public string BankCode { get; set; }
        public string Date { get; set; }
        public string DateSarResid { get; set; }
        public string PrcntZemanat { get; set; }
        public string RequestNum { get; set; }
        public string RequstType { get; set; }
        public string ShobeCode { get; set; }
        public string ShobeName { get; set; }
        public string ZmntIdNo { get; set; }
        public string ZmntLgId { get; set; }
        public string ZmntName { get; set; }
        public string ZmntLName { get; set; }
    }
}
