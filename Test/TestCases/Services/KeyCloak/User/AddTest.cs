using Core.GenericResultModel;
using Gateway.KeyCloak.IServices;
using Moq;
using FluentValidation.TestHelper;
using Application.Services.Auth.Keycloak.UserService;
using static Application.Services.Auth.Keycloak.UserService.AddUserKeyCloakRequest;
using Gateway.Model.KeyCloak.Params;
using Gateway.Model.KeyCloak.ViewModel;

namespace Test.TestCases.Services.KeyCloak.User
{
    public class AddTest
    {
        private readonly Mock<IUserService> _service = new();

        private static AddUserKeyCloakRequest _correctRequestParams = new AddUserKeyCloakRequest()
        {
            username = "admin",
            Password = "123456789",
            ConfirmPassword = "123456789"
        };

        private static AddUserKeyCloakRequest _worngRequestParams = new AddUserKeyCloakRequest()
        {
            username = "admin",
            Password = "123456789",
            ConfirmPassword = "987654321"
        };


        [Fact]
        public async Task AddTest_Validation_Success()
        {
            // Arrange

            //Act
            var validates = new AddUserKeyCloakValidator();
            var resultValidate = await validates.TestValidateAsync(_correctRequestParams);

            //Assert
            resultValidate.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task AddTest_Validation_Failed()
        {
            // Arrange

            //Act
            var validates = new AddUserKeyCloakValidator();
            var resultValidate = await validates.TestValidateAsync(_worngRequestParams);

            //Assert
            resultValidate.ShouldHaveAnyValidationError();
        }

        [Fact]
        public async Task GetTest_Success()
        {
            // Arrange
            var systemUnderTest = new AddUserKeyCloakRequestHandler(_service.Object);

            _service.Setup(x => x.AddAsync(It.IsAny<AddUserKeyCloakParams>())).ReturnsAsync(new ApiResult<UserKeyCloakVM>() { IsSuccess = true, Code = 200 });

            //Act
            var result = await systemUnderTest.Handle(_correctRequestParams, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task GetTest_Failed()
        {
            // Arrange
            var systemUnderTest = new AddUserKeyCloakRequestHandler(_service.Object);

            _service.Setup(x => x.AddAsync(It.IsAny<AddUserKeyCloakParams>())).ReturnsAsync(new ApiResult<UserKeyCloakVM>() { IsSuccess = false, Code = 500 });

            //Act
            var result = await systemUnderTest.Handle(_correctRequestParams, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.NotEqual(200, result.Code);
        }
    }
}
