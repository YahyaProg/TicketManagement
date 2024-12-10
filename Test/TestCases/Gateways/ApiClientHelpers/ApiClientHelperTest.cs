using Gateway.KeyCloak.Utils;
using Gateway.Model.KeyCloak.ViewModel;
using Infrastructure;
using Infrastructure.Repositories.Setting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;

namespace Test.TestCases.Gateways.ApiClientHelpers
{
    public class ApiClientHelperTest
    {

        private readonly Mock<IHttpContextAccessor> _httpContextAccessor = new();
        private readonly Mock<DBContext> _dBContext = new();
        private readonly Mock<IDbSettings> _dbSetting = new();

        [Fact]
        public async void ConvertJsonToData_Test()
        {
            var Data = new UserKeyCloakVM { id = Guid.NewGuid() };
            var successfulResult = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(Data), System.Text.Encoding.UTF8, "application/json")
            };

            var result = await ApiClientHelper.ConvertJsonToData<UserKeyCloakVM>(successfulResult);
            Assert.NotNull(result);
        }
        [Fact]
        public void FaildReturn_Test()
        {
            var result = ApiClientHelper.FaildReturn<UserKeyCloakVM>(HttpStatusCode.BadRequest, "message", "message");
            Assert.NotNull(result);

        }
        [Fact]
        public void SuccessReturn_Test()
        {
            var Data = new UserKeyCloakVM { id = Guid.NewGuid() };
            var result = ApiClientHelper.SuccessReturn<UserKeyCloakVM>(Data);
            Assert.NotNull(result);

        }

        [Fact]
        public void ServiceAddress_Test()
        {
            var helper = new ApiClientHelper(_httpContextAccessor.Object, _dbSetting.Object);
            var result = helper.ServiceAddress("Address");
            Assert.NotNull(result);

        }
    }
}
