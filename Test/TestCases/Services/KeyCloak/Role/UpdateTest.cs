using Application.Services.Auth.Keycloak.RoleService;
using Core.GenericResultModel;
using FluentValidation.TestHelper;
using Gateway.KeyCloak.IServices;
using Gateway.Model.KeyCloak.Role;
using Moq;
using static Application.Services.Auth.Keycloak.RoleService.UpdateRoleRequest;
using static Application.Services.Auth.Keycloak.UserService.GetUserKeyCloakRequest;

namespace Test.TestCases.Services.KeyCloak.Role
{
    public class UpdateTest
    {
        private readonly Mock<IRoleService> _roleService = new Mock<IRoleService>();

        private static UpdateRoleRequest _correctRequestParams = new UpdateRoleRequest()
        {
            Name = "name",
            Description = "description",
        };
        
        private static UpdateRoleRequest _worngRequestParams = new UpdateRoleRequest()
        {
            Name = string.Empty,
            Description = string.Empty,
        };

        [Fact]
        public async Task Update_Validation_Success()
        {
            //Arrange

            //Act
            var validates = new UpdateRoleRequestValidator();
            var resultValidate = await validates.TestValidateAsync(_correctRequestParams);

            //Assert
            resultValidate.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Update_Validation_Failed()
        {
            //Arrange

            //Act
            var validates = new UpdateRoleRequestValidator();
            var resultValidate = await validates.TestValidateAsync(_worngRequestParams);

            //Assert
            resultValidate.ShouldHaveAnyValidationError();
        }

        [Fact]
        public async Task UpdateTest_Success()
        {
            // Arrange
            var request = new UpdateRoleRequest();

            var systemUnderTest = new UpdateRoleRequestHandler(_roleService.Object);

            _roleService.Setup(x => x.UpdateAsync(It.IsAny<UpdateRoleParams>())).ReturnsAsync(new ApiResult<bool>() { IsSuccess = true });

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task UpdateTest_Failed()
        {
            // Arrange
            var request = new UpdateRoleRequest();

            var systemUnderTest = new UpdateRoleRequestHandler(_roleService.Object);

            _roleService.Setup(x => x.UpdateAsync(It.IsAny<UpdateRoleParams>())).ReturnsAsync(new ApiResult<bool>() { IsSuccess = false, Code = 500 });

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
        }
    }
}
