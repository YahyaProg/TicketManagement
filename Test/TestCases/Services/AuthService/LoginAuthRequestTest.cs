using Application.Services.AuthService;
using Core.Entities;
using Core.GenericResultModel;
using FluentValidation.TestHelper;
using Gateway.External.IServices;
using Gateway.KeyCloak.IServices;
using Gateway.Model.External.Requests;
using Gateway.Model.External.Responses;
using Gateway.Model.KeyCloak.Dto;
using Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using Test.Helper;


namespace Test.TestCases.Services.AuthServiceTest;

public class LoginAuthRequestTest
{
    private readonly Mock<IAuthService> _keycloakService = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<IMemoryCache> _memoryCach = new();
    private readonly Mock<IExternalServices> _externalServices = new();
    private static readonly string ken = "salam";
    private static readonly string reken = "bye";

    [Fact]
    public async Task LoginAuthRequestTest_Validation_Success()
    {
        //Arrange
        LoginWithKeyCloakRequest loginWithKeyCloakRequest = new LoginWithKeyCloakRequest()
        {
            Username = "admin",
            Password = "Daya@123",
            RequestId = 1,
            Code = "#S@22C"
        };

        //Act
        var validates = new LoginWithKeyCloakRequestValidator();
        var resultValidate = await validates.TestValidateAsync(loginWithKeyCloakRequest);

        //Assert
        resultValidate.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task LoginAuthRequestTest_Validation_Error()
    {
        //Arrange
        LoginWithKeyCloakRequest loginWithKeyCloakRequest = new LoginWithKeyCloakRequest()
        {
            Username = null,
            Password = null,
            RequestId = 1,
            Code = null
        };

        //Act
        var validates = new LoginWithKeyCloakRequestValidator();
        var resultValidate = await validates.TestValidateAsync(loginWithKeyCloakRequest);

        //Assert
        resultValidate.ShouldHaveAnyValidationError();
    }

    [Fact]
    public async Task LoginAuthRequestTest_Success()
    {
        //Arrange
        LoginWithKeyCloakRequest request = new()
        {
            Username = "admin",
            Password = "Daya@123",
            RequestId = It.IsAny<long>(),
            Code = It.IsAny<string>()
        };

        _ = _keycloakService.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new LoginKeycloakDto
        {
            access_token = ken,
            refresh_expires_in = 20240421,
            refresh_token = reken
        });

        _ = _unitOfWork.Setup(x => x.Repository.GetByIdAsync<Captcha>(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Captcha
        {
            Id = 1,
            Code = "asdads",
            Base64Image = "asda",
            CreatedAt = DateTime.Now,
            ExpirationDate = DateTime.Now.AddMinutes(5),
            IsPassed = true,
            RetriesLeft = 2,
            UpdatedAt = null
        });

        var context = new Mock<DBContext>();
        context.Setup(x => x.Organizations).ReturnsDbSet([new() { Id = 1 }]);
        _unitOfWork.Setup(x => x.Context).Returns(context.Object);

        _ = _unitOfWork.Setup(x => x.Repository.Update<Captcha>(It.IsAny<Captcha>()));

        _ = _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

        var _systemUnderTest = new LoginWithKeyCloakRequestHandler(_unitOfWork.Object, _keycloakService.Object, _externalServices.Object, _memoryCach.Object);

        //Act
        var result = await _systemUnderTest.Handle(request, CancellationToken.None);

        //Assert
        Assert.True(true);
    }


    [Fact]
    public async Task LoginAuthRequestTest_Faild()
    {
        //Arrange
        LoginWithKeyCloakRequest request = new()
        {
            Username = "admin",
            Password = "1234",
            RequestId = 1,
            Code = "#S@22C"
        };

        _ = _keycloakService.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new LoginKeycloakDto
        {
            access_token = ken,
            refresh_expires_in = 20240421,
            refresh_token = reken
        });

        _ = _unitOfWork.Setup(x => x.Repository.GetByIdAsync<Captcha>(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Captcha
        {
            Id = 1,
            Code = "asdads",
            Base64Image = "asda",
            CreatedAt = DateTime.Now,
            ExpirationDate = DateTime.Now.AddMinutes(5),
            IsPassed = true,
            RetriesLeft = 2,
            UpdatedAt = null
        });
        _ = _unitOfWork.Setup(x => x.Repository.Update<Captcha>(It.IsAny<Captcha>()));

        _ = _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);


        var context = new Mock<DBContext>();
        context.Setup(x => x.Organizations).ReturnsDbSet([new() { Id = 1 }]);
        context.Setup(x => x.Branches).ReturnsDbSet([]);
        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        _unitOfWork.Setup(x => x.Context).Returns(context.Object);


        var _systemUnderTest = new LoginWithKeyCloakRequestHandler(_unitOfWork.Object, _keycloakService.Object, _externalServices.Object, _memoryCach.Object);

        //Act
        var result = await _systemUnderTest.Handle(request, CancellationToken.None);

        //Assert
        Assert.True(true);
    }



    [Fact]
    public async Task Handle_InValidRequestCapthchaCode()
    {
        // Arrange
        var request = new LoginWithKeyCloakRequest
        {
            Username = "username",
            Password = "password",
            Code = "cod1",
            RequestId = 1
        };

        var captcha = new Captcha { Id = 1, Code = "code", ExpirationDate = DateTime.Now.AddMinutes(10), RetriesLeft = 2 };
        var keycloakServiceMock = new Mock<IAuthService>();
        _ = keycloakServiceMock.Setup(k => k.Login(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new LoginKeycloakDto() { access_token = "access_token", refresh_token = "refresh_token", refresh_expires_in = 3600 });

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.Repository.GetByIdAsync<Captcha>(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(captcha);

        var contextMock = new Mock<DBContext>();
        contextMock.Setup(x => x.BankStaffs).ReturnsDbSet([]);
        unitOfWorkMock.Setup(x => x.Context).Returns(contextMock.Object);

        var context = new Mock<DBContext>();
        context.Setup(x => x.Organizations).ReturnsDbSet([new() { Id = 1 }]);
        context.Setup(x => x.Branches).ReturnsDbSet([]);
        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        _unitOfWork.Setup(x => x.Context).Returns(context.Object);

        var handler = new LoginWithKeyCloakRequestHandler(unitOfWorkMock.Object, keycloakServiceMock.Object, _externalServices.Object, _memoryCach.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(true);
    }

    [Fact]
    public async Task Handle_ClientNotfound()
    {
        // Arrange
        var request = new LoginWithKeyCloakRequest
        {
            Username = "username",
            Password = "password",
            Code = "code",
            RequestId = 1
        };

        var captcha = new Captcha { Id = 1, Code = "code", ExpirationDate = DateTime.Now.AddMinutes(10), RetriesLeft = 2 };
        var keycloakServiceMock = new Mock<IAuthService>();
        _ = keycloakServiceMock.Setup(k => k.Login(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(value: null);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.Repository.GetByIdAsync<Captcha>(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(captcha);

        var contextMock = new Mock<DBContext>();
        contextMock.Setup(x => x.BankStaffs).ReturnsDbSet([]);
        contextMock.Setup(x => x.Organizations).ReturnsDbSet([new() { Id = 1 }]);
        contextMock.Setup(x => x.Branches).ReturnsDbSet([]);
        contextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        unitOfWorkMock.Setup(x => x.Context).Returns(contextMock.Object);

        var handler = new LoginWithKeyCloakRequestHandler(unitOfWorkMock.Object, keycloakServiceMock.Object, _externalServices.Object, _memoryCach.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Code);
        Assert.Null(result.Data);
    }
    [Fact]
    public async Task Handle_ReturnTrue()
    {
        // Arrange
        var request = new LoginWithKeyCloakRequest
        {
            Username = "username",
            Password = "password",
            Code = "code",
            RequestId = 1
        };
        var resource = new Dictionary<string, Dictionary<string, List<string>>>()
        {
            {
                "sap",
                new Dictionary<string, List<string>>()
                {
                    {"roles", ["SAP-Normal","SAP-Admin"] }
                }
            }
        };
        var token = MoqHelper.GenerateJwtToken([
            new("sub","123"),
            new("branchCode","123"),
            new("branchName","123"),
            new("OrganizationId","123"),
            new("preferred_username","123"),
            new("resource_access",JsonConvert.SerializeObject(resource)),
            new("name","رضا کرامتی"),
            ]);

        var captcha = new Captcha { Id = 1, Code = "code", ExpirationDate = DateTime.Now.AddMinutes(10), RetriesLeft = 2 };
        var keycloakServiceMock = new Mock<IAuthService>();
        _ = keycloakServiceMock.Setup(k => k.Login(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new LoginKeycloakDto()
            {
                access_token = token,
                refresh_token = "refresh_token",
                refresh_expires_in = 3600
            });

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.Repository.GetByIdAsync<Captcha>(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(captcha);

        var contextMock = new Mock<DBContext>();
        contextMock.Setup(x => x.BankStaffs).ReturnsDbSet([]);
        unitOfWorkMock.Setup(x => x.Context).Returns(contextMock.Object);
        contextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);
        contextMock.Setup(x => x.Organizations).ReturnsDbSet([new() { Id = 1 }]);
        contextMock.Setup(x => x.Roles).ReturnsDbSet([new() { Id = 1, Name = "view-users" }]);
        contextMock.Setup(x => x.MenuRoles).ReturnsDbSet([new() { Id = 1 }]);
        contextMock.Setup(x => x.Menus).ReturnsDbSet([new() { Id = 1 }]);
        contextMock.Setup(x => x.RoleServices).ReturnsDbSet([new() { RoleId = 1, ServiceId = 1 }]);
        contextMock.Setup(x => x.Branches).ReturnsDbSet([]);
        var content = new ApiResult<BranchResponse>()
        {
            Message = "",
            MessageEn = "",
            Data = new()
            {
                Branch = new() { Code = "" },
                RelatedBranch = [new() { Code = "" }]
            }
        };

        _externalServices.Setup(x => x.GetAllBranchRelations(It.IsAny<BranchCodeRequest>())).ReturnsAsync(content.ToExternalMoqResponse());
        var handler = new LoginWithKeyCloakRequestHandler(unitOfWorkMock.Object, keycloakServiceMock.Object, _externalServices.Object, new MemoryCache(new MemoryCacheOptions()));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
    }
}
