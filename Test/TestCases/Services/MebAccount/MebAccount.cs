using Application.Services.MebAccountService;
using Core.Entities;
using Core.Enums;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.MebAccount
{
    public class MebAccountTest
    {

        [Theory]
        [InlineData(EMebAccount_Accperiod.YeSale)]
        [InlineData(EMebAccount_Accperiod.SeMahe)]
        [InlineData(EMebAccount_Accperiod.ShishMahe)]
        public async Task GetCbAccountTurnoverRequestHandler(EMebAccount_Accperiod accperiod)
        {
            // Arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            var system = new GetCbAccountTurnoverRequestHandler(collection.UnitOfWork.Object);
            var request = new GetCbAccountTurnoverRequest()
            {
                OwnerId = 1,
                Accperiod = accperiod,
                Page = 1,
                Size = 10
            };

            var cbAccountTurnOvers = new List<CbAccountTurnover>()
            {
                new CbAccountTurnover()
                {
                    CurrencyId = 1,
                    OpeningDate = DateTime.Now,
                    OwnerId = 1
                }
            };

            var currencies = new List<Core.Entities.Currency>()
            {
                new Core.Entities.Currency()
                {
                    Id = 1,
                    Title = "امیریه"
                }
            };

            collection.Context.Setup(x => x.AccountTurnovers).ReturnsDbSet(cbAccountTurnOvers);
            collection.Context.Setup(x => x.Currencies).ReturnsDbSet(currencies);


            // Act
            var result = await system.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
