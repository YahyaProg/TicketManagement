//using System;
//using FluentResults;
//using Microsoft.Extensions.Configuration;
//using System.Threading.Tasks;
//using Gateway.Captcha.HttpRequestHelper;
//using Gateway.Captcha.Services.Models;
//using Core.GenericResultModel;

//namespace Gateway.Captcha.Services
//{
//    public class CaptchaService : ICaptchaService
//    {
//        private readonly IConfiguration _config;
//        private readonly ICaptchaHttpRequestHelper _httpHelper;

//        public CaptchaService(IConfiguration configuration, ICaptchaHttpRequestHelper httpHelper)
//        {
//            _config = configuration;
//            _httpHelper = httpHelper;
//        }

//        public async Task<ApiResult<VerifyCaptchaOutputDto>> Verify(Guid id, string code)
//        {
//            var result = new Result<VerifyCaptchaOutputDto>();
//            var url = _config["Captcha:BaseUrl"] + _config["Captcha:VerifyUrl"];
//            var input = new VerifyCaptchaParameters()
//            {
//                Id = id,
//                Code = code
//            };

//            var output = await _httpHelper.HttpPostAsync<VerifyCaptchaParameters, VerifyCaptchaOutput>(input, url);
//            if (output is { IsSuccess: true })
//                return new ApiResult<VerifyCaptchaOutputDto>()
//                {
//                    StatusCode = EStatusCode.Success,
//                    Data = output.Data
//                };
//            else
//                return new ApiResult<VerifyCaptchaOutputDto>()
//                {
//                    StatusCode = EStatusCode.BadRequest,
//                    ErrorCode = 438
//                };
//        }
//    }
//}
