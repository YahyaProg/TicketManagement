using Application.Services.Auth.Keycloak.RoleService;
using Core.GenericResultModel;
using FluentValidation.TestHelper;
using Gateway.KeyCloak.IServices;
using Moq;
using static Application.Services.Auth.Keycloak.RoleService.DeleteRoleRequest;
using static Application.Services.Auth.Keycloak.RoleService.GetRoleRequest;


namespace Test.TestCases.Services.KeyCloak.Role
{
    public class DeleteTest
    {
        private readonly Mock<IRoleService> _roleService = new();
        private static DeleteRoleRequest _correctRequestParams = new DeleteRoleRequest()
        {
            Name = "name",
        };

        private static DeleteRoleRequest _worngRequestParams = new DeleteRoleRequest()
        {
            Name = string.Empty,
        };

        [Fact]
        public async Task Delete_Validation_Success()
        {
            //Arrange

            //Act
            var validates = new DeleteRoleRequestValidator();
            var resultValidate = await validates.TestValidateAsync(_correctRequestParams);

            //Assert
            resultValidate.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Delete_Validation_Failed()
        {
            //Arrange

            //Act
            var validates = new DeleteRoleRequestValidator();
            var resultValidate = await validates.TestValidateAsync(_worngRequestParams);

            //Assert
            resultValidate.ShouldHaveAnyValidationError();
        }

        [Fact]
        public async Task DeleteTest_Success()
        {
            // Arrange
            var request = new DeleteRoleRequest();

            var systemUnderTest = new DeleteRoleRequestHandler(_roleService.Object);

            _roleService.Setup(x => x.DeleteAsync(It.IsAny<string>())).ReturnsAsync(new ApiResult<bool>() { IsSuccess = true });

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DeleteTest_Failed()
        {
            // Arrange
            var request = new DeleteRoleRequest();

            var systemUnderTest = new DeleteRoleRequestHandler(_roleService.Object);

            _roleService.Setup(x => x.DeleteAsync(It.IsAny<string>())).ReturnsAsync(new ApiResult<bool>() { IsSuccess = false, Code= 500 });

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
        }
    }
}
