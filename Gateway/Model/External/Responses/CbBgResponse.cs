using System;
using System.Collections.Generic;

namespace Gateway.Model.External.Responses
{
    public class CbBgResponse
    {
        public string Id { get; set; }
        public string AccountNo { get; set; }
        public string CustomerId { get; set; }
        public string Barnch { get; set; }
        public string BgType { get; set; }
        public string LoanNo { get; set; }
        public string BgStatus { get; set; }
        public string BgStatusDesc { get; set; }
        public double? BgNewamt { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime? BgExpiryDate { get; set; }
        public double? BgAmount { get; set; }
        public string NpaclassCode { get; set; }
        public string Description { get; set; }
        public bool? Settled { get; set; }
        public double? OutSatnding { get; set; }
        public double? NpaOutstanding
        {
            get
            {
                if (CurrencyCode.Contains("IRR", StringComparison.CurrentCultureIgnoreCase) &&
                    (bool)Settled &&
                    NpaclassCode != null &&
                    NpaclassCode != "APL")
                    return OutSatnding;
                return 0;
            }
        }
        public double? PaOutstanding
        {
            get
            {
                if (CurrencyCode.Contains("IRR", StringComparison.CurrentCultureIgnoreCase) &&
                    (bool)Settled &&
                    NpaclassCode != null &&
                    NpaclassCode == "APL")
                    return OutSatnding;
                return 0;
            }
        }
        public double? PrincipalOutstanding
        {
            get
            {
                if (CurrencyCode.Contains("IRR", StringComparison.CurrentCultureIgnoreCase) && (bool)Settled)
                    return OutSatnding;
                return 0;
            }
        }
        public List<CollateralInfo> CollateralInfoList { get; set; }
    }
}
