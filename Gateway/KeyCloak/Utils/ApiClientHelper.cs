
using Core.GenericResultModel;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Infrastructure.Repositories.Setting;
using Infrastructure;

namespace Gateway.KeyCloak.Utils
{
    public class ApiClientHelper(IHttpContextAccessor _httpContextAccessor, IDbSettings _dbSetting)
    {
        public Dictionary<string, string> SetAuthorizationHeader()
        {
            Dictionary<string, string> headers = new() { };
            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var headerAuth))
            {
                headers.Add("Authorization", headerAuth.ToString());
            }

            return headers;
        }

        public static async Task<T> ConvertJsonToData<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(responseBody);
            return result;
        }

        public static ApiResult<T> FaildReturn<T>(HttpStatusCode httpStatusCode, string msg = null, string msgEn = null)
        {
            var result = new ApiResult<T>();
            
            result.Code =  (int)httpStatusCode;
            result.IsSuccess = false;
            
            if (msg is not null)
                result.Message = msg;
            
            if (msgEn is not null)
                result.MessageEn = msgEn;

            return result;
        }

        public static ApiResult<T> SuccessReturn<T>(T data)
        {
            return new ApiResult<T> { Data = data, Code = 0 };
        }

        public string ServiceAddress(string serviceAddress)
        {

            var basePath = _dbSetting.GetSetting("keycloak_connection_url");

            var servicePath = serviceAddress;
            var url = basePath + servicePath;
            return url;
        }
    }
}
