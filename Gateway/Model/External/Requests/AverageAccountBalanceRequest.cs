using Core.CustomAttribute;

namespace Gateway.Model.External.Requests
{
    public class AverageAccountBalanceRequest
    {
        [MaxLengthFa(10), IsValid(CustomeValidation = ECustomeValidation.NationalId)]
        public string NationalId { get; set; }
        public string ShFromDate { get; set; }
        public string ShToDate { get; set; }
    }
}
