using Application.Services.RiskCalculation;
using Core.Entities;
using Moq.EntityFrameworkCore;
using Newtonsoft.Json;
using static Application.Services.RiskCalculation.JsonDefsRequestHandler;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.RiskCalculationService
{
    public class JsonDefsTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task JsonDefsServiceTest(long GroupId)
        {
            var request = new JsonDefsRequest()
            {
                ProposalId = 1
            };


            var moqCollection = GetUnitOfWorkMoqCollection();
            var handler = new JsonDefsRequestHandler(moqCollection.UnitOfWork.Object);

            moqCollection.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet([
                new IndividualCustomer
                {
                    Id = 12345,

                }
                ]);

            moqCollection.Context.Setup(x => x.Proposals).ReturnsDbSet([
                new Proposal
                {
                    Id = request.ProposalId,
                    CustomerId = 12345,
                    Customer = new Customer { Id = 12345, ClientNo = "C123456" },
                    Deleted = false
                }
                ]);

            moqCollection.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet([

               new Core.Entities.CustomerScheme
                {
                    Id = 1,
                    CustomerId = 12345,
                    Customer = new Customer { Id = 12345, ClientNo = "C123456" },
                    ProposalSchemeId = 1
                }
               ]);

            moqCollection.Context.Setup(x => x.OccupationInformations).ReturnsDbSet([

              new Core.Entities.OccupationInformation
                {
                    Id = 1,
                    CustomerSchemeId = 1,
                    IsicGroupId = 1
                }
              ]);

            moqCollection.Context.Setup(x => x.RiskCustomerGroupItems).ReturnsDbSet([

            new Core.Entities.RiskCustomerGroupItem
                {
                    Id = 1,
                    IsicGroupId = 1
                }
            ]);


            moqCollection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet([

               new Core.Entities.ProposalScheme
                {
                    Id = 1,
                    CustomerId = 12345,
                    Customer = new Customer { Id = 12345, ClientNo = "C123456" },
                    Deleted = false,
                    ProposalId = request.ProposalId
                }
               ]);

            // Mocking Customers
            moqCollection.Context.Setup(x => x.Customers).ReturnsDbSet([
                 new Customer
                {
                    Id = 12345,
                    ClientNo = "C123456"
                }
                ]);

            // Mocking CorporateCustomers
            moqCollection.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet(
               [ new CorporateCustomer
                {
                    Id = 12345
                }]
            );

            // Mocking FinancialYearInfos
            moqCollection.Context.Setup(x => x.FinancialYearInfos).ReturnsDbSet(
                [new Core.Entities.FinancialYearInfo
                {
                    CorporateCustomerId = 12345,
                    Deleted = false,
                    Finalized = true,
                    FromDate = DateTime.Now.AddYears(-1),
                    ToDate = DateTime.Now,
                    Viewed1y = true
                }]
            );

            // Mocking RiskAdjustmentGroups
            moqCollection.Context.Setup(x => x.RiskAdjustmentGroups).ReturnsDbSet(
               [ new RiskAdjustmentGroup
                {
                    Id = 1,
                    Title = "Risk Adjustment Group 1",
                    Deleted = false,
                    ApproverId = 101,
                }]
            );

            moqCollection.Context.Setup(x => x.RiskAdjustmentItems).ReturnsDbSet([
                       new RiskAdjustmentItem { Id = 1, Title = "Adjustment Item 1", Value = 1, Deleted = false, RiskAdjustmentGroupId = 1 },
                        new RiskAdjustmentItem { Id = 2, Title = "Adjustment Item 2", Value = 2, Deleted = false, RiskAdjustmentGroupId = 1 }
                ]);

            // Mocking RiskRanks
            moqCollection.Context.Setup(x => x.RiskRanks).ReturnsDbSet(
               [ new RiskRank
                {
                    ProposalId = request.ProposalId,
                    LatePaymentRiskUserId = 1,
                    RmComment = "Risk Manager Comment",
                    RiskComment = "Risk Comment"
                }]
            );

            // Mocking RiskRankItems
            moqCollection.Context.Setup(x => x.RiskRankItems).ReturnsDbSet(
               [ new RiskRankItem
                {
                    Id = 1,
                    Title = "Rank Item 1",
                    Value = 5
                }]
            );

            // Mocking RiskRankAdjustments
            moqCollection.Context.Setup(x => x.RiskRankAdjustments).ReturnsDbSet(
                [new RiskRankAdjustment
                {
                    AdjustmentItemId = 1,
                    RiskRankId = 1
                }]
            );

            // Mocking CutoffConfigs
            moqCollection.Context.Setup(x => x.CutoffConfigs).ReturnsDbSet(
                [new Core.Entities.CutoffConfig
                {
                    Type = "risk-mapping",
                    Cutoff = JsonConvert.SerializeObject(new List<Cutoff> { new Cutoff { Start = 0, End = 100, Value = "High Risk" } })
                },
                new Core.Entities.CutoffConfig
                {
                    Type = "late-payment-effect",
                    Cutoff = JsonConvert.SerializeObject(new List<Cutoff> { new Cutoff { Start = null, End = null, Value = "1.5" } })
                }
            ]
            );

            // indv id = 12345

            // Mocking PrimaryRisks
            moqCollection.Context.Setup(x => x.PrimaryRisks).ReturnsDbSet(
                [new Core.Entities.PrimaryRisk
                {
                    RiskCustomerGroupId = GroupId,// 1-0
                    Ver = 1
                }]
            );

            //Act
            var res = await handler.Handle(request, CancellationToken.None);



            //Assert
            Assert.True(res.IsSuccess);

        }
    }
}
