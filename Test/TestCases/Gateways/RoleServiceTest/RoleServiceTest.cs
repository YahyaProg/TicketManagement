using Core.Entities;
using Core.GenericResultModel;
using Gateway;
using Gateway.KeyCloak.Services;
using Gateway.Model.KeyCloak.Role;
using Infrastructure.Repositories.Setting;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;

namespace Test.TestCases.Gateways.RoleServiceTest
{
    public class RoleServiceTest
    {

        private readonly Mock<IApiClient> _apiClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleService _roleService;
        private readonly Mock<IDbSettings> _dbSetting;

        public RoleServiceTest()
        {
            var claims = new List<Claim>
            {
                new ("scope","test"),
                new ("name","test" ),
                new ("roles","test" ),
                new ("roles","test" ),
                new ("IsAuthenticated","true" ),
            };
            _apiClient = new Mock<IApiClient>();
            _httpContextAccessor = Helper.MoqHelper.GetHttpContextAccessor(claims: claims);
            _dbSetting = new Mock<IDbSettings>();
            _roleService = new RoleService(_apiClient.Object, _httpContextAccessor, _dbSetting.Object);
        }

        [Fact]
        public async void GetAllAsync_test()
        {

            // Arrange
            var search = "search";
            var expectedRoles = new List<RoleVM> { new RoleVM { Id = new Guid(), Name = "Role 1" } };
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.GetAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                .ReturnsAsync(response);

            // Act
            var result = await _roleService.GetAllAsync(search);

            // Assert
            Assert.True(result.IsSuccess);

        }

        [Fact]
        public async Task GetAllAsync_testFailAsync()
        {
            // Arrange
            var search = "search";
            var expectedRoles = new List<RoleVM> { new RoleVM { Id = new Guid(), Name = "Role 1" } };
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.GetAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                .ReturnsAsync(response);

            // Act
            var result = await _roleService.GetAllAsync(search);

            // Assert
            Assert.False(result.IsSuccess);

        }
        [Fact]
        public async Task GetByNameAsync_testAsync()
        {
            // Arrange
            var search = "search";
            var expectedRoles = new RoleVM {  Id = new Guid(), Name = "Role 1"  };
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.GetAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                .ReturnsAsync(response);

            // Act
            var result = await _roleService.GetByNameAsync(search);

            // Assert
            Assert.True(result.IsSuccess);

        }
        [Fact]
        public async Task GetByNameAsync_testFailAsync()
        {
            // Arrange
            var search = "search";
            var expectedRoles = new List<RoleVM> { new RoleVM { Id = new Guid(), Name = "Role 1" } };
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.GetAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                .ReturnsAsync(response);

            // Act
            var result = await _roleService.GetByNameAsync(search);

            // Assert
            Assert.False(result.IsSuccess);

        }
        [Fact]
        public async Task AddAsync_testAsync()
        {
            // Arrange
            var add = new AddRoleParams() {
                Description = "description",
                Name = "name",
            };

            var expectedRoles = new RoleVM { Id = new Guid(), Name = "Role 1" };
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.PostAsync(It.IsAny<string>(),It.IsAny<AddRoleParams>() ,It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                .ReturnsAsync(response);
            _apiClient.Setup(a => a.GetAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                .ReturnsAsync(response);

            // Act
            var result = await _roleService.AddAsync(add);

            // Assert
            Assert.True(result.IsSuccess);

        }
        [Fact]
        public async Task AddAsync_testFailAsync()
        {
            // Arrange
            var add = new AddRoleParams()
            {
                Description = "description",
                Name = "name",
            };

            var expectedRoles = new RoleVM { Id = new Guid(), Name = "Role 1" };
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.PostAsync(It.IsAny<string>(), It.IsAny<AddRoleParams>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                .ReturnsAsync(response);

            // Act
            var result = await _roleService.AddAsync(add);

            // Assert
            Assert.False(result.IsSuccess);

        }
        [Fact]
        public async Task AddAsync_testConflictAsync()
        {
            // Arrange
            var add = new AddRoleParams()
            {
                Description = "description",
                Name = "name",
            };

            var expectedRoles = new RoleVM { Id = new Guid(), Name = "Role 1" };
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.Conflict, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.PostAsync(It.IsAny<string>(), It.IsAny<AddRoleParams>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                .ReturnsAsync(response);

            // Act
            var result = await _roleService.AddAsync(add);

            // Assert
            Assert.False(result.IsSuccess);

        }
        [Fact]
        public async Task DeleteAsync_testAsync()
        {
            // Arrange
            var search = "search";
            var expectedRoles = new RoleVM { Id = new Guid(), Name = "Role 1" };
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.DeleteAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                .ReturnsAsync(response);

            // Act
            var result = await _roleService.DeleteAsync(search);

            // Assert
            Assert.True(result.IsSuccess);

        }
        [Fact]
        public async Task DeleteAsync_testFailAsync()
        {
            // Arrange
            var search = "search";


            var expectedRoles = new RoleVM { Id = new Guid(), Name = "Role 1" };
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.DeleteAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                .ReturnsAsync(response);

            // Act
            var result = await _roleService.DeleteAsync(search);

            // Assert
            Assert.False(result.IsSuccess);

        }

        [Fact]
        public async Task UpdateAsync_testAsync()
        {
            // Arrange
            var update = new UpdateRoleParams()
            {
                Description = "description",
                Name = "name",
            };

            var expectedRoles = new RoleVM { Id = new Guid(), Name = "Role 1" };
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.PostAsync(It.IsAny<string>(), It.IsAny<UpdateRoleParams>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                .ReturnsAsync(response);

            // Act
            var result = await _roleService.UpdateAsync(update);

            // Assert
            Assert.True(result.IsSuccess);

        }
        [Fact]
        public async Task UpdateAsync_testFailAsync()
        {
            // Arrange
            var update = new UpdateRoleParams()
            {
                Description = "description",
                Name = "name",
            };

            var expectedRoles = new RoleVM { Id = new Guid(), Name = "Role 1" };
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.PostAsync(It.IsAny<string>(), It.IsAny<UpdateRoleParams>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                .ReturnsAsync(response);

            // Act
            var result = await _roleService.UpdateAsync(update);

            // Assert
            Assert.False(result.IsSuccess);

        }
        [Fact]
        public async Task UpdateAsync_testConflictAsync()
        {
            // Arrange
            var update = new UpdateRoleParams()
            {
                Description = "description",
                Name = "name",
            };

            var expectedRoles = new RoleVM { Id = new Guid(), Name = "Role 1" };
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.Conflict, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.PostAsync(It.IsAny<string>(), It.IsAny<UpdateRoleParams>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                .ReturnsAsync(response);

            // Act
            var result = await _roleService.UpdateAsync(update);

            // Assert
            Assert.False(result.IsSuccess);

        }


    }
}
