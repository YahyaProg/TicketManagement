using Core.GenericResultModel;
using Gateway.KeyCloak.IServices;
using Moq;
using static Application.Services.Auth.Keycloak.UserService.GetUserKeyCloakRequest;
using Application.Services.Auth.Keycloak.UserService;
using FluentValidation.TestHelper;
using Gateway.Model.KeyCloak.ViewModel;

namespace Test.TestCases.Services.KeyCloak.User
{
    public class GetTest
    {
        private readonly Mock<IUserService> _service = new();

        private static GetUserKeyCloakRequest _correctRequestParams = new GetUserKeyCloakRequest
        {
            Id = Guid.NewGuid()
        };

        private static GetUserKeyCloakRequest _worngRequestParams = new GetUserKeyCloakRequest
        {
            Id = Guid.Empty
        };

        [Fact]
        public async Task GetTest_Validation_Success()
        {
            //Arrange

            //Act
            var validates = new GetUserKeyCloakRequestValidation();
            var resultValidate = await validates.TestValidateAsync(_correctRequestParams);

            //Assert
            resultValidate.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task GetTest_Validation_Failed()
        {
            //Arrange

            //Act
            var validates = new GetUserKeyCloakRequestValidation();
            var resultValidate = await validates.TestValidateAsync(_worngRequestParams);

            //Assert
            resultValidate.ShouldHaveAnyValidationError();
        }

        [Fact]
        public async Task GetTest_Success()
        {
            // Arrange
            var request = new GetUserKeyCloakRequest() { Id = Guid.NewGuid() };

            var systemUnderTest = new GetUserKeyCloakRequestHandler(_service.Object);

            _service.Setup(x => x.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new ApiResult<UserKeyCloakVM>() { IsSuccess = true, Code = 200 });

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task GetTest_Failed()
        {
            // Arrange
            var request = new GetUserKeyCloakRequest() { Id = Guid.NewGuid() };

            var systemUnderTest = new GetUserKeyCloakRequestHandler(_service.Object);

            _service.Setup(x => x.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new ApiResult<UserKeyCloakVM>() { IsSuccess = false, Code = 500 });

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.NotEqual(200, result.Code);
        }
    }
}
