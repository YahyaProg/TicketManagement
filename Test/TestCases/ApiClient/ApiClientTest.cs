using Core.ViewModel;
using Gateway;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Protected;
using System.Net;
using Test.Helper;

namespace Test.TestCases.ApiClientTest
{
    public class ApiClientTest
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactory = new();
        private readonly HttpClient _httpClient = MoqHelper.getHttpClientMoq();

        [Fact]
        public async Task httpClient_getAsync_Test()
        {
            //Arrange
            var systemUnderTest = new ApiClient(_httpClient, _httpClientFactory.Object);

            //Act
            var result = await systemUnderTest.GetAsync("https://test.com");

            //Assert
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task httpClient_getAsyncWithType_Test()
        {
            //Arrange
            var systemUnderTest = new ApiClient(_httpClient, _httpClientFactory.Object);

            //Act
            var result = await systemUnderTest.GetAsync<BankVM>("https://test.com");

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task httpClient_deleteAsync_Test()
        {
            //Arrange
            var systemUnderTest = new ApiClient(_httpClient, _httpClientFactory.Object);

            //Act
            var result = await systemUnderTest.DeleteAsync("https://test.com");

            //Assert
            Assert.True(result.IsSuccessStatusCode);
        }


        [Fact]
        public async Task httpClient_deleteAsyncWithType_Test()
        {
            //Arrange
            var systemUnderTest = new ApiClient(_httpClient, _httpClientFactory.Object);

            //Act
            var result = await systemUnderTest.DeleteAsync<BankVM>("https://test.com");

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task httpClient_putAsync_Test()
        {
            //Arrange
            var systemUnderTest = new ApiClient(_httpClient, _httpClientFactory.Object);
            var bank = new BankVM()
            {
                Code = "23",
                Id = 1,
                Title = "salam",
                Version = 123
            };
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("test", "test2");

            //Act
            var result = await systemUnderTest.PutAsync<BankVM>("https://test.com", bank, headers);

            //Assert
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task httpClient_putAsyncWithType_Test()
        {
            //Arrange
            var systemUnderTest = new ApiClient(_httpClient, _httpClientFactory.Object);
            var bank = new BankVM()
            {
                Code = "23",
                Id = 1,
                Title = "salam",
                Version = 123
            };
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("test", "test2");

            //Act
            var result = await systemUnderTest.PutAsync<BankVM, BankVM>("https://test.com", bank, headers);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task httpClient_patchAsync_Test()
        {
            //Arrange
            var systemUnderTest = new ApiClient(_httpClient, _httpClientFactory.Object);
            var bank = new BankVM()
            {
                Code = "23",
                Id = 1,
                Title = "salam",
                Version = 123
            };
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("test", "test2");

            //Act
            var result = await systemUnderTest.PatchAsync<BankVM>("https://test.com", bank, headers);

            //Assert
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task httpClient_patchAsyncWithType_Test()
        {
            //Arrange
            var systemUnderTest = new ApiClient(_httpClient, _httpClientFactory.Object);
            var bank = new BankVM()
            {
                Code = "23",
                Id = 1,
                Title = "salam",
                Version = 123
            };
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("test", "test2");

            //Act
            var result = await systemUnderTest.PatchAsync<BankVM, BankVM>("https://test.com", bank, headers);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task httpClient_postAsync_Test()
        {
            //Arrange
            var systemUnderTest = new ApiClient(_httpClient, _httpClientFactory.Object);
            var bank = new BankVM()
            {
                Code = "23",
                Id = 1,
                Title = "salam",
                Version = 123
            };
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("test", "test2");

            //Act
            var result = await systemUnderTest.PostAsync<BankVM>("https://test.com", bank, headers);

            //Assert
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task httpClient_postAsyncWithType_Test()
        {
            //Arrange
            var systemUnderTest = new ApiClient(_httpClient, _httpClientFactory.Object);
            var bank = new BankVM()
            {
                Code = "23",
                Id = 1,
                Title = "salam",
                Version = 123
            };
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("test", "test2");

            //Act
            var result = await systemUnderTest.PostAsync<BankVM, BankVM>("https://test.com", bank, headers);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task httpClient_postAsyncWithFactoryName1_Test()
        {
            //Arrange
            var systemUnderTest = new ApiClient(_httpClient, _httpClientFactory.Object);
            var bank = new BankVM()
            {
                Code = "23",
                Id = 1,
                Title = "salam",
                Version = 123
            };
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("test", "test2");
            _httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            //Act
            var result = await systemUnderTest.PostAsync<BankVM>("https://test.com", bank, "amir", headers);

            //Assert
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task httpClient_postAsyncWithFactoryName_Test()
        {
            //Arrange
            var systemUnderTest = new ApiClient(_httpClient, _httpClientFactory.Object);
            var bank = new BankVM()
            {
                Code = "23",
                Id = 1,
                Title = "salam",
                Version = 123
            };
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("test", "test2");
            _httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            //Act
            var result = await systemUnderTest.PostAsync<BankVM, BankVM>("https://test.com", bank, "amir", headers);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task httpClient_PostFormUrlEncodedAsyncWithType_Test()
        {
            //Arrange
            var systemUnderTest = new ApiClient(_httpClient, _httpClientFactory.Object);
            var bank = new BankVM()
            {
                Code = "23",
                Id = 1,
                Title = "salam",
                Version = 123
            };

            Dictionary<string, string> parametere = new Dictionary<string, string>();
            parametere.Add("test", "test2");
            _httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            //Act
            var result = await systemUnderTest.PostFormUrlEncodedAsync<BankVM>("https://test.com", parametere);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task httpClient_PostFormUrlEncodedAsync_Test()
        {
            //Arrange
            var systemUnderTest = new ApiClient(_httpClient, _httpClientFactory.Object);
            var bank = new BankVM()
            {
                Code = "23",
                Id = 1,
                Title = "salam",
                Version = 123
            };

            Dictionary<string, string> parametere = new Dictionary<string, string>();
            parametere.Add("test", "test2");
            _httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            //Act
            var result = await systemUnderTest.PostFormUrlEncodedAsync("https://test.com", parametere);

            //Assert
            Assert.True(result.IsSuccessStatusCode);
        }
    }
}