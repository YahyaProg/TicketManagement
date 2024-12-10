using Application.Services.Auth.Keycloak.UserService;
using Core.GenericResultModel;
using FluentValidation.TestHelper;
using Gateway.KeyCloak.IServices;
using Moq;
using static Application.Services.Auth.Keycloak.UserService.ChangePasswordKeyCloakRequest;

namespace Test.TestCases.Services.KeyCloak.User
{
    public class ChangePasswordTest
    {
        private readonly Mock<IUserService> _service = new();

        private static ChangePasswordKeyCloakRequest _correctRequestParams = new ChangePasswordKeyCloakRequest
        {
            Id = Guid.NewGuid(),
            Password = "123456789",
            ConfirmPassword = "123456789",
        };

        private static ChangePasswordKeyCloakRequest _worngRequestParams = new ChangePasswordKeyCloakRequest
        {
            Id = Guid.NewGuid(),
            Password = "123456789",
            ConfirmPassword = "987654321",
        };

        [Fact]
        public async Task ChangePasswordTest_Validation_Success()
        {
            //Arrange

            //Act
            var validates = new ChangePasswordKeyCloakValidator();
            var resultValidate = await validates.TestValidateAsync(_correctRequestParams);

            //Assert
            resultValidate.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task ChangePasswordTest_Validation_Failed()
        {
            //Arrange

            //Act
            var validates = new ChangePasswordKeyCloakValidator();
            var resultValidate = await validates.TestValidateAsync(_worngRequestParams);

            //Assert
            resultValidate.ShouldHaveAnyValidationError();
        }

        [Fact]
        public async Task ChangePasswordTest_Success()
        {
            // Arrange
            var systemUnderTest = new ChangePasswordKeyCloakRequestHandler(_service.Object);

            _service.Setup(x => x.ChangePasswordAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(new ApiResult<bool>() { IsSuccess = true, Code = 200 });

            //Act
            var result = await systemUnderTest.Handle(_correctRequestParams, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task ChangePasswordTest_Failed()
        {
            // Arrange
            var systemUnderTest = new ChangePasswordKeyCloakRequestHandler(_service.Object);

            _service.Setup(x => x.ChangePasswordAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(new ApiResult<bool>() { IsSuccess = false, Code = 500 });

            //Act
            var result = await systemUnderTest.Handle(_correctRequestParams, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.NotEqual(200, result.Code);
        }
    }
}
