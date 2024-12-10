using Application.Services.InqueryData.Inqueries;
using Core.GenericResultModel;
using Core.Logger;
using Gateway.External.IServices;
using Gateway.Model.External.Requests;
using Gateway.Model.External.Responses;
using Moq;
using Moq.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Test.Helper;

namespace Test.TestCases.Services.InqueryData.inqueriesTests
{
    public class EstelamChequeInqueryTest
    {
        [Theory]
        [InlineData(Core.Enums.EInqueryType.ReturnedCheckBalance)]
        [InlineData(Core.Enums.EInqueryType.NumberOfBouncedChecks)]
        [InlineData(Core.Enums.EInqueryType.ReturningChequeAmountInParsian)]
        [InlineData(Core.Enums.EInqueryType.ReturningChequeAmountOtherBanks)]
        public async Task EstelamChequeTest(Core.Enums.EInqueryType inqueryType)
        {
            // arrange
            var logger = new Mock<ILoggerManager>();
            var external = new Mock<IExternalServices>();
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            collection.Context.Setup(x => x.InqueryValidationConfigs).ReturnsDbSet([
                new Core.Entities.InqueryValidationConfig(){Id = 1, InqueryType = Core.Enums.EInqueryType.ReturnedCheckBalance, Formula = "1 + (2 * 3)"},
                new Core.Entities.InqueryValidationConfig(){Id = 2, InqueryType = Core.Enums.EInqueryType.NumberOfBouncedChecks, Formula = "1 + (2 * 3)"},
                new Core.Entities.InqueryValidationConfig(){Id = 3, InqueryType = Core.Enums.EInqueryType.ReturningChequeAmountInParsian, Formula = "1 + (2 * 3)"},
                new Core.Entities.InqueryValidationConfig(){Id = 4, InqueryType = Core.Enums.EInqueryType.ReturningChequeAmountOtherBanks, Formula = "1 + (2 * 3)"},
                ]);

            collection.Context.Setup(x => x.InqueryDatas).ReturnsDbSet([]);

            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);


            var estelamcheqRes = MoqHelper.GetExternalResponseMoq<EstelamChequeResponse>(new()
            {
                BouncedCheques = [
                    new(){Amount = 123, BouncedAmount = 2, BankCode = 54},
                    new(){Amount = 123, BouncedAmount = 2, BankCode = 34}
                    ]
            });

            external.Setup(x => x.EstelamCheque(It.IsAny<EstelamChequeRequest>())).ReturnsAsync(estelamcheqRes);


            var system = new EstelamChequeInquery(collection.Context.Object, external.Object, logger.Object);

            // act
            var result = await system.EstelamCheque(new() { Id = 1 }, "232", "2323", inqueryType);


            // assert
            Assert.True(result.IsSuccess);
        }
    }
}
