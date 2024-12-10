using Application.Services.MebAccountService;
using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Gateway.External.IServices;
using Gateway.Model.External.Requests;
using Gateway.Model.External.Responses;
using Moq;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.MebAccount
{
    public class Estelam
    {
        //public static IEnumerable<object[]> requests =>
        //  new List<object[]>
        //  {
        //        new object[] { EMebAccount_Accperiod.YeSale },
        //        new object[] { EMebAccount_Accperiod.SeMahe },
        //        new object[] { EMebAccount_Accperiod.ShishMahe },
        //  };

        private readonly Mock<IExternalServices> externalService = new();

        //[Theory, MemberData(nameof(requests))]
        [Fact]
        public async Task EstelamMebAccountTest()
        {
            var initSetting = new InitSetting
            {
                ExternalSettings = new ExternalSettings
                {
                    Rate = 1
                }
            };
            // Arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            var system = new EstelamMebAccountRequestHandler(collection.UnitOfWork.Object, externalService.Object, initSetting);

            var request = new EstelamMebAccountRequest()
            {
                ProposalId = 1
            };


            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet(new List<Core.Entities.ProposalScheme>()
            {
                new()
                {
                    Id= 1,
                    Deleted = false,
                    ProposalId = 1
                }
            });

            collection.Context.Setup(x => x.Customers).ReturnsDbSet(new List<Core.Entities.Customer>()
            {
                new()
                {
                    Id= 1,
                    ClientNo = "22",
                    IndividualCustomer = new()
                    {
                        Id = 1,
                        NationalId = "2051023"
                    },
                    CorporateCustomer = new()
                    {
                        Id = 1,
                        CorpId = "1235"
                    }
                }
            });

            collection.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet(new List<Core.Entities.CustomerScheme>()
            {
                new()
                {
                    Id= 1,
                    ProposalSchemeId = 1,
                    CustomerId =1,
                    ProposalId = 1
                }
            });

            collection.Context.Setup(x => x.Managers).ReturnsDbSet(new List<Core.Entities.Manager>()
            {
                new()
                {
                    CustomerSchemeId = 1,
                    Id = 2,
                    CorpBoardOfDirectorId = 1,
                    PersonId = 1,
                    PositionTypeId = 1
                }
            });

            collection.Context.Setup(x => x.Proposals).ReturnsDbSet(new List<Core.Entities.Proposal>()
            {
                new Proposal()
                {
                    Id = 1,
                    CustomerId = 1,
                    Customer = new()
                    {
                        Id = 1,
                        CorporateCustomer = new()
                        {
                            Id= 1,
                            CorpId = "232"
                        },
                        IndividualCustomer = new()
                        {
                            Id = 1,
                            NationalId = "2312"
                        }
                    } // 1:Crp or 2:Indv
                }
            });

            collection.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet(new List<CorporateCustomer>()
            {
                new CorporateCustomer() {Id =  1}
            });

            collection.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet(new List<IndividualCustomer>()
            {
                new IndividualCustomer() {Id =  1}
            });

            collection.Context.Setup(x => x.BlackListAcctTypes).ReturnsDbSet(new List<Core.Entities.BlackListAcctType>()
            {
                new()
                {
                    Type = EBlackListAcctType_type.acct,
                    Module = "23"
                }
            });

            collection.Context.Setup(x => x.Currencies).ReturnsDbSet(new List<Core.Entities.Currency>()
            {
                new()
                {
                    Code = "IRRR"
                }
            });

            collection.Context.Setup(x => x.ProposalManagerAccounts).ReturnsDbSet(new List<Core.Entities.ProposalManagerAccount>()
            {
                new()
                {
                    Id = 1
                }
            });


            collection.Context.Setup(x => x.MebAccounts).ReturnsDbSet(new List<Core.Entities.MebAccount>()
            {
                new()
                {
                    ProposalId = 4,
                }
            });

            // ExternalServices
            var cbAccTurnOverRes = MoqHelper.GetExternalResponseMoq(new List<CbAccountTurnoverResponse>
            {
                new()
                {
                    AccountNo = "12412412",
                    Currency = "IR",
                    AccountType = "private",
                    Branch = "babol",
                    FinalBalance = "10000",
                    InterestRate = "23",
                    OpeningDate = "1402/2/4",
                    AvgBalanceHalf = "232",
                    AvgBalanceQuarter = "23",
                    AvgBalanceYear = "34"
                }
            });

            // ExternalServices
            var cbCustomerRes = MoqHelper.GetExternalResponseMoq(new CbCustomerResponse()
            {
                CustomerId = "23"
            });
            externalService.Setup(x => x.CbAccountTurnover(It.IsAny<CbAccountTurnoversRequest>())).ReturnsAsync(cbAccTurnOverRes);
            externalService.Setup(x => x.CbCustomer(It.IsAny<CbCustomerRequest>())).ReturnsAsync(cbCustomerRes);


            // Act
            var result = await system.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
