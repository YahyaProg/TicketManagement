using Gateway.KeyCloak.Services;
using Gateway.Model.KeyCloak.Dto;
using Infrastructure.Repositories.Setting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace Test.TestCases.Gateways.KeyCloakTest;

public class AuthServiceTests
{
    private const string BaseUrl = "https://example.com";
    private const string AuthPath = "/realms/master/protocol/openid-connect/token";
    private const string ClientId = "test-client-id";
    private const string ClientSecret = "test-client-secret";



    [Fact]
    public async Task RefreshToken_InValidToken_ReturnsNull()
    {
        // Arrange
        var refreshToken = "valid-refresh-token";
        var expectedResponse = new LoginKeycloakDto
        {
            access_token = "invalid-access-token",
            refresh_token = "invalid-refresh-token",
            refresh_expires_in = 3600
        };

        var mockDbSettings = new Mock<IDbSettings>();
        mockDbSettings.Setup(s => s.GetSetting("keycloak_connection_url")).Returns(BaseUrl);
        mockDbSettings.Setup(s => s.GetSetting("keycloak_resource")).Returns(ClientId);
        mockDbSettings.Setup(s => s.GetSetting("keycloak_c_s")).Returns(ClientSecret);

        var mockHttpHandler = CreateMockHttpHandler(
            HttpStatusCode.OK,
            JsonConvert.SerializeObject(expectedResponse)
        );

        var httpClient = new HttpClient(mockHttpHandler.Object);
        var authService = new AuthService(mockDbSettings.Object);

        // Act
        var result = await authService.RefreshToken(refreshToken);

        // Assert
        Assert.Null(result);

    }

    [Fact]
    public async Task Login_InvalidCredentials_ReturnsNull()
    {
        // Arrange
        var username = "invaliduser";
        var password = "invalidpassword";

        var mockDbSettings = new Mock<IDbSettings>();
        mockDbSettings.Setup(s => s.GetSetting("keycloak_connection_url")).Returns(BaseUrl);
        mockDbSettings.Setup(s => s.GetSetting("keycloak_resource")).Returns(ClientId);
        mockDbSettings.Setup(s => s.GetSetting("keycloak_c_s")).Returns(ClientSecret);

        var mockHttpHandler = CreateMockHttpHandler(HttpStatusCode.BadRequest, "");

        var httpClient = new HttpClient(mockHttpHandler.Object);
        var authService = new AuthService(mockDbSettings.Object);

        // Act
        var result = await authService.Login(username, password);

        // Assert
        Assert.Null(result);

    }

    private Mock<HttpMessageHandler> CreateMockHttpHandler(HttpStatusCode statusCode, string responseContent)
    {
        var mockHandler = new Mock<HttpMessageHandler>();

        mockHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(responseContent)
            });

        return mockHandler;
    }
}