using Application.Services.AuthService;
using FluentValidation.TestHelper;
using Gateway.KeyCloak.IServices;
using Gateway.Model.KeyCloak.Dto;
using Moq;
using static Application.Services.AuthService.RefreshWithKeyCloakRequest;

namespace Test.TestCases.Services.AuthServiceTest;

public class RefreshAuthRequestTest
{

    private readonly Mock<IAuthService> _keycloakService = new();
    private static readonly string ken = "salam";
    private static readonly string reken = "bye";


    [Fact]
    public async Task RefreshAuthRequestTest_Validation_Success()
    {
        //Arrange
        RefreshWithKeyCloakRequest refreshWithKeyCloakRequest = new RefreshWithKeyCloakRequest()
        {
            Token = ken,
        };

        //Act
        var validates = new RefreshWithKeyCloakRequestValidate();
        var resultValidate = await validates.TestValidateAsync(refreshWithKeyCloakRequest);

        //Assert
        resultValidate.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task LoginAuthRequestTest_Validation_Error()
    {
        //Arrange
        RefreshWithKeyCloakRequest refreshWithKeyCloakRequest = new RefreshWithKeyCloakRequest()
        {
            Token = null
        };

        //Act
        var validates = new RefreshWithKeyCloakRequestValidate();
        var resultValidate = await validates.TestValidateAsync(refreshWithKeyCloakRequest);

        //Assert
        resultValidate.ShouldHaveAnyValidationError();
    }

    [Fact]
    public async Task LoginAuthRequestTest_Success()
    {
        //Arrange
        RefreshWithKeyCloakRequest request = new RefreshWithKeyCloakRequest()
        {
            Token = ken,
        };

        _ = _keycloakService.Setup(x => x.RefreshToken(It.IsAny<string>())).ReturnsAsync(new LoginKeycloakDto
        {
            access_token = ken,
            refresh_expires_in = 20240421,
            refresh_token = reken
        });

        var _systemUnderTest = new RefreshWithKeyCloakRequestHandler(_keycloakService.Object);

        //Act
        var result = await _systemUnderTest.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
    }


    [Fact]
    public async Task LoginAuthRequestTest_Faild()
    {
        //Arrange
        RefreshWithKeyCloakRequest request = new RefreshWithKeyCloakRequest()
        {
            Token = "FalidData",
        };

        _ = _keycloakService.Setup(x => x.RefreshToken(It.IsAny<string>())).ReturnsAsync(new LoginKeycloakDto
        {
            access_token = ken,
            refresh_expires_in = 20240421,
            refresh_token = reken
        });

        var _systemUnderTest = new RefreshWithKeyCloakRequestHandler(_keycloakService.Object);

        //Act
        var result = await _systemUnderTest.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
    }
}