namespace Gateway.Model.External.Responses;

public class GetIdentificationResponse
{
    public string NationalCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
    public string ShenasnameSeri { get; set; }
    public string ShenasnameSerial { get; set; }
    public string ShenasnameNo { get; set; }
    public string BirthDate { get; set; }
    public int? Gender { get; set; }
    public bool? DeathStatus { get; set; }
    public string DeathDate { get; set; }
}
