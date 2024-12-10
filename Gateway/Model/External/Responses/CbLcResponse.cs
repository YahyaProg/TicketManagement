using System.Collections.Generic;

namespace Gateway.Model.External.Responses
{
    public class CbLcResponse
    {
        public string Customerid { get; set; }
        public string Accountno { get; set; }
        public string CbiReference { get; set; }
        public string Branch { get; set; }
        public string BehalfBranch { get; set; }
        public double? Amount { get; set; }
        public string Currencycode { get; set; }
        public string IssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public double? MarginAmount { get; set; }
        public string MarginAmountCCY { get; set; }
        public double? Balance { get; set; }
        public double? TotalLiability { get; set; }
        public double? TotalDebt { get; set; }
        public double? Remainingamount { get; set; }
        public LcSettleType LcSettleType { get; set; }
        public LcStatus LcStatus { get; set; }
        public LcType LcType { get; set; }
        public List<Parts> Parts { get; set; }
        public Samat Samat { get; set; }
    }

    public class LcSettleType
    {
        public string Code { get; set; }
        public string Title { get; set; }
    }

    public class LcStatus
    {
        public string Code { get; set; }
        public string Title { get; set; }
    }

    public class LcType
    {
        public string Code { get; set; }
        public string Title { get; set; }
    }

    public class Parts
    {
        public long? PartNumber { get; set; }
        public double? PartAmount { get; set; }
        public string PartAmountCCY { get; set; }
        public double? ReceivedAmount { get; set; }
        public string ReceivedAmountCCY { get; set; }
        public string ReceiptDate { get; set; }
        public string PaymentDueDate { get; set; }
    }

    public class Samat
    {
        public Samat28_1 Samat28_1 { get; set; }
        public List<Samat28_5> Samat28_5 { get; set; }
        public List<Samat28_3> Samat28_3 { get; set; }
        public List<SamatCollateral> SamatCollateral { get; set; }
    }

    public class Samat28_1
    {
        public string RequestNumber { get; set; }
        public double? CurrencyRate { get; set; }
        public double? Amount { get; set; }
        public string Ccy { get; set; }
        public string Tax { get; set; }
        public EmployeeCount EmployeeCount { get; set; }
        public string IsicCode { get; set; }
        public string IsicTitle { get; set; }
        public string SubIsicCode { get; set; }
        public string SubIsicTitle { get; set; }
        public string OwnerTypeCode { get; set; }
        public string OwnerTypeTitle { get; set; }
        public string SupplySourceCode { get; set; }
        public string SupplySourceTitle { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerType { get; set; }
        public string ShahabCode { get; set; }
        public string NationalCode { get; set; }
        public string LegalId { get; set; }
        public string Gender { get; set; }
    }

    public class EmployeeCount
    {
        public string Code { get; set; }
        public string Title { get; set; }
    }

    public class Samat28_5
    {
        public Actcode ActCode { get; set; }
        public double? TotalDebtAmount { get; set; }
        public double? BadDebtAmount { get; set; }
        public double? PenaltyClauseAmount { get; set; }
        public double? PenaltyChargeAmount { get; set; }
        public double? DoubtfulAmount { get; set; }
        public double? LiabilityBalanceAmount { get; set; }
        public string ActDate { get; set; }
        public string CreatedDate { get; set; }
    }

    public class Actcode
    {
        public string Code { get; set; }
        public string Title { get; set; }
    }

    public class Samat28_3
    {
        public string CustomerNumber { get; set; }
        public Customertype CustomerType { get; set; }
        public string ShahabCode { get; set; }
        public string Gender { get; set; }
        public string NationalCode { get; set; }
    }

    public class Customertype
    {
        public string Code { get; set; }
        public string Title { get; set; }
    }

    public class SamatCollateral
    {
        public double? CollateralAmount { get; set; }
        public long? Priority { get; set; }
        public string CollTypeCode { get; set; }
        public string CollTypeTitle { get; set; }
        public string CreatedDate { get; set; }
    }
}
