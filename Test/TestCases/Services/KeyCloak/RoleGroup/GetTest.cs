using Core.GenericResultModel;
using Gateway.KeyCloak.IServices;
using Moq;
using static Application.Services.Auth.Keycloak.RoleGroupService.GetRoleGroupKeycloakRequest;
using Application.Services.Auth.Keycloak.RoleGroupService;
using Gateway.Model.KeyCloak.Role;
using Gateway.Model.KeyCloak.Dto;

namespace Test.TestCases.Services.KeyCloak.RoleGroup
{
    public class GetTest
    {
        private readonly Mock<IRoleGroupService> _roleGroupService = new();
        private readonly Mock<IGroupService> _groupService = new();

        [Fact]
        public async Task GetTest_Success()
        {
            // Arrange
            var request = new GetRoleGroupKeycloakRequest();

            var systemUnderTest = new GetRoleGroupKeycloakRequestHandler(_roleGroupService.Object, _groupService.Object);

            _groupService.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(new ApiResult<RoleKeycloakDto>() { IsSuccess = true, Data = new RoleKeycloakDto { name = "name", id = Guid.NewGuid() } } );
            _roleGroupService.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(new ApiResult<List<RoleVM>>() { IsSuccess = true });

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetTest_Faild()
        {
            // Arrange
            var request = new GetRoleGroupKeycloakRequest();

            var systemUnderTest = new GetRoleGroupKeycloakRequestHandler(_roleGroupService.Object, _groupService.Object);

            _groupService.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(new ApiResult<RoleKeycloakDto>() { IsSuccess = true });
            _roleGroupService.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(new ApiResult<List<RoleVM>>() { IsSuccess = false,Code = 500 });

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
        }
    }
}
