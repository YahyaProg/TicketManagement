using Application.Services.AuthService;
using Application.Services.CutoffConfigService;
using Application.Services.RiskRankHistService;
using Core.Entities;
using Core.Enums;
using Core.GenericResultModel;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.RiskRankHistTest
{
    public class GetRiskRankInfoTest
    {
        readonly Mock<IMediator> mediator = new();

        [Fact]
        public async Task GetRiskRankInfoRequestFoundTest()
        {
            // Arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            var system = new GetRiskRankInfoRequestHandler(collection.Context.Object, mediator.Object);

            mediator.Setup(x => x.Send(It.IsAny<GetCutoffConfigByTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ApiResult<Core.Entities.CutoffConfig>
            {
                Data = new()
                {
                    Id = 1,
                    Cutoff = "[{\"value\":\"2\",\"end\":\"3\",\"collaterals\":\"1حداقل 20% وثایق گروه یک (غیرمنقول و گروه نقد) و الباقی قرارداد(چک/سفته)\"},{\"start\":\"0\",\"value\":\"D\",\"end\":\"2\",\"collaterals\":\"2حداقل 20% وثایق گروه یک (غیرمنقول و گروه نقد) و الباقی قرارداد(چک/سفته)\"},{\"start\":\"2\",\"value\":\"C\",\"end\":\"4\",\"collaterals\":\"3حداقل 20% وثایق گروه یک (غیرمنقول و گروه نقد) و الباقی قرارداد(چک/سفته)\"},{\"start\":\"4\",\"value\":\"B\",\"end\":\"6\",\"collaterals\":\"4حداقل 20% وثایق گروه یک (غیرمنقول و گروه نقد) و الباقی قرارداد(چک/سفته)\"},{\"start\":\"6\",\"value\":\"8\",\"end\":\"8\",\"collaterals\":\"5حداقل 20% وثایق گروه یک (غیرمنقول و گروه نقد) و الباقی قرارداد(چک/سفته)\"},{\"start\":\"8\",\"value\":\"10\",\"collaterals\":\"6حداقل 20% وثایق گروه یک (غیرمنقول و گروه نقد) و الباقی قرارداد(چک/سفته)\"}]",
                    Type = ""
                }
            });

            collection.Context.Setup(x => x.RiskRankItems).ReturnsDbSet(new List<RiskRankItem>() {
                new() { Id = 1 }
            });

            collection.Context.Setup(x => x.RiskRankHists).ReturnsDbSet(new List<RiskRankHist>() {
                new RiskRankHist() {
                    Id = 1,
                    ProposalId = 1,
                    BasicRateRiskId = 1,
                    ChangeDate = DateTime.Now,
                    BasicRateId = 1,
                    BasicRateRmId = 1,
                    BRateAdjId =1,
                    BRateAdjRmId = 1,
                    BRateAdjRiskId = 1,
                    FinalRateId = 1,
                    FinalRateRmId = 1,
                    FinalRateRiskUserId = 1,
                    LatePaymentId = 1,
                    FinalScore = 23
                }
            });

            collection.Context.Setup(x => x.Proposals).ReturnsDbSet(new List<Proposal>() {
                new() { Id = 1, BankStaffId = 1, CustomerId = 1 }
            });

            collection.Context.Setup(x => x.Customers).ReturnsDbSet(new List<Customer>() {
                new() { Id = 1, BankStaffId = 1,ClientNo="123" }
            });


            collection.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet(new List<CorporateCustomer>() {
                new() { Id = 1, IsicGroupId = 1, Name = "تست" }
            });

            collection.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet(new List<IndividualCustomer>() {
                new() { Id = 1, LastName = "man", FirstName = "تست", NationalId = "232" }
            });

            collection.Context.Setup(x => x.IsicGroups).ReturnsDbSet(new List<IsicGroup>() {
                new() { Id = 1, Title = "تولیدی" }
            });

            collection.Context.Setup(x => x.BankStaffs).ReturnsDbSet(new List<BankStaff>() {
                new() { Id = 1, FirstName = "حسن", LastName = "محمدی",OrganizationId=1 }
            });
            collection.Context.Setup(x => x.Organizations).ReturnsDbSet(new List<Organization>() {
                new() { Id = 1, Title = "اعتبارات" }
            });

            collection.Context.Setup(x => x.ProposalCreditItems).ReturnsDbSet([
                new ProposalCreditItem(){
                    Id = 1,
                    ProposalSchemeId = 1,
                    ProposalId = 1,
                    CreditPeriodId = 1,
                    CreditSubTypeId = 1,
                    CreditTypeId = 1,
                    Amount = 123
               }]);
            collection.Context.Setup(x => x.CreditTypes).ReturnsDbSet([
                new CreditType(){
                    Id = 1,
                    Title = "salam"
                }]);
            collection.Context.Setup(x => x.CreditSubTypes).ReturnsDbSet([
                new CreditSubType(){
                    Id = 1,
                    CreditTypeId= 1,
                }
                ]);

            // Act
            var result = await system.Handle(new GetRiskRankInfoRequest() { ProposalId = 1 }, CancellationToken.None);

            // Assert

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetRiskRankInfoRequestNotFoundTest()
        {
            // Arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            var system = new GetRiskRankInfoRequestHandler(collection.Context.Object, mediator.Object);

            mediator.Setup(x => x.Send(It.IsAny<GetCutoffConfigByTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ApiResult<Core.Entities.CutoffConfig>
            {
                Data = new()
                {
                    Id = 1,
                    Cutoff = "[{\"value\":\"E\",\"end\":\"0\",\"collaterals\":\"1حداقل 20% وثایق گروه یک (غیرمنقول و گروه نقد) و الباقی قرارداد(چک/سفته)\"},{\"start\":\"0\",\"value\":\"D\",\"end\":\"2\",\"collaterals\":\"2حداقل 20% وثایق گروه یک (غیرمنقول و گروه نقد) و الباقی قرارداد(چک/سفته)\"},{\"start\":\"2\",\"value\":\"C\",\"end\":\"4\",\"collaterals\":\"3حداقل 20% وثایق گروه یک (غیرمنقول و گروه نقد) و الباقی قرارداد(چک/سفته)\"},{\"start\":\"4\",\"value\":\"B\",\"end\":\"6\",\"collaterals\":\"4حداقل 20% وثایق گروه یک (غیرمنقول و گروه نقد) و الباقی قرارداد(چک/سفته)\"},{\"start\":\"6\",\"value\":\"A\",\"end\":\"8\",\"collaterals\":\"5حداقل 20% وثایق گروه یک (غیرمنقول و گروه نقد) و الباقی قرارداد(چک/سفته)\"},{\"start\":\"8\",\"value\":\"A+\",\"collaterals\":\"6حداقل 20% وثایق گروه یک (غیرمنقول و گروه نقد) و الباقی قرارداد(چک/سفته)\"}]",
                    Type = ""
                }
            });

            collection.Context.Setup(x => x.RiskRankItems).ReturnsDbSet(new List<RiskRankItem>() {
                new() { Id = 1 }
            });

            collection.Context.Setup(x => x.RiskRankHists).ReturnsDbSet(new List<RiskRankHist>() { new RiskRankHist() { Id = 1, ProposalId = 1, BasicRateRiskId = 1, ChangeDate = DateTime.Now } });

            collection.Context.Setup(x => x.Customers).ReturnsDbSet(new List<Customer>() {
                new() { Id = 1, BankStaffId = 1 }
            });

            collection.Context.Setup(x => x.Proposals).ReturnsDbSet(new List<Proposal>() {
                new() { Id = 1, BankStaffId = 1, CustomerId = 1 }
            });
            collection.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet(new List<IndividualCustomer>() {
                new() { Id = 1, LastName = "man", FirstName = "تست", NationalId = "232" }
            });


            collection.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet(new List<CorporateCustomer>() {
                new() { Id = 1, IsicGroupId = 1, Name = "تست" }
            });

            collection.Context.Setup(x => x.IsicGroups).ReturnsDbSet(new List<IsicGroup>() {
                new() { Id = 1, Title = "تولیدی" }
            });

            collection.Context.Setup(x => x.BankStaffs).ReturnsDbSet(new List<BankStaff>() {
                new() { Id = 1, FirstName = "حسن", LastName = "محمدی", OrganizationId = 1 }
            });
            collection.Context.Setup(x => x.Organizations).ReturnsDbSet(new List<Organization>() {
                new() { Id = 1, Title = "daya" }
            });

            // Act
            var result = await system.Handle(new GetRiskRankInfoRequest() { ProposalId = 3 }, CancellationToken.None);

            // Assert
            Assert.Equal(200, result.Code);
        }
    }
}