using Application.Services.RiskCalculation;
using Core.Enums;
using Core.Entities;
using Core.Helpers;
using Moq;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;
using Core.ViewModel;

namespace Test.TestCases.Services.RiskCalculationService
{
    public class SaveRiskCalculationRequestTest
    {
        public static IEnumerable<object[]> requests =>
          new List<object[]>
          {
                        new object[] { 1 },
                        new object[] { 2 },
                        new object[] { 3 },
          };

        [Theory, MemberData(nameof(requests))]
        public async Task Handle_SaveRiskCalculationRequest_ShouldReturnSuccess(int testCase)
        {

            // Arrange
            var request = new SaveRiskCalculationRequest()
            {
                proposalId = 1,
                needApproved = testCase is 1 ? true : false,
                finalRisk = new()
                {
                    label = "High",
                    value = 30,
                    labelRm = testCase is 1 ? "" : testCase is 2 ?"2113" : "" ,
                    valueRisk = 1,
                    valueRm = 2,
                },
                items = new List<Core.ViewModel.RiskCalculation.RiskCalcItem>()
                {
                    new(){
                        id = 1,finalAfterCutoff = 2, customAfterCutoff = 1, items =
                            new Core.ViewModel.RiskCalculation.RiskCalcItem[]
                            {
                                new()
                                {
                                    id = 1,
                                }
                            }
                    }
                },
                tadils = [
                    new(){id = 1, title = "salam", value = new(){ Id = 1, Title = "salam2", Value = 1 } }
                    ]
            };
            var moq = GetUnitOfWorkMoqCollection();
            var helper = new Mock<IUserHelper>();
            var user = new UserDto { Id = "2" };

            helper.Setup(x => x.GetUserFromToken()).Returns(user);

            moq.Context.Setup(x => x.FinancialValues).ReturnsDbSet(
            [
                new FinancialValue { Id = 1 }
            ]);

            moq.Context.Setup(x => x.RiskRankAdjustments).ReturnsDbSet(
           [
               new RiskRankAdjustment { Id = 1 }
           ]);

            moq.Context.Setup(x => x.Proposals).ReturnsDbSet(
            [
                new Proposal { Id = request.proposalId, CustomerId = 1 }
            ]);

            moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet(new List<BankStaff>
            {
                new BankStaff { Id = 1, UserId = user.Id, OrganizationId = 1 }
            });

            moq.Context.Setup(x => x.Organizations).ReturnsDbSet(new List<Organization>
            {
                new Organization { Id = 1, OrganizationType = testCase is 1 ? EOrganizationType.risk : testCase is 2 ? EOrganizationType.credit_department : EOrganizationType.credit_department}, // EOrganizationType.credit_department
            });

            moq.Context.Setup(x => x.RiskRankItems).ReturnsDbSet(new List<RiskRankItem>
            {
                new RiskRankItem { Title = "High" },
                new RiskRankItem { Title = "Medium" },
                new RiskRankItem { Title = "Low" }
            });

            moq.Context.Setup(x => x.InqueryValidationConfigs).ReturnsDbSet(new List<Core.Entities.InqueryValidationConfig>
            {
                new Core.Entities.InqueryValidationConfig { InqueryType = EInqueryType.ValidityRemainingTime, ValidationDays = 365 }
            });

            moq.Context.Setup(x => x.RiskRankHists).ReturnsDbSet(new List<RiskRankHist>
            {
                new RiskRankHist { ProposalId = request.proposalId, ReportNumber = 1 }
            });

            moq.Context.Setup(x => x.RiskRanks).ReturnsDbSet(new List<RiskRank>
            {
                new RiskRank { ProposalId = request.proposalId }
            });

            moq.Context.Setup(x => x.OtherOrgAccesses).ReturnsDbSet(new List<OtherOrgAccess>
            {
                new OtherOrgAccess { ProposalId = request.proposalId, Org = "risk" }
            });


            var handler = new SaveRiskCalculationRequestHandler(moq.Context.Object, helper.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            moq.Context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        }
    }
}
