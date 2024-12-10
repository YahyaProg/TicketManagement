using Application.Services.Auth.Keycloak.UserService;
using Core.GenericResultModel;
using FluentValidation.TestHelper;
using Gateway.KeyCloak.IServices;
using Moq;
using static Application.Services.Auth.Keycloak.UserService.ChangeStatusUserKeyCloakRequest;

namespace Test.TestCases.Services.KeyCloak.User
{
    public class ChangeStatusTest
    {
        private readonly Mock<IUserService> _service = new();

        private static ChangeStatusUserKeyCloakRequest _correctRequestParams = new ChangeStatusUserKeyCloakRequest
        {
            UserId = Guid.NewGuid(),
            Status = true
        };

        private static ChangeStatusUserKeyCloakRequest _worngRequestParams = new ChangeStatusUserKeyCloakRequest
        {
            UserId = Guid.Empty,
            Status = null
        };

        [Fact]
        public async Task ChangeStatusTest_Validation_Success()
        {
            //Arrange

            //Act
            var validates = new ChangeStatusUserKeyCloakValidator();
            var resultValidate = await validates.TestValidateAsync(_correctRequestParams);

            //Assert
            resultValidate.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task ChangeStatusTest_Validation_Failed()
        {
            //Arrange

            //Act
            var validates = new ChangeStatusUserKeyCloakValidator();
            var resultValidate = await validates.TestValidateAsync(_worngRequestParams);

            //Assert
            resultValidate.ShouldHaveAnyValidationError();
        }

        [Fact]
        public async Task ChangeStatusTest_Success()
        {
            // Arrange
            var systemUnderTest = new ChangeStatusUserKeyCloakHandler(_service.Object);

            _service.Setup(x => x.ChangeStatusAsync(It.IsAny<Guid>(),It.IsAny<bool>())).ReturnsAsync(new ApiResult<bool>() { IsSuccess = true, Code = 200 });

            //Act
            var result = await systemUnderTest.Handle(_correctRequestParams, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task ChangeStatusTest_Failed()
        {
            // Arrange
            var systemUnderTest = new ChangeStatusUserKeyCloakHandler(_service.Object);

            _service.Setup(x => x.ChangeStatusAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(new ApiResult<bool>() { IsSuccess = false, Code = 500 });

            //Act
            var result = await systemUnderTest.Handle(_correctRequestParams, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.NotEqual(200, result.Code);
        }
    }
}
