using Application.Services.LcmebService;
using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Gateway.External.IServices;
using Gateway.Model.External.Requests;
using Gateway.Model.External.Responses;
using Moq;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.LcMebTests
{
    public class EstelamLcMebHandlerTests
    {

        [Theory]
        [InlineData((long)1001, (long)23)]
        [InlineData((long)3001, (long)23)]
        [InlineData((long)3001, (long)0)]
        public async Task Handle_EstelamLcMeb(long CustomerID, long RemainingAmount)
        {
            // Arrange
            var request = new EstelamLcMebRequest
            {
                ProposalId = 1,
                CustomerId = 1
            };

            InitSetting initSetting = new InitSetting()
            {
                ExternalSettings = new()
                {
                    Rate = 1
                }
            };
            var externalServicesMock = new Mock<IExternalServices>();
            var moqCollection = MoqHelper.GetUnitOfWorkMoqCollection();
            var handler = new EstelamLcMebRequestHandler(moqCollection.UnitOfWork.Object, externalServicesMock.Object, initSetting);

            // Mock for Proposals DbSet
            moqCollection.Context.Setup(x => x.Proposals).ReturnsDbSet(new List<Proposal>
            {
                new Proposal
                {
                    Id = 1,
                    CustomerId = 1001,
                    Customer = new Customer
                    {
                        Id = 1001,
                        CorporateCustomer = new CorporateCustomer { Id = 1001, CorpId = "Corp123" },
                        IndividualCustomer = null
                    }
                }
            });

            // Mock for CorporateCustomers DbSet
            moqCollection.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet(new List<CorporateCustomer>
            {
                new CorporateCustomer { Id = 1001, CorpId = "Corp123" }
            });

            // Mock for IndividualCustomers DbSet
            moqCollection.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet(new List<IndividualCustomer>
            {
                new IndividualCustomer { Id = 2001, NationalId = "Indiv123" }
            });

            // Mock for ProposalSchemes DbSet
            moqCollection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet(new List<Core.Entities.ProposalScheme>
            {
                new Core.Entities.ProposalScheme
                {
                    Id = 10,
                    Proposal = new Proposal { Id = 1 },
                    CreateDate = DateTime.Now.AddDays(-10),
                    Deleted = false
                }
            });

            // Mock for CustomerSchemes DbSet
            moqCollection.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet(new List<Core.Entities.CustomerScheme>
            {
                new Core.Entities.CustomerScheme { Id = 20, ProposalScheme = new Core.Entities.ProposalScheme { Id = 10 } }
            });

            // Mock for Managers DbSet
            moqCollection.Context.Setup(x => x.Managers).ReturnsDbSet(new List<Manager>
            {
                new Manager { Id = 1, CustomerScheme = new Core.Entities.CustomerScheme { Id = 20 }, PersonId = 3001 }
            });

            // Mock for Customers DbSet
            moqCollection.Context.Setup(x => x.Customers).ReturnsDbSet(new List<Customer>
            {
                new Customer
                {
                    Id = 3001,
                    CorporateCustomer = new CorporateCustomer { Id = 3001, CorpId = "ManagerCorp" },
                    IndividualCustomer = null
                }
            });

            // Mock for Lcmebs DbSet
            moqCollection.Context.Setup(x => x.Lcmebs).ReturnsDbSet(new List<Lcmeb>
            {
                new Lcmeb {
                    Id = 1,
                    CustomerId = CustomerID,
                    AccountNo = "Acc123",
                    LcSettleTypeId = 1,
                    CurrencyId = 1
                }
            });

            // Mock for ProposalManagerLcs DbSet
            moqCollection.Context.Setup(x => x.ProposalManagerLcs).ReturnsDbSet(new List<ProposalManagerLc>
            {
                new ProposalManagerLc { Id = 1, ProposalId = 1, Lc = new Lcmeb { Id = 1 } }
            });

            // Mock for LcsettleTypes DbSet
            moqCollection.Context.Setup(x => x.LcsettleTypes).ReturnsDbSet(new List<LcsettleType>
            {
                new LcsettleType { Id = 1, Code = ELCSettleType_code.sight }
            });

            // Mock for Currencies DbSet
            moqCollection.Context.Setup(x => x.Currencies).ReturnsDbSet(new List<Currency>
            {
                new Currency { Id = 1, Title = "USD", Code = "USD" },
                new Currency { Id = 2, Title = "IRR", Code = "IRR" }
            });


            var cbRes = MoqHelper.GetExternalResponseMoq(new CbCustomerResponse()
            {
                CustomerId = "23"
            });
            externalServicesMock.Setup(x => x.CbCustomer(It.IsAny<CbCustomerRequest>())).ReturnsAsync(cbRes);


            var cblcRes = MoqHelper.GetExternalResponseMoq(new List<CbLcResponse>()
            {
                new() { Amount = 23, Remainingamount = RemainingAmount, Accountno = "Acc123" }
            });
            externalServicesMock.Setup(x => x.CbLc(It.IsAny<CbLcRequest>())).ReturnsAsync(cblcRes);

            // Act
            var res = await handler.Handle(request, CancellationToken.None);


            // Assert
            Assert.True(res.IsSuccess);
        }

    }
}
