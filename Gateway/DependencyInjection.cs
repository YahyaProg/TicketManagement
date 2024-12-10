using Core.Logger;
using Gateway.Captcha.HttpRequestHelper;
using Gateway.KeyCloak.IServices;
using Gateway.KeyCloak.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Gateway
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGateway(this IServiceCollection services)
        {
            //captcha services
            services.AddTransient<ICaptchaHttpRequestHelper, CaptchaHttpRequestHelper>();

            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRoleGroupService, RoleGroupService>();
            //if you want a custom httpclient and HttpClientHandler
            //Create something like the code below and give it as an input, for example post  => PostAsync(httpClientName:my_httpClient_sample_name)
            services.AddHttpClient<IApiClient, ApiClient>().ConfigurePrimaryHttpMessageHandler(() =>
                new LoggingHandler(
                    new HttpClientHandler(),
                    services.BuildServiceProvider().GetService<ILoggerManager>(),
                    services.BuildServiceProvider().GetService<IHttpContextAccessor>()
                )
            );

            return services;
        }
    }
}
