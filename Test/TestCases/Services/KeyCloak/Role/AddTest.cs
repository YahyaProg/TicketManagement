using Application.Services.Auth.Keycloak.RoleService;
using Core.GenericResultModel;
using FluentValidation.TestHelper;
using Gateway.KeyCloak.IServices;
using Gateway.Model.KeyCloak.Role;
using Moq;
using static Application.Services.Auth.Keycloak.RoleService.AddRoleRequest;
using static Application.Services.Auth.Keycloak.RoleService.GetRoleRequest;


namespace Test.TestCases.Services.KeyCloak.Role
{
    public class AddTest
    {
        private readonly Mock<IRoleService> _roleService = new Mock<IRoleService>();
        private static AddRoleRequest _correctRequestParams = new AddRoleRequest()
        {
            Name = "name",
        };

        private static AddRoleRequest _worngRequestParams = new AddRoleRequest()
        {
            Name = string.Empty,
        };

        [Fact]
        public async Task Get_Validation_Success()
        {
            //Arrange

            //Act
            var validates = new AddRoleRequestValidator();
            var resultValidate = await validates.TestValidateAsync(_correctRequestParams);

            //Assert
            resultValidate.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Get_Validation_Failed()
        {
            //Arrange

            //Act
            var validates = new AddRoleRequestValidator();
            var resultValidate = await validates.TestValidateAsync(_worngRequestParams);

            //Assert
            resultValidate.ShouldHaveAnyValidationError();
        }

        [Fact]
        public async Task AddTest_Success()
        {
            // Arrange
            var request = new AddRoleRequest();

            var systemUnderTest = new AddRoleRequestHandler(_roleService.Object);

            _roleService.Setup(x => x.AddAsync(It.IsAny<AddRoleParams>())).ReturnsAsync(new ApiResult<RoleVM>() { IsSuccess = true });

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task AddTest_Failed()
        {
            // Arrange
            var request = new AddRoleRequest();

            var systemUnderTest = new AddRoleRequestHandler(_roleService.Object);

            _roleService.Setup(x => x.AddAsync(It.IsAny<AddRoleParams>())).ReturnsAsync(new ApiResult<RoleVM>() { IsSuccess = false, Code = 500 });
            
            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
        }
    }
}
