using Application.Services.AuthService.CaptchaService;
using Core.Entities;
using Infrastructure;
using Infrastructure.Repositories.Setting;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Services.AuthService.CaptchaService.GenerateCaptchaRequest;

namespace Test.TestCases.Services.GenerateCaptcha
{
    public class GenerateCaptcha
    {
        [Fact]
        public async Task Handle_GeneratesCaptchaCodeAndImage_ReturnsApiResultWithCaptchaVm()
        {
            // Arrange
            var config = new Mock<IDbSettings>();
            config.Setup(c => c.GetSetting("captcha_allowed_letters")).Returns("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
            config.Setup(c => c.GetSetting("captcha_code_length")).Returns("6");
            config.Setup(c => c.GetSetting("captcha_default_retries_left")).Returns("5");
            config.Setup(c => c.GetSetting("captcha_expire_time")).Returns("10");

            var context = new Mock<DBContext>();

            context.Setup(x => x.Captchas.Add(It.IsAny<Captcha>()));
            context.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()));

            var request = new GenerateCaptchaRequest();
            var handler = new GenerateCaptchaRequestHandler(config.Object, context.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result is { Code : 200 or 0});
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.CaptchaCode);

        }

    }
}
