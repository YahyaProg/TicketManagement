using Application.Services.Auth.Keycloak.UserService;
using Core.Helpers;
using FluentValidation.TestHelper;
using Gateway.KeyCloak.IServices;
using Gateway.Model.KeyCloak.Dto;
using Moq;
using static Application.Services.Auth.Keycloak.UserService.ChangePasswordWithTokenKeyCloakRequest;
using static Application.Services.Auth.Keycloak.UserService.ChangePasswordWithTokenKeyCloakRequest.ChangePasswordWithTokenKeyCloakValidator;

namespace Test.TestCases.Services.AuthServiceTest
{
    public class ChangePasswordWithTokenKeyCloakRequestTest
    {
        private readonly Mock<IAuthService> _authServiceMock = new();
        private readonly Mock<IUserService> _userServiceMock = new();
        private readonly Mock<IUserHelper> _userHelperMock = new();
        private readonly string token = Environment.GetEnvironmentVariable("Test_Token") ?? "";
        private readonly string refreshToken = Environment.GetEnvironmentVariable("Test_Refresh_Token") ?? "";

        [Fact]
        public async Task ChangePasswordWithTokenKeyCloakRequestTest_Validation_Success()
        {
            //Arrange
            ChangePasswordWithTokenKeyCloakRequest loginWithKeyCloakRequest = new ChangePasswordWithTokenKeyCloakRequest()
            {
                OldPasswrod = "123456",
                Password = "123456789",
                ConfirmPassword = "123456789",
            };

            //Act
            var validates = new ChangePasswordWithTokenKeyCloakValidator();
            var resultValidate = await validates.TestValidateAsync(loginWithKeyCloakRequest);

            //Assert
            resultValidate.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task ChangePasswordWithTokenKeyCloakRequestTest_Validation_Failed()
        {
            //Arrange
            ChangePasswordWithTokenKeyCloakRequest loginWithKeyCloakRequest = new ChangePasswordWithTokenKeyCloakRequest()
            {
                OldPasswrod = "123456",
                Password = "123456789",
                ConfirmPassword = "1234567890",
            };

            //Act
            var validates = new ChangePasswordWithTokenKeyCloakValidator();
            var resultValidate = await validates.TestValidateAsync(loginWithKeyCloakRequest);

            //Assert
            resultValidate.ShouldHaveAnyValidationError();
        }

        [Fact]
        public async Task ChangePasswordWithTokenKeyCloakRequestTest_Success()
        {
            //Arrange
            ChangePasswordWithTokenKeyCloakRequest request = new()
            {
                OldPasswrod = "password",
                Password = "password123",
                ConfirmPassword = "password123"
            };

            _ = _userHelperMock.Setup(u => u.GetUserFromToken()).Returns(new Core.ViewModel.UserDto
            {
                Id = "cd3788b1-53b4-4c38-b93d-815a918c9508",
                UserName = "admin",
            });
            
            _ = _authServiceMock.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new LoginKeycloakDto
            {
                access_token = token,
                refresh_expires_in = 20240421,
                refresh_token = refreshToken
            });

            _ = _userServiceMock.Setup(u => u.ChangePasswordAsync(It.IsAny<Guid>(), It.IsAny<string>())).
                ReturnsAsync(new Core.GenericResultModel.ApiResult<bool> { Code = 0, Data = true, IsSuccess = true });

            var _systemUnderTest = new ChangePasswordWithTokenKeyCloakRequestHandler(_authServiceMock.Object, _userServiceMock.Object, _userHelperMock.Object);

            //Act
            var result = await _systemUnderTest.Handle(request, CancellationToken.None);


            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task ChangePasswordWithTokenKeyCloakRequestTest_Failed()
        {
            //Arrange
            ChangePasswordWithTokenKeyCloakRequest request = new()
            {
                OldPasswrod = "password",
                Password = "password123",
                ConfirmPassword = "password123"
            };

            _ = _userHelperMock.Setup(u => u.GetUserFromToken()).Returns(new Core.ViewModel.UserDto
            {
                Id = "cd3788b1-53b4-4c38-b93d-815a918c9508",
                UserName = "admin",
            });

            _ = _authServiceMock.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new LoginKeycloakDto
            {
                access_token = token,
                refresh_expires_in = 20240421,
                refresh_token = refreshToken
            });

            _ = _userServiceMock.Setup(u => u.ChangePasswordAsync(It.IsAny<Guid>(), It.IsAny<string>())).
                ReturnsAsync(new Core.GenericResultModel.ApiResult<bool> { Code = 500, Data = false, IsSuccess = false });

            var _systemUnderTest = new ChangePasswordWithTokenKeyCloakRequestHandler(_authServiceMock.Object, _userServiceMock.Object, _userHelperMock.Object);

            //Act
            var result = await _systemUnderTest.Handle(request, CancellationToken.None);


            Assert.False(result.IsSuccess);
        }
    }
}
