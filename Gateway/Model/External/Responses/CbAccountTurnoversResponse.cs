namespace Gateway.Model.External.Responses;

public class CbAccountTurnoverResponse
{
    public string AccountNo { get; set; }
    public string AccountType { get; set; }
    public string AccountStatus { get; set; }
    public string OpeningDate { get; set; }
    public string Currency { get; set; }
    public string Branch { get; set; }
    public string CreditTurnoverYear { get; set; }
    public string DebitTurnoverYear { get; set; }
    public string AvgBalanceYear { get; set; }
    public string CreditTurnoverHalf { get; set; }
    public string DebitTurnoverHalf { get; set; }
    public string AvgBalanceHalf { get; set; }
    public string CreditTurnoverQuarter { get; set; }
    public string DebitTurnoverQuarter { get; set; }
    public string AvgBalanceQuarter { get; set; }
    public string FinalBalance { get; set; }
    public string InterestRate { get; set; }
    public long? CountDebitTurnoverHalf { get; set; }
    public long? CountDebitTurnoverQuarter { get; set; }
    public long? CountDebitTurnoverYear { get; set; }
    public long? CountCreditTurnoverHalf { get; set; }
    public long? CountCreditTurnoverQuarter { get; set; }
    public long? CountCreditTurnoverYear { get; set; }
}
