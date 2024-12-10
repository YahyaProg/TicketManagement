using Application.Services.Auth.Keycloak.GroupService;
using Core.GenericResultModel;
using Gateway.KeyCloak.IServices;
using Gateway.Model.KeyCloak.Dto;
using Moq;
using static Application.Services.Auth.Keycloak.GroupService.GetAllGroupsKeycloakRequest;

namespace Test.TestCases.Services.KeyCloak.Group
{
    public class GetAllTest
    {
        private readonly Mock<IGroupService> _service = new();

        [Fact]
        public async Task GetAllTest_Success()
        {
            // Arrange
            var request = new GetAllGroupsKeycloakRequest();

            var systemUnderTest = new GetAllGroupsKeycloakRequestHandler(_service.Object);

            _service.Setup(x => x.GetAllAsync()).ReturnsAsync(new ApiResult<List<RoleKeycloakDto>>() { Code = 200});

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task GetAllTest_Faild()
        {
            // Arrange
            var request = new GetAllGroupsKeycloakRequest();

            var systemUnderTest = new GetAllGroupsKeycloakRequestHandler(_service.Object);

            _service.Setup(x => x.GetAllAsync()).ReturnsAsync(new ApiResult<List<RoleKeycloakDto>>() { IsSuccess = false, Code = 500});

            //Act
            var result = await systemUnderTest.Handle(request, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.NotEqual(200, result.Code);
        }

    }
}
