using Gateway.KeyCloak.Services;
using Gateway;
using Gateway.Model.KeyCloak.Role;
using Infrastructure.Repositories.Setting;
using Moq;
using System.Net;
using Newtonsoft.Json;
using Test.Helper;
using Gateway.Model.KeyCloak.RoleGroup;

namespace Test.TestCases.Gateways.RoleGroupServiceTest
{
    public class RoleGroupServiceTest
    {
        private readonly Mock<IApiClient> _apiClient = new();
        private readonly Mock<IDbSettings> _dbSetting = new();

        [Fact]
        public async void GetAsync_test()
        {

            // Arrange
            var system = new RoleGroupService(_apiClient.Object, MoqHelper.GetHttpContextAccessor(), _dbSetting.Object);

            var expectedRoles = new List<RoleVM> { new RoleVM { Id = new Guid(), Name = "Role 1" } };
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.GetAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                .ReturnsAsync(response);

            // Act
            var result = await system.GetAsync(Guid.NewGuid());

            // Assert
            Assert.True(result.IsSuccess);

        }


        [Fact]
        public async void AddAsync_test()
        {

            // Arrange
            var system = new RoleGroupService(_apiClient.Object, MoqHelper.GetHttpContextAccessor(), _dbSetting.Object);
            var request = new List<AddRoleGroupParams>()
            {
                new AddRoleGroupParams(){Id = Guid.NewGuid(), Name = "amir"}
            };

            var expectedRoles = new List<RoleVM> { new RoleVM { Id = new Guid(), Name = "Role 1" } };
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.PostAsync(It.IsAny<string>(), It.IsAny<List<AddRoleGroupParams>>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                .ReturnsAsync(response);

            // Act
            var result = await system.AddAsync(request, Guid.NewGuid());

            // Assert
            Assert.True(result.IsSuccess);
        }


        [Fact]
        public async void AddAsync_failed_test()
        {

            // Arrange
            var system = new RoleGroupService(_apiClient.Object, MoqHelper.GetHttpContextAccessor(), _dbSetting.Object);
            var request = new List<AddRoleGroupParams>()
            {
                new AddRoleGroupParams(){Id = Guid.NewGuid(), Name = "amir"}
            };

            var expectedRoles = new List<RoleVM> { new RoleVM { Id = new Guid(), Name = "Role 1" } };
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.BadGateway, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.PostAsync(It.IsAny<string>(), It.IsAny<List<AddRoleGroupParams>>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                .ReturnsAsync(response);

            // Act
            var result = await system.AddAsync(request, Guid.NewGuid());

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
