using System.Collections.Generic;

namespace Gateway.Model.External.Responses
{
    public class CbLoanResponse
    {
        public string Accountno { get; set; }
        public long? Id { get; set; }
        public long? Customerid { get; set; }
        public string Branch { get; set; }
        public string SchemeDesc { get; set; }
        public string Scheme { get; set; }
        public string Currencycode { get; set; }
        public string ReviewDate { get; set; }
        public double? Amount { get; set; }
        public string Status { get; set; }
        public string NpaClassCode { get; set; }
        public double? InterestRate { get; set; }
        public double? LocalLedgerBalance { get; set; }
        public double? Outsatnding { get; set; }

        public double? RemainingAmount { get { return LocalLedgerBalance; } }
        public double? NpaOutstanding
        {
            get
            {
                if (NpaClassCode is null || NpaClassCode != "APL")
                    return Outsatnding;

                return 0;
            }
        }
        public double? PaOutstanding
        {
            get
            {
                if (NpaClassCode is not null && NpaClassCode == "APL")
                    return Outsatnding;

                return 0;
            }
        }
        public List<CollateralInfo> CollateralInfoList { get; set; }
    }

    public class CollateralInfo
    {
        public long? CollateralCode { get; set; }
        public string CollateralType { get; set; }
        public double? Amount { get; set; }
    }
}
