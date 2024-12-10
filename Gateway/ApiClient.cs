using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gateway;

public interface IApiClient
{
    Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string> headers = null, int timeOutSecond = 10);
    Task<TOut> GetAsync<TOut>(string url, Dictionary<string, string> headers = null, int timeOutSecond = 10);
    Task<HttpResponseMessage> DeleteAsync(string url, Dictionary<string, string> headers = null, int timeOutSecond = 10);
    Task<TOut> DeleteAsync<TOut>(string url, Dictionary<string, string> headers = null, int timeOutSecond = 10);
    Task<HttpResponseMessage> PutAsync<TIn>(string url, TIn content, Dictionary<string, string> headers = null, int timeOutSecond = 10);
    Task<TOut> PutAsync<TIn, TOut>(string url, TIn content, Dictionary<string, string> headers = null, int timeOutSecond = 10);
    Task<HttpResponseMessage> PatchAsync<TIn>(string url, TIn content, Dictionary<string, string> headers = null, int timeOutSecond = 10);
    Task<TOut> PatchAsync<TIn, TOut>(string url, TIn content, Dictionary<string, string> headers = null, int timeOutSecond = 10);
    Task<HttpResponseMessage> PostAsync<TIn>(string url, TIn content, Dictionary<string, string> headers = null, int timeOutSecond = 10);
    Task<TOut> PostAsync<TIn, TOut>(string url, TIn content, Dictionary<string, string> headers = null, int timeOutSecond = 10);
    Task<HttpResponseMessage> PostAsync<TIn>(string url, TIn content, string httpClientName, Dictionary<string, string> headers = null, int timeOutSecond = 10);
    Task<TOut> PostAsync<TIn, TOut>(string url, TIn content, string httpClientName, Dictionary<string, string> headers = null, int timeOutSecond = 10);
    Task<TOut> PostFormUrlEncodedAsync<TOut>(string url, Dictionary<string, string> parameters, Dictionary<string, string> headers = null, int timeOutSecond = 10);
    Task<HttpResponseMessage> PostFormUrlEncodedAsync(string url, Dictionary<string, string> parameters, Dictionary<string, string> headers = null, int timeOutSecond = 10);
}

public class ApiClient(HttpClient _httpClient, IHttpClientFactory _httpClientFactory) : IApiClient
{

    Dictionary<string, string> _headers = new Dictionary<string, string>();

    private static TimeSpan DefaultTimeout { get=> TimeSpan.FromSeconds(100); }


    public async Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string> headers = null, int timeOutSecond = 10)
    {
        AddHeaders(headers);
        if (_httpClient.Timeout == DefaultTimeout) _httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);

        HttpResponseMessage response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseContentRead);
        return response;
    }
    public async Task<TOut> GetAsync<TOut>(string url, Dictionary<string, string> headers = null, int timeOutSecond = 10)
    {
        AddHeaders(headers);
        if (_httpClient.Timeout == DefaultTimeout) _httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);

        HttpResponseMessage response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseContentRead);
        string contentStr = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TOut>(contentStr);
    }
    public async Task<HttpResponseMessage> DeleteAsync(string url, Dictionary<string, string> headers = null, int timeOutSecond = 10)
    {
        AddHeaders(headers);
        if (_httpClient.Timeout == DefaultTimeout) _httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);

        return await _httpClient.DeleteAsync(url);
    }
    public async Task<TOut> DeleteAsync<TOut>(string url, Dictionary<string, string> headers = null, int timeOutSecond = 10)
    {
        AddHeaders(headers);
        if (_httpClient.Timeout == DefaultTimeout) _httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);

        HttpResponseMessage response = await _httpClient.DeleteAsync(url);
        string contentStr = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TOut>(contentStr);
    }
    public async Task<HttpResponseMessage> PutAsync<TIn>(string url, TIn content, Dictionary<string, string> headers = null, int timeOutSecond = 10)
    {
        AddHeaders(headers);
        if (_httpClient.Timeout == DefaultTimeout) _httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);

        return await _httpClient.PutAsync(url, CreateHttpContent(content, headers));
    }
    public async Task<TOut> PutAsync<TIn, TOut>(string url, TIn content, Dictionary<string, string> headers = null, int timeOutSecond = 10)
    {
        AddHeaders(headers);
        if (_httpClient.Timeout == DefaultTimeout) _httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);

        HttpResponseMessage response = await _httpClient.PutAsync(url, CreateHttpContent(content, headers));
        string contentStr = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TOut>(contentStr);

    }
    public async Task<HttpResponseMessage> PatchAsync<TIn>(string url, TIn content, Dictionary<string, string> headers = null, int timeOutSecond = 10)
    {
        AddHeaders(headers);
        if (_httpClient.Timeout == DefaultTimeout) _httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);

        return await _httpClient.PatchAsync(url, CreateHttpContent(content, headers));

    }
    public async Task<TOut> PatchAsync<TIn, TOut>(string url, TIn content, Dictionary<string, string> headers = null, int timeOutSecond = 10)
    {
        AddHeaders(headers);
        if (_httpClient.Timeout == DefaultTimeout) _httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);

        var response = await _httpClient.PatchAsync(url, CreateHttpContent(content, headers));
        string contentStr = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TOut>(contentStr);

    }
    public async Task<HttpResponseMessage> PostAsync<TIn>(string url, TIn content, Dictionary<string, string> headers = null, int timeOutSecond = 10)
    {
        AddHeaders(headers);
        if (_httpClient.Timeout == DefaultTimeout) _httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);

        return await _httpClient.PostAsync(url, CreateHttpContent(content, headers));
    }
    public async Task<TOut> PostAsync<TIn, TOut>(string url, TIn content, Dictionary<string, string> headers = null, int timeOutSecond = 10)
    {
        AddHeaders(headers);
        if (_httpClient.Timeout == DefaultTimeout) _httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);

        HttpResponseMessage response = await _httpClient.PostAsync(url, CreateHttpContent(content, headers));
        string contentStr = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TOut>(contentStr);
    }
    public async Task<HttpResponseMessage> PostAsync<TIn>(string url, TIn content, string httpClientName, Dictionary<string, string> headers = null, int timeOutSecond = 10)
    {
        using var httpClient = _httpClientFactory.CreateClient(httpClientName);
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        foreach (var item in headers.Where(h => !h.Key.Equals("content-type", StringComparison.CurrentCultureIgnoreCase)))
            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);

        return await httpClient.PostAsync(url, CreateHttpContent(content, headers));
    }
    public async Task<TOut> PostAsync<TIn, TOut>(string url, TIn content, string httpClientName, Dictionary<string, string> headers = null, int timeOutSecond = 10)
    {
        using var httpClient = _httpClientFactory.CreateClient(httpClientName);
        httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);
        foreach (var item in headers.Where(h => !h.Key.Equals("content-type", StringComparison.CurrentCultureIgnoreCase)))
            httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);

        var response = await httpClient.PostAsync(url, CreateHttpContent(content, headers));
        var contentStr = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TOut>(contentStr);
    }
    public async Task<TOut> PostFormUrlEncodedAsync<TOut>(string url, Dictionary<string, string> parameters, Dictionary<string, string> headers = null, int timeOutSecond = 10)
    {
        AddHeaders(headers);
        if (_httpClient.Timeout == DefaultTimeout) _httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);

        List<KeyValuePair<string, string>> nameValueCollection = [];
        foreach (KeyValuePair<string, string> item in parameters)
            nameValueCollection.Add(new KeyValuePair<string, string>(item.Key, item.Value));

        FormUrlEncodedContent requestContent = new(nameValueCollection);

        HttpResponseMessage response = await _httpClient.PostAsync(url.ToString(), requestContent);
        string contentStr = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TOut>(contentStr);
    }
    public async Task<HttpResponseMessage> PostFormUrlEncodedAsync(string url, Dictionary<string, string> parameters, Dictionary<string, string> headers = null, int timeOutSecond = 10)
    {
        AddHeaders(headers);
        if (_httpClient.Timeout == DefaultTimeout) _httpClient.Timeout = TimeSpan.FromSeconds(timeOutSecond);

        List<KeyValuePair<string, string>> nameValueCollection = [];
        foreach (KeyValuePair<string, string> item in parameters)
            nameValueCollection.Add(new KeyValuePair<string, string>(item.Key, item.Value));

        FormUrlEncodedContent requestContent = new(nameValueCollection);
        HttpResponseMessage response = await _httpClient.PostAsync(url.ToString(), requestContent);
        return response;
    }

    private static HttpContent CreateHttpContent<TIn>(TIn content, Dictionary<string, string> headers)
    {
        string json = "";
        if (content != null)
            json = JsonConvert.SerializeObject(content);

        string contentType = "application/json";
        if (headers.Any(h => h.Key.Equals("content-type", StringComparison.CurrentCultureIgnoreCase)))
            contentType = headers["content-type"];

        return new StringContent(json, Encoding.UTF8, contentType);
    }

    private void AddHeaders(Dictionary<string, string> headers)
    {
        _httpClient.DefaultRequestHeaders.Clear();

        headers ??= [];
        _headers = headers;

        foreach (KeyValuePair<string, string> item in _headers.Where(h => !h.Key.Equals("content-type", StringComparison.CurrentCultureIgnoreCase)))
            _httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
    }
}
