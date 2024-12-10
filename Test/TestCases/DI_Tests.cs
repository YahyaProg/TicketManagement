using Application;
using Core;
using Gateway;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Test.TestCases
{
    public class DI_Tests
    {


        [Fact]
        public void DependencyInjectionsTest()
        {
            var services = new ServiceCollection();

            services.AddInfrastructure();
            services.AddApplication(new()
            {
                Rabbit = new()
                {
                    Host = "",
                    Password = "",
                    Port = "123",
                    Username = ""
                }
            });
            services.AddGateway();
            services.AddCore();
            Assert.NotNull(services);
        }
    }
}
