using Core.GenericResultModel;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using RestEase;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.Helper;

public static class MoqHelper
{
    public static HttpClient getHttpClientMoq()
    {
        var _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
        var _httpClient = new HttpClient(_mockHttpMessageHandler.Object);

        return _httpClient;
    }

    public static Response<T> ToExternalMoqResponse<T>(this T content)
    {
        var f = new Func<T>(() => content);
        var externalResponse = new Response<T>(null, new HttpResponseMessage(), f);
        return externalResponse;
    }

    public static HttpContextAccessor GetHttpContextAccessor(Dictionary<string, string>? headers = null, Dictionary<object, object>? items = null, IEnumerable<Claim>? claims = null)
    {
        var accessor = new HttpContextAccessor
        {
            HttpContext = new DefaultHttpContext()
        };

        if (headers is not null && headers.Count != 0)
            foreach (var item in headers)
                accessor.HttpContext.Request.Headers.Append(item.Key, item.Value);

        if (items is not null && items.Count != 0)
            foreach (var item in items)
                accessor.HttpContext.Items.Add(item.Key, item.Value);

        if (claims is not null)
            accessor.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));

        return accessor;
    }

    public static MoqCollection GetUnitOfWorkMoqCollection()
    {
        var collection = new MoqCollection();

        Mock<IDbContextTransaction> _transaction = new();

        collection.Repository.Setup(x => x.BeginTransactionAsync(System.Data.IsolationLevel.Unspecified, CancellationToken.None))
            .ReturnsAsync(_transaction.Object);

        collection.UnitOfWork.Setup(x => x.Context).Returns(collection.Context.Object);

        collection.UnitOfWork.Setup(x => x.Repository).Returns(collection.Repository.Object);

        // database transaction
        var contextDataBase = new Mock<DatabaseFacade>(collection.Context.Object);
        contextDataBase.Setup(x => x.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(_transaction.Object);
        collection.Context.Setup(x => x.Database).Returns(contextDataBase.Object);

        return collection;
    }

    public static Response<ApiResult<T>> GetExternalResponseMoq<T>(this T data, bool? IsSuccess = null)
    {
        var jsonContent = JsonConvert.SerializeObject(new ApiResult<T>(200, true) { Data = data, Message = "موفق", MessageEn = "success" });
        var response = new Response<ApiResult<T>>(
            jsonContent, // string? response
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            },
            () => new ApiResult<T>()
            {
                Message = "ajaaab",
                MessageEn = "ajaaab",
                Data = data,
                IsSuccess = IsSuccess ?? true
            }
        );

        return response;
    }

    public static string GenerateJwtToken(List<Claim> claims)
    {
        #pragma warning disable S6781 // JWT secret keys should not be disclosed
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes("aVeryLongSecretKeyThatIsAtLeast32BytesLong"));
        #pragma warning restore S6781 // JWT secret keys should not be disclosed
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public class MoqCollection
    {
        public Mock<IRepository> Repository { get; set; } = new();
        public Mock<DBContext> Context { get; set; } = new();
        public Mock<IUnitOfWork> UnitOfWork { get; set; } = new();
    }

}
