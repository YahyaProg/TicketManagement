using Application.Services.RiskRankHistService;
using Core.Entities;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.RiskRankHistTest
{
    public class Search
    {
        [Fact]
        public async Task searchRiskRankHist()
        {
            // Arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            var system = new RiskRankHistSearchRequestHandler(collection.Context.Object);

            collection.Context.Setup(x => x.RiskRankItems).ReturnsDbSet(new List<Core.Entities.RiskRankItem>()
            {
                new Core.Entities.RiskRankItem(){Id = 1 }
            }) ;
            collection.Context.Setup(x => x.RiskRankHists).ReturnsDbSet(new List<Core.Entities.RiskRankHist>() { new Core.Entities.RiskRankHist() { Id = 1, ProposalId = 1, BasicRateRiskId = 1, ChangeDate = DateTime.Now } });
            collection.Context.Setup(x => x.Customers).ReturnsDbSet(new List<Core.Entities.Customer>()
            {
                new Core.Entities.Customer(){Id = 1, BankStaffId= 1}
            });

            collection.Context.Setup(x => x.Proposals).ReturnsDbSet(new List<Core.Entities.Proposal>()
            {
                new Core.Entities.Proposal(){Id = 1, BankStaffId = 1, CustomerId = 1 }
            });

            collection.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet(new List<CorporateCustomer>()
            {
                new CorporateCustomer(){Id = 1, IsicGroupId = 1, Name = "amirhosein"}
            });

            collection.Context.Setup(x => x.IsicGroups).ReturnsDbSet(new List<Core.Entities.IsicGroup>()
            {
                new Core.Entities.IsicGroup(){Id = 1, Title = "daya"}
            });

            collection.Context.Setup(x => x.BankStaffs).ReturnsDbSet(new List<Core.Entities.BankStaff>()
            {
                new Core.Entities.BankStaff(){Id = 1 }
            });
            collection.Context.Setup(x => x.Organizations).ReturnsDbSet(new List<Organization>()
            {
                new Organization(){ Id = 1, Title = "daya"}
            });


            // Act
            var result = await system.Handle(new RiskRankHistSearchRequest(), CancellationToken.None);


            // Assert

            Assert.True(result.IsSuccess);
        }
    }
}
