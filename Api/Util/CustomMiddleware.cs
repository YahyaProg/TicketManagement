using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.GenericResultModel;
using Core.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Util;

public class CustomMiddleware(RequestDelegate _next,
    IWebHostEnvironment _env,
    IUserHelper userHelper)
{
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        DateTime startDate = DateTime.Now;
        string url = httpContext.Request.Path.Value;
        //if (httpContext.User.Identity.IsAuthenticated)
        //{
        //    user = userHelper.GetUserFromToken();
        //}
        if (new List<string>() { "/swagger" }.Any(u => url.Contains(u, StringComparison.CurrentCultureIgnoreCase)))
        {
            try
            {
                await _next(httpContext);
            }
            catch
            { /* do nothing */ }
        }
        else
        {
            Stream originalBody = httpContext.Response.Body;
            try
            {
                var requestCode = Guid.NewGuid().ToString();


                using (var memStream = new MemoryStream())
                {
                    httpContext.Response.Body = memStream;

                    await _next(httpContext);


                    memStream.Position = 0;
                    string response = await new StreamReader(memStream).ReadToEndAsync();

                    memStream.Position = 0;
                    httpContext.Response.Headers.Append("x-frame-option", "Deny");

                    await memStream.CopyToAsync(originalBody);
                }

            }
            catch (Exception ex)
            {
                httpContext.Response.Body = originalBody;
                await HandleExceptionAsync(httpContext, ex, startDate);
            }
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, DateTime startDate)
    {
        
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        string jsonstring = string.Empty;
        var camelSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        if (_env.IsDevelopment())
        {
            jsonstring = JsonConvert.SerializeObject(new ApiResult(500, false) { Message = ex.Message },camelSettings).ToString();
            await context.Response.WriteAsync(jsonstring, Encoding.UTF8);
        }
        else
        {

            if (ex is TaskCanceledException /*|| ex is System.Data.SqlClient.SqlException) */&& ex.Message.Contains("Timeout"))
            {
                jsonstring = JsonConvert.SerializeObject(new ApiResult(408, false), camelSettings).ToString();
            }
            else
            {
                jsonstring = JsonConvert.SerializeObject(new ApiResult(500, false) { Message = "در حال حاضر امکان پردازش تراکنش شما فراهم نیست" }, camelSettings).ToString();
            }
            await context.Response.WriteAsync(jsonstring, Encoding.UTF8);
        }
    }
}
