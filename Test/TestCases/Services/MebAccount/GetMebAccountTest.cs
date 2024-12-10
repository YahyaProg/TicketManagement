using Application.Services.MebAccountService;
using Core.Entities;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.MebAccount
{
    public class GetMebAccountTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task GetMebAccount_Test(int type)
        {
            // Arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();


            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet([
                new ProposalScheme(){
                    Id = type != 1 ? 1 : 0, // 1 = 0
                    CustomerId = 1
                }
                ]);

            collection.Context.Setup(x => x.Currencies).ReturnsDbSet([
               new Currency(){Id = 1, Title = "salam"}
               ]);

            collection.Context.Setup(x => x.MebAccounts).ReturnsDbSet([
                new Core.Entities.MebAccount(){
                        Id = 1,
                        CustomerId = 1,
                        AccountNo = "12321312",
                        CreditTurnover = 232,
                        AvgBalance = 123,
                        AccountType = "personal",
                        FromDate = DateTime.Now,
                        ToDate = type == 4 ? DateTime.Now.AddDays(90) : type == 2 ? DateTime.Now.AddDays(180) : type == 3 ? DateTime.Now.AddDays(365) : DateTime.Now.AddDays(90), // 1= 3 mahe 
                        CurrencyId = 1,
                        InterestRate = 213,
                        Currency = new Currency(){ Id = 1, Title = "salam"}
                    }
                ]);

            collection.Context.Setup(x => x.ProposalManagerAccounts).ReturnsDbSet(new List<Core.Entities.ProposalManagerAccount>()
            {
                new()
                {
                    Id = 1
                }
            });


            var handler = new GetMebAccountRequestHandler(collection.Context.Object);

            // Act
            var result = await handler.Handle(new() { Page = 1, Size = 10, ProposalSchemeId = 1 }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);

        }
    }
}
