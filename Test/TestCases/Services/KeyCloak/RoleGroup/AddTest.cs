using Application.Services.Auth.Keycloak.RoleGroupService;
using Core.GenericResultModel;
using Gateway.KeyCloak.IServices;
using Gateway.Model.KeyCloak.RoleGroup;
using Moq;
using static Application.Services.Auth.Keycloak.RoleGroupService.AddRoleGroupKeycloakRequest;

namespace Test.TestCases.Services.KeyCloak.RoleGroup
{
    public class AddTest
    {
        private readonly Mock<IRoleGroupService> _groupService = new();

        [Fact]
        public async Task AddTest_Success()
        {
            // Arrange
            var request = new AddRoleGroupKeycloakRequest();

            var systemUnderTest = new AddRoleGroupKeycloakRequestHandler(_groupService.Object);

            _groupService.Setup(x => x.AddAsync(It.IsAny<List<AddRoleGroupParams>>(),It.IsAny<Guid>())).ReturnsAsync(new ApiResult<bool>() { IsSuccess = true });

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task AddTest_Faild()
        {
            // Arrange
            var request = new AddRoleGroupKeycloakRequest();

            var systemUnderTest = new AddRoleGroupKeycloakRequestHandler(_groupService.Object);

            _groupService.Setup(x => x.AddAsync(It.IsAny<List<AddRoleGroupParams>>(), It.IsAny<Guid>())).ReturnsAsync(new ApiResult<bool>() { IsSuccess = false, Code = 500});
            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
        }
    }
}
