using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using Api.Util;
using Core.GenericResultModel;
using Core.Helpers;
using Core.Logger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Test.TestCases.Middleware;


public class CustomMiddlewareTest
{
    private readonly Mock<ILoggerManager> _logger = new();
    private readonly Mock<IHttpContextAccessor> _accessor = new();
    private readonly Mock<InitSetting> _setting = new();
    private readonly Mock<IWebHostEnvironment> _env = new();
    private readonly Mock<IUserHelper> helper = new();
    private readonly string token = "Bearer " + Environment.GetEnvironmentVariable("Test_Token") ?? "";

    public CustomMiddlewareTest()
    {
        var assembly = Assembly.GetAssembly(typeof(ResourceConfig)) ?? Assembly.GetExecutingAssembly();

        ResourceManager rmEn = new("Core.Resources.Resource-en", assembly);
        ResourceConfig.ResourcesEn = rmEn.GetResourceSet(CultureInfo.CurrentCulture, true, true);

        ResourceManager rmFa = new("Core.Resources.Resource-fa", assembly);
        ResourceConfig.ResourcesFa = rmFa.GetResourceSet(CultureInfo.CurrentCulture, true, true);

        helper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto()
        {
            UserName = "Test",
            Family = "Test",
            Name = "Test",
        });
    }
    [Fact]
    public async Task CustomMiddlewareTest_Invoke()
    {
        //Arrange
        HttpContext ctx = new DefaultHttpContext();
        _accessor.Object.HttpContext = ctx;
        RequestDelegate next = (HttpContext hc) => Task.CompletedTask;
        CustomMiddleware middleware = new(next, _logger.Object, _setting.Object, _env.Object,helper.Object);

        //Act
        await middleware.InvokeAsync(ctx);

        //Create the DefaultHttpContext
        var context = new DefaultHttpContext();
        context.Request.Headers.Append("AID", "channel");
        context.Request.Headers.Append("Authorization", token);
        context.Request.Headers.Append("Authorization", "its for error");
        context.Response.Body = new MemoryStream();

        //Call the middleware
        await middleware.InvokeAsync(context);

        //'en' seems OK for me as the default
        Assert.Equal(200, context.Response.StatusCode);
        // Assert.Equal(0, result?.Code);

    }
    [Fact]
    public async Task CustomMiddlewareTest_Exception()
    {
        //Arrange
        HttpContext ctx = new DefaultHttpContext();
        _accessor.Object.HttpContext = ctx;
        RequestDelegate next = (HttpContext hc) => Task.FromException(new Exception("akbari delmibari exception"));
        CustomMiddleware middleware = new(next, _logger.Object, _setting.Object, _env.Object, helper.Object);

        //Act
        await middleware.InvokeAsync(ctx);

        //Create the DefaultHttpContext
        var context = new DefaultHttpContext();
        context.Request.Headers.Append("AID", "channel");
        context.Request.Headers.Append("Authorization", token);
        context.Request.Headers.Append("Authorization", "its for error");
        context.Response.Body = new MemoryStream();

        //Call the middleware
        await middleware.InvokeAsync(context);

        //'en' seems OK for me as the default
        Assert.Equal(500, context.Response.StatusCode);
        // Assert.Equal(0, result?.Code);

    }
}