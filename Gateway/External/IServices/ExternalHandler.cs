using Core.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Gateway.External.IServices;

public class ExternalHandler(InitSetting initSetting) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Clear();
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.Add("XApiKey", initSetting.ExternalSettings.ApiKey);
        return base.SendAsync(request, cancellationToken);
    }
}
