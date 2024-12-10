using System.ComponentModel.DataAnnotations;

namespace Gateway.Model.External.Requests;

public class IranianScoreInquiryRequest
{
    [Required(ErrorMessage = "کد ملی مشتری وارد نشده است!")]
    [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessage = "کد ملی باید 10 رقم باشد")]
    public string NationalCode { get; set; }
}
