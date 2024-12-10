using Application.Services.Auth.Keycloak.RoleGroupService;
using Core.GenericResultModel;
using Gateway.KeyCloak.IServices;
using Gateway.Model.KeyCloak.RoleGroup;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Services.Auth.Keycloak.RoleGroupService.AddRoleGroupKeycloakRequest;
using static Application.Services.Auth.Keycloak.RoleGroupService.DeleteRoleGroupKeycloakRequest;

namespace Test.TestCases.Services.KeyCloak.RoleGroup
{
    public class DeleteTest
    {
        private readonly Mock<IRoleGroupService> _groupService = new();

        [Fact]
        public async Task DeleteTest_Success()
        {
            // Arrange
            var request = new DeleteRoleGroupKeycloakRequest();

            var systemUnderTest = new DeleteRoleGroupKeycloakRequestHandler(_groupService.Object);

            _groupService.Setup(x => x.DeleteAsync(It.IsAny<List<DeleteRoleGroupParams>>(), It.IsAny<Guid>())).ReturnsAsync(new ApiResult<bool>() { IsSuccess = true });

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DeleteTest_Faild()
        {
            // Arrange
            var request = new DeleteRoleGroupKeycloakRequest();

            var systemUnderTest = new DeleteRoleGroupKeycloakRequestHandler(_groupService.Object);

            _groupService.Setup(x => x.DeleteAsync(It.IsAny<List<DeleteRoleGroupParams>>(), It.IsAny<Guid>())).ReturnsAsync(new ApiResult<bool>() { IsSuccess = false, Code = 500 });
            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
        }
    }
}
