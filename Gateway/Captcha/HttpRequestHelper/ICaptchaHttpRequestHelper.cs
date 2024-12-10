using Newtonsoft.Json;
using Request_Helper.HttpRquestHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.Captcha.HttpRequestHelper
{
    public interface ICaptchaHttpRequestHelper : IHttpRequest
    {
        Task<TResponse> HttpGetAsync<TResponse>(string url);
        Task<TResponse> HttpPostAsync<TRequest, TResponse>(TRequest parameters, string url);
    }

    public class CaptchaHttpRequestHelper : HttpRequest, ICaptchaHttpRequestHelper
    {
        public async Task<TResponse> HttpGetAsync<TResponse>(string url)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            System.Net.Http.HttpResponseMessage res = await HttpGetAsync(url, headers);

            string readContent = await res.Content.ReadAsStringAsync();
            TResponse result = JsonConvert.DeserializeObject<TResponse>(readContent);
            return result;
        }


        public async Task<TResponse> HttpPostAsync<TRequest, TResponse>(TRequest parameters, string url = null)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();

            System.Net.Http.HttpResponseMessage res = await HttpPostAsync(parameters, url, headers);

            string readContent = await res.Content.ReadAsStringAsync();
            TResponse result = JsonConvert.DeserializeObject<TResponse>(readContent);

            return result;
        }
    }
}
