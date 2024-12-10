using System.Collections.Generic;

namespace Gateway.Model.External.Responses;

public class SamatEstelamByRqNumberResponse
{
    public string BadHesabiDate { get; set; }
    public string Country { get; set; }
    public string CustomerType { get; set; }
    public string DateEstlm { get; set; }
    public string Fname { get; set; }
    public string LegalId { get; set; }
    public string Lname { get; set; }
    public string NationalCd { get; set; }
    public string ShenaseEstlm { get; set; }
    public string ShenaseRes { get; set; }
    public List<EstelamAsliRow> EstelamAsliRows { get; set; }
}

public class EstelamAsliRow
{
    public string AdamSabtSanadEntezami { get; set; }
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
    public string ArzCode { get; set; }
    public string BankCode { get; set; }
    public string DasteBandi { get; set; }
    public string Date { get; set; }
    public string DateEstehal { get; set; }
    public string DateSarResid { get; set; }
    public string Estehal { get; set; }
    public string HadafAzDaryaft { get; set; }
    public string MainIdNo { get; set; }
    public string MainLName { get; set; }
    public string MainLgId { get; set; }
    public string MainName { get; set; }
    public string PlaceCdMasraf { get; set; }
    public string RequestNum { get; set; }
    public string RequstType { get; set; }
    public string RsrcTamin { get; set; }
    public string ShobeCode { get; set; }
    public string ShobeName { get; set; }
    public string Type { get; set; }
}