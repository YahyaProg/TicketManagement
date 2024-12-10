namespace Gateway.Model.External.Responses;

public class IranianScoreInquiryResponse
{
    public string MobileNumber { get; set; }
    public string NationalCode { get; set; }
    public string ReportLink { get; set; }
    public bool? NegativeChequeStatus { get; set; }
    public string Risk { get; set; }
    public long? RiskScore { get; set; }
    public string HashedData { get; set; }
    public string ReportDate { get; set; }
    public string ScoreReportJson { get; set; }
}
