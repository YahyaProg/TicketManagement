using Core.Entities;
using Core.ViewModel.AccountModuleDesc;
using FluentValidation.TestHelper;
using Infrastructure;
using Moq;
using static Application.Services.AccountModuleDescService.AddRangeAccountModuleDescRequest;
using AccountModuleDescServiceUsing = Application.Services.AccountModuleDescService;

namespace Test.TestCases.Services.AccountModuleDesc.AddRangeAccountModuleDescRequest;


public class AddRangeAccountModuleDescRequestTest
{
    private readonly Mock<DBContext> context = new();

    [Fact]
    public async Task AddRangeAccountModuleDescRequest_Validation_Success()
    {
        //Arrange
        AccountModuleDescServiceUsing.AddRangeAccountModuleDescRequest request = new()
        {
            AccountModuleDescs = [
                new AccountModuleDescInputVM()
                {
                    Code = "code",
                    Title = "title",
                    Version = 1
                },
                new AccountModuleDescInputVM()
                {
                    Code = "code2",
                    Title = "title2",
                    Version = 2
                }
            ]
        };

        //Act
        var validations = new AddRangeAccountModuleDescRequestValidator();
        var resultValidate = await validations.TestValidateAsync(request);

        //Assert
        resultValidate.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task AddRangeAccountModuleDescRequest_Validation_Error()
    {
        //Arrange
        AccountModuleDescServiceUsing.AddRangeAccountModuleDescRequest request = new()
        {
            AccountModuleDescs = [
              new AccountModuleDescInputVM()
                {
                    Code = "code",
                    Title = null,
                    Version = 1
                },
                new AccountModuleDescInputVM()
                {
                    Code = "code2",
                    Title = "title2",
                    Version = null
                }
          ]
        };

        //Act
        var validations = new AddRangeAccountModuleDescRequestValidator();
        var resultValidate = await validations.TestValidateAsync(request);

        //Assert
        resultValidate.ShouldHaveAnyValidationError();
    }

    [Fact]
    public async Task AddRangeAccountModuleDescRequest_Success()
    {
        //Arrange
        AccountModuleDescServiceUsing.AddRangeAccountModuleDescRequest request = new()
        {
            AccountModuleDescs = [
             new AccountModuleDescInputVM()
                {
                    Code = "code",
                    Title = "title",
                    Version = 1
                },
                new AccountModuleDescInputVM()
                {
                    Code = "code2",
                    Title = "title2",
                    Version = 2
                }
         ]
        };

        context.Setup(x => x.AccountModuleDescs.AddRange(It.IsAny<Core.Entities.AccountModuleDesc[]>()));
        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

        var _systemUnderTest = new AddRangeAccountModuleDescRequestHandler(context.Object);

        //Act
        var result = await _systemUnderTest.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task AddRangeAccountModuleDescRequest_Fail()
    {
        //Arrange
        AccountModuleDescServiceUsing.AddRangeAccountModuleDescRequest request = new()
        {
            AccountModuleDescs = [
             new AccountModuleDescInputVM()
                {
                    Code = "code",
                    Title = "title",
                    Version = 1
                },
                new AccountModuleDescInputVM()
                {
                    Code = "code2",
                    Title = "title2",
                    Version = 2
                }
         ]
        };

        context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
        context.Setup(x => x.AccountModuleDescs.AddRange(It.IsAny<Core.Entities.AccountModuleDesc[]>()));

        var _systemUnderTest = new AddRangeAccountModuleDescRequestHandler(context.Object);

        //Act
        var result = await _systemUnderTest.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal(400, result.Code);
    }
}