using Core.GenericResultModel;
using Core.ViewModel.Branch;
using Gateway;
using Gateway.KeyCloak.IServices;
using Gateway.KeyCloak.Services;
using Gateway.Model.KeyCloak.Dto;
using Gateway.Model.KeyCloak.Params;
using Gateway.Model.KeyCloak.ViewModel;
using Infrastructure;
using Infrastructure.Repositories.Setting;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;

namespace Test.TestCases.Gateways.UserServiceTest
{
    public class UserServiceTest
    {
        private readonly Mock<IApiClient> _apiClient;                                                 
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Mock<IDbSettings> _dbSetting;
        private readonly UserService _userService;
        private readonly Mock<IAuthService> _authService;

        public UserServiceTest()
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
            _authService = new Mock<IAuthService>();

            _userService = new UserService(_apiClient.Object, _httpContextAccessor, _dbSetting.Object, _authService.Object);

        }
        [Fact]
        public async Task AddAsync_testAsync()
        {
            var add = new AddUserKeyCloakParams()
            {
                firstName =  "test",
                lastName = "test",
                enabled = true,
                username = "test"
            };
            var expectedRoles = new UserKeyCloakVM { id = new Guid(), firstName = "name 1" , lastName = "last 1", enabled = true };
            var expectedGetRoles = new List<UserKeyCloakVM> { new UserKeyCloakVM() { id = new Guid(), firstName = "name 1", lastName = "last 1", enabled = true } };

            var responseAdd = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };
            var responseGet = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedGetRoles)) };

            _apiClient.Setup(a => a.PostAsync(It.IsAny<string>(), It.IsAny <AddUserKeyCloakParams>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
               .ReturnsAsync(responseAdd);
            _apiClient.Setup(a => a.GetAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                          .ReturnsAsync(responseGet);

            // Act
            var result = await _userService.AddAsync(add);

            // Assert
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task AddAsync_testConflictAsync()
        {
            var add = new AddUserKeyCloakParams()
            {
                firstName = "test",
                lastName = "test",
                enabled = true,
                username = "test"
            };
            var expectedRoles = new UserKeyCloakVM { id = new Guid(), firstName = "name 1", lastName = "last 1", enabled = true };
            var expectedGetRoles = new List<UserKeyCloakVM> { new UserKeyCloakVM() { id = new Guid(), firstName = "name 1", lastName = "last 1", enabled = true } };

            var responseAdd = new HttpResponseMessage { StatusCode = HttpStatusCode.Conflict, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.PostAsync(It.IsAny<string>(), It.IsAny<AddUserKeyCloakParams>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
               .ReturnsAsync(responseAdd);

            // Act
            var result = await _userService.AddAsync(add);

            // Assert
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public async Task AddAsync_testNotFoundAsync()
        {
            var add = new AddUserKeyCloakParams()
            {
                firstName = "test",
                lastName = "test",
                enabled = true,
                username = "test"
            };
            var expectedRoles = new UserKeyCloakVM { id = new Guid(), firstName = "name 1", lastName = "last 1", enabled = true };
            var expectedGetRoles = new List<UserKeyCloakVM> { new UserKeyCloakVM() { id = new Guid(), firstName = "name 1", lastName = "last 1", enabled = true } };

            var responseAdd = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent(JsonConvert.SerializeObject(expectedRoles)) };

            _apiClient.Setup(a => a.PostAsync(It.IsAny<string>(), It.IsAny<AddUserKeyCloakParams>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
               .ReturnsAsync(responseAdd);

            // Act
            var result = await _userService.AddAsync(add);

            // Assert
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public async Task FindByUserNameAsync_TestAsync()
        {
            var username = "test";
            var expectedGetRoles = new List<UserKeyCloakVM> { new UserKeyCloakVM() { id = new Guid(), firstName = "name 1", lastName = "last 1", enabled = true } };

            var responseGet = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedGetRoles)) };


            _apiClient.Setup(a => a.GetAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                          .ReturnsAsync(responseGet);

            // Act
            var result = await _userService.FindByUserNameAsync(username);

            // Assert
            Assert.True(result.IsSuccess);

        }
        [Fact]
        public async Task FindByUserNameAsync_TestBadRequestAsync()
        {
            var username = "test";
            var expectedGetRoles = new List<UserKeyCloakVM> { new UserKeyCloakVM() { id = new Guid(), firstName = "name 1", lastName = "last 1", enabled = true } };

            var responseGet = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent(JsonConvert.SerializeObject(expectedGetRoles)) };


            _apiClient.Setup(a => a.GetAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                          .ReturnsAsync(responseGet);

            // Act
            var result = await _userService.FindByUserNameAsync(username);

            // Assert
            Assert.False(result.IsSuccess);

        }
        [Fact]
        public async Task FindByIdAsync_TestAsync()
        {
            var id = new Guid();
            var expectedGetRoles = new UserKeyCloakVM {  id = new Guid(), firstName = "name 1", lastName = "last 1", enabled = true } ;

            var responseGet = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedGetRoles)) };


            _apiClient.Setup(a => a.GetAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                          .ReturnsAsync(responseGet);

            // Act
            var result = await _userService.FindByIdAsync(id);

            // Assert
            Assert.True(result.IsSuccess);

        }
        [Fact]
        public async Task FindByIdAsync_TestBadRequestAsync()
        {
            var id = new Guid();
            var expectedGetRoles = new UserKeyCloakVM { id = new Guid(), firstName = "name 1", lastName = "last 1", enabled = true };

            var responseGet = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent(JsonConvert.SerializeObject(expectedGetRoles)) };


            _apiClient.Setup(a => a.GetAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                          .ReturnsAsync(responseGet);

            // Act
            var result = await _userService.FindByIdAsync(id);

            // Assert
            Assert.False(result.IsSuccess);

        }
        [Fact]
        public async Task ChangePasswordAsync_TestAsync()
        {
            var userId = new Guid();
            var password = "test";
            var expectedGetRoles =  true ;
            var token = new LoginKeycloakDto
            {
                access_token = "asdiajodadadasdds",
                refresh_expires_in = 12313,
                refresh_token = "assasdsadas"
            };

            var responseUpdate = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedGetRoles)) };

            _authService.Setup(a => a.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(token);

            _apiClient.Setup(a => a.PutAsync(It.IsAny<string>(), It.IsAny<object>() ,It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                          .ReturnsAsync(responseUpdate);

            // Act
            var result = await _userService.ChangePasswordAsync(userId, password);

            // Assert
            Assert.True(result.IsSuccess);

        }
        [Fact]
        public async Task ChangePasswordAsync_TestBadRequestAsync()
        {
            var userId = new Guid();
            var password = "test";
            var expectedGetRoles = true;
            var token = new LoginKeycloakDto
            {
                access_token = "asdiajodadadasdds",
                refresh_expires_in = 12313,
                refresh_token = "assasdsadas"
            };

            var responseUpdate = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent(JsonConvert.SerializeObject(expectedGetRoles)) };

            _authService.Setup(a => a.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(token);

            _apiClient.Setup(a => a.PutAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                          .ReturnsAsync(responseUpdate);

            // Act
            var result = await _userService.ChangePasswordAsync(userId, password);

            // Assert
            Assert.False(result.IsSuccess);

        }
        [Fact]
        public async Task ChangeStatusAsync_TestAsync()
        {
            var id = new Guid();
            var status = true;
            var expectedGetRoles = true;

            var responseUpdate = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedGetRoles)) };


            _apiClient.Setup(a => a.PutAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                          .ReturnsAsync(responseUpdate);

            // Act
            var result = await _userService.ChangeStatusAsync(id, status);

            // Assert
            Assert.True(result.IsSuccess);

        }
        [Fact]
        public async Task ChangeStatusAsync_TestBadRequestAsync()
        {
            var id = new Guid();
            var status = true;
            var expectedGetRoles = true;

            var responseUpdate = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent(JsonConvert.SerializeObject(expectedGetRoles)) };


            _apiClient.Setup(a => a.PutAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                          .ReturnsAsync(responseUpdate);

            // Act
            var result = await _userService.ChangeStatusAsync(id, status);

            // Assert
            Assert.False(result.IsSuccess);

        }
        [Fact]
        public async Task UpdateAsync_TestAsync()
        {
            var input = new UpdateUserKeyCloakParams()
            {
                enabled = true,
                firstName = "test",
                lastName = "test",
                id = new Guid()
            };
            var expectedGetRoles = true;

            var responseUpdate = new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(expectedGetRoles)) };


            _apiClient.Setup(a => a.PutAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                          .ReturnsAsync(responseUpdate);

            // Act
            var result = await _userService.UpdateAsync(input);

            // Assert
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task UpdateAsync_TestBadRequestAsync()
        {
            var input = new UpdateUserKeyCloakParams()
            {
                enabled = true,
                firstName = "test",
                lastName = "test",
                id = new Guid()
            };
            var expectedGetRoles = true;

            var responseUpdate = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent(JsonConvert.SerializeObject(expectedGetRoles)) };


            _apiClient.Setup(a => a.PutAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<int>()))
                          .ReturnsAsync(responseUpdate);

            // Act
            var result = await _userService.UpdateAsync(input);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
