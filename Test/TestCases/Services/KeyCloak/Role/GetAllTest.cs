using Application.Services.Auth.Keycloak.RoleService;
using Core.GenericResultModel;
using Gateway.KeyCloak.IServices;
using Gateway.KeyCloak.Services;
using Gateway.Model.KeyCloak.Role;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Services.Auth.Keycloak.RoleGroupService.DeleteRoleGroupKeycloakRequest;
using static Application.Services.Auth.Keycloak.RoleService.GetAllRoleRequest;

namespace Test.TestCases.Services.KeyCloak.Role
{
    public class GetAllTest
    {
        private readonly Mock<IRoleService> _roleService = new Mock<IRoleService>();

        [Fact]
        public async Task GetAllTest_Success()
        {
            // Arrange
            var request = new GetAllRoleRequest();

            var systemUnderTest = new GetAllRoleRequestHandler(_roleService.Object);

            _roleService.Setup(x => x.GetAllAsync(It.IsAny<string>())).ReturnsAsync(new ApiResult<List<RoleVM>>() { IsSuccess = true });

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetAllTest_Failed()
        {
            // Arrange
            var request = new GetAllRoleRequest();

            var systemUnderTest = new GetAllRoleRequestHandler(_roleService.Object);

            _roleService.Setup(x => x.GetAllAsync(It.IsAny<string>())).ReturnsAsync(new ApiResult<List<RoleVM>>() { IsSuccess = false, Code=500 });

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
        }
    }
}
