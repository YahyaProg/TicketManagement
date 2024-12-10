using Core.CustomAttribute;

namespace Gateway.Model.External.Requests
{
    public class CbCustomerRequest
    {
        public long? Customerid { get; set; }

        [MaxLengthFa(10), IsValid(CustomeValidation = ECustomeValidation.NationalId)]
        public string Nationalidno { get; set; }
    }
}
