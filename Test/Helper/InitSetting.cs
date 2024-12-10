using Core.Helpers;
using Xunit;

namespace Tests.Helpers
{
    public class InitSettingTests
    {
        [Fact]
        public void InitSetting_PropertyAssignment_WorksCorrectly()
        {
            // Arrange
            var connectionStrings = new SettingConnectionStrings { DefaultConnection = "Server=myServer;Database=myDb;" };
            var keyCloakSetting = new KeyCloakSetting
            {
                AuthServerUrl = "http://authserver.com",
                Realm = "myRealm",
                Resource = "myResource",
                Secret = "mySecret",
                Issuer = "myIssuer",
                RsaExponent = "exponent",
                RsaModulus = "modulus"
            };
            var companyTypePercent = new CompanyTypePercent { IsManufacturing = 60.5, IsNotManufacturing = 39.5 };
            var riskService = new RiskService { BaseUrl = "http://riskservice.com" };
            var defaultTimeOutSec = new DefaultTimeOutSec { RapperService = 30, Database = 60, RabbitMq = 45 };
            var settingRabbit = new SettingRabbit { Host = "localhost", Port = "5672", Username = "guest", Password = "guest" };
            var externalSettings = new ExternalSettings { BaseUrl = "http://externalservice.com", ApiKey = "apiKey123", Rate = 100 };
            var riskRange = new RiskCalculationRange { Min = "0", Max = "100" };

            // Act
            var initSetting = new InitSetting
            {
                ConnectionStrings = connectionStrings,
                JwtSecret = "jwtSecret",
                AesSecretKey = "aesSecretKey",
                DefaultTimeOutSec = defaultTimeOutSec,
                Rabbit = settingRabbit,
                UploadDirectory = "/uploads",
                ExternalSettings = externalSettings,
                IsPublished = true,
                RiskRange = riskRange,
                RiskService = riskService,
                CompanyTypePercent = companyTypePercent,
                KeyCloak = keyCloakSetting
            };

            // Assert
            Assert.Equal(connectionStrings, initSetting.ConnectionStrings);
            Assert.Equal("jwtSecret", initSetting.JwtSecret);
            Assert.Equal("aesSecretKey", initSetting.AesSecretKey);
            Assert.Equal(defaultTimeOutSec, initSetting.DefaultTimeOutSec);
            Assert.Equal(settingRabbit, initSetting.Rabbit);
            Assert.Equal("/uploads", initSetting.UploadDirectory);
            Assert.Equal(externalSettings, initSetting.ExternalSettings);
            Assert.True(initSetting.IsPublished);
            Assert.Equal(riskRange, initSetting.RiskRange);
            Assert.Equal(riskService, initSetting.RiskService);
            Assert.Equal(companyTypePercent, initSetting.CompanyTypePercent);
            Assert.Equal(keyCloakSetting, initSetting.KeyCloak);
        }

        [Fact]
        public void KeyCloakSetting_PropertyAssignment_WorksCorrectly()
        {
            // Arrange
            var keyCloak = new KeyCloakSetting
            {
                AuthServerUrl = "http://authserver.com",
                Realm = "myRealm",
                Resource = "myResource",
                Secret = "mySecret",
                Issuer = "myIssuer",
                RsaExponent = "exponent",
                RsaModulus = "modulus"
            };

            // Assert
            Assert.Equal("http://authserver.com", keyCloak.AuthServerUrl);
            Assert.Equal("myRealm", keyCloak.Realm);
            Assert.Equal("myResource", keyCloak.Resource);
            Assert.Equal("mySecret", keyCloak.Secret);
            Assert.Equal("myIssuer", keyCloak.Issuer);
            Assert.Equal("exponent", keyCloak.RsaExponent);
            Assert.Equal("modulus", keyCloak.RsaModulus);
        }

        [Fact]
        public void CompanyTypePercent_PropertyAssignment_WorksCorrectly()
        {
            // Arrange
            var companyType = new CompanyTypePercent { IsManufacturing = 55.0, IsNotManufacturing = 45.0 };

            // Assert
            Assert.Equal(55.0, companyType.IsManufacturing);
            Assert.Equal(45.0, companyType.IsNotManufacturing);
        }

    }
}