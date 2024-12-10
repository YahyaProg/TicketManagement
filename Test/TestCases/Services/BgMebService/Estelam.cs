using Application.Services.BgmebService;
using Core.Entities;
using Core.Helpers;
using Gateway.External.IServices;
using Gateway.Model.External.Requests;
using Gateway.Model.External.Responses;
using Moq;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.BgMebService
{
    public class Estelam
    {
        private readonly Mock<IExternalServices> externalService = new();

        [Fact]
        public async Task EstelamBgMebTest()
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
            var system = new EstelamBgMebRequestHandler(collection.UnitOfWork.Object, externalService.Object, initSetting);

            var request = new EstelamBgMebRequest()
            {
                ProposalId = 1,
                CustomerId = 1
            };

            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet(new List<ProposalScheme>()
            {
                new()
                {
                    Id= 1,
                }
            });

            collection.Context.Setup(x => x.ProposalManagerBgs).ReturnsDbSet(new List<ProposalManagerBg>()
            {
                new()
                {
                    Id= 11,
                }
            });

            collection.Context.Setup(x => x.Customers).ReturnsDbSet(new List<Core.Entities.Customer>()
            {
                new()
                {
                    Id= 1,
                    IndividualCustomer = new()
                    {
                        Id = 1,
                        NationalId = "2329432"
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

            collection.Context.Setup(x => x.Managers).ReturnsDbSet(new List<Manager>()
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

            collection.Context.Setup(x => x.BankGuaranteeTypes).ReturnsDbSet(new List<BankGuaranteeType>());

            collection.Context.Setup(x => x.Proposals).ReturnsDbSet(new List<Core.Entities.Proposal>()
            {
                new Core.Entities.Proposal()
                {
                    Id = 1,
                    Customer = new()
                    {
                        Id = 1,
                        CorporateCustomer = new()
                        {

                        }
                    } // 1:Crp or 2:Indv
                }
            });

            collection.Context.Setup(x => x.Currencies).ReturnsDbSet(new List<Core.Entities.Currency>()
            {
                new()
                {
                    Code = "IRRR"
                }
            });

            collection.Context.Setup(x => x.Bgmebs).ReturnsDbSet(new List<Bgmeb>());
            collection.Context.Setup(x => x.LoanMebs).ReturnsDbSet(new List<Core.Entities.LoanMeb>()
            {
                new Core.Entities.LoanMeb()
                {
                    Id = 1,
                    LoanStatusId = 2,
                }
            });

            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet([new() {
                Id = 1,
                ProposalId = 1
            }]);

            // ExternalServices
            var BgResponse = MoqHelper.GetExternalResponseMoq(new List<CbBgResponse>()
            {
               new()
               {
                   AccountNo = "123",
                   BgNewamt = 23,
                   CurrencyCode = "IRr",
                   Settled = true,
                   NpaclassCode = "DA",
                   OutSatnding = 1,
                   LoanNo = "23"
               }
            });

            externalService.Setup(x => x.CbBg(It.IsAny<CbBgRequest>())).ReturnsAsync(BgResponse);

            var CustomerResponse = MoqHelper.GetExternalResponseMoq(new CbCustomerResponse()
            {
                CustomerId = "1"
            });

            externalService.Setup(x => x.CbCustomer(It.IsAny<CbCustomerRequest>())).ReturnsAsync(CustomerResponse);

            // Act
            var result = await system.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
