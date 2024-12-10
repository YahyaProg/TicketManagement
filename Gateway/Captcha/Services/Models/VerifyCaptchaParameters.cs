using System;

namespace Gateway.Captcha.Services.Models
{
    public class VerifyCaptchaParameters
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
    }
}