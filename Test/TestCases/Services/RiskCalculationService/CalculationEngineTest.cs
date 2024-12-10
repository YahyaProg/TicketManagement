using Core.Entities;
using Core.Enums;
using Core.ViewModel.RiskInfoPage;
using DocumentFormat.OpenXml.InkML;
using Moq;
using Moq.EntityFrameworkCore;
using static Application.Services.RiskCalculationEngineService.CalculationEngineRequest;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Services.RiskCalculationServiceTest
{
    public class CalculationEngineTest
    {
        private readonly MoqCollection col = GetUnitOfWorkMoqCollection();


        [Theory]
        [InlineData(1,true)]
        [InlineData(2,false)]
        public async Task RiskCalculationEngine_TEST(int type,bool expected)
        {
            // Arrange
            col.Context.Setup(x => x.Proposals).ReturnsDbSet([
                new Proposal(){
                    Id = 1,
                    CustomerId = 1,
                    Customer = new Customer(){
                        Id =1 ,
                        CorporateCustomer = new(){
                            Id = 1,
                            IsicGroupId = 1,
                            IsicSubGroupId = 1
                        }
                    }
                }]);

            col.Context.Setup(x => x.IsicGroups).ReturnsDbSet([
                new(){Id = 1, Code = "234"}
            ]);

            col.Context.Setup(x => x.IsicSubGroups).ReturnsDbSet([
                new(){Id = 1}
            ]);

            col.Context.Setup(x => x.CustomerRequestDetails).ReturnsDbSet([new() { Amount = 10, ProposalId = 1, CustomerId = 1 }]);
            col.Context.Setup(x => x.RiskCustomerGroupItems).ReturnsDbSet([
               new RiskCustomerGroupItem()
               {
                    Id = 1,
                    IsicSubGroupId = 1,
                    IsicGroupId = 1,
                    RiskCustomerGroupId = 1,
                    RiskCustomerGroup = new(){
                        Id = 1
                    },
                    IsicSubGroup = new(){
                        Id = 1,
                    }
               }]);

            col.Context.Setup(x => x.FinancialValues).ReturnsDbSet([
                new FinancialValue() {Id = 1}
            ]);

            col.Context.Setup(x => x.PrimaryRisks).ReturnsDbSet([
                new Core.Entities.PrimaryRisk() {Id = 11, Category = EPrimaryRisk_category.C, Deleted = false, Ver = 1, FormulaId = 1, Title = "salam"},
                new Core.Entities.PrimaryRisk() {Id = 1, Category = EPrimaryRisk_category.C, Deleted = false, Ver = 1, FormulaId = 1},
                new Core.Entities.PrimaryRisk() {Id = 3, Category = EPrimaryRisk_category.C, Deleted = false, Ver = 1, FormulaId = 1, ParentId = 11},
                new Core.Entities.PrimaryRisk() {Id = 2, Category = EPrimaryRisk_category.C, Deleted = false, Ver = 1, FormulaId = 1, ParentId = 1},
                new Core.Entities.PrimaryRisk() {Id = 4, Category = EPrimaryRisk_category.C, Deleted = false, Ver = 1, RiskInfoGroupId = 1, ParentId = 11},
                //new Core.Entities.PrimaryRisk() {Id = 3, Category = EPrimaryRisk_category.C, Deleted = false, Ver = 1, FormulaId = 1 , ParentId = 1},
                //new Core.Entities.PrimaryRisk() {Id = 4, Category = EPrimaryRisk_category.C, Deleted = false, Ver = 1, FormulaId = 1 , ParentId = 1},
            ]);

            col.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet([
              new FinancialFormula() {Id = 1,
                  PrevYearPercents = "[{\"value\":\"100\"}]",
                  Formula = "[{\"title\":\"ifelse\",\"function\":true},{\"title\":\"(\",\"parantez\":true},{\"title\":\"(\",\"parantez\":true},{\"title\":\"45\",\"number\":true},{\"title\":\"*\",\"operator\":true},{\"id\":1,\"title\":\"تابع نسبت جاری\",\"operand\":true,\"year\":{\"id\":0,\"title\":\"آخرين سال\"}},{\"title\":\")\",\"parantez\":true},{\"title\":\"<\",\"function\":true},{\"title\":\"45\",\"number\":true},{\"title\":\",\",\"function\":true},{\"title\":\"45\",\"number\":true},{\"title\":\"*\",\"operator\":true},{\"id\":12,\"title\":\"تابع نسبت جاری\",\"operand\":true,\"year\":{\"id\":0,\"title\":\"آخرين سال\"}},{\"title\":\",\",\"function\":true},{\"title\":\"45\",\"number\":true},{\"title\":\")\",\"parantez\":true}]"},
              new FinancialFormula() {Id = 12, PrevYearPercents = "[{\"value\":\"100\"}]",
                  Formula = type == 2 ? "[{\"title\":\"(\",\"parantez\":true},{\"title\":\"(\",\"parantez\":true},{\"title\":\"(\",\"parantez\":true},{\"title\":\"100\",\"number\":true},{\"title\":\"/\",\"operator\":true},{\"title\":\"100\",\"number\":true},{\"title\":\")\",\"parantez\":true},{\"title\":\"*\",\"operator\":true},{\"title\":\"100\",\"number\":true},{\"title\":\")\",\"parantez\":true},{\"title\":\"*\",\"operator\":true},{\"title\":\"2\",\"number\":true},{\"title\":\")\",\"parantez\":true},{\"title\":\"/\",\"operator\":true},{\"title\":\"0\",\"number\":true}]"
                  : "[{\"title\":\"(\",\"parantez\":true},{\"title\":\"(\",\"parantez\":true},{\"title\":\"(\",\"parantez\":true},{\"title\":\"100\",\"number\":true},{\"title\":\"/\",\"operator\":true},{\"title\":\"100\",\"number\":true},{\"title\":\")\",\"parantez\":true},{\"title\":\"*\",\"operator\":true},{\"title\":\"100\",\"number\":true},{\"title\":\")\",\"parantez\":true},{\"title\":\"*\",\"operator\":true},{\"title\":\"2\",\"number\":true},{\"title\":\")\",\"parantez\":true},{\"title\":\"/\",\"operator\":true},{\"title\":\"100\",\"number\":true}]"}
            ]);

            col.Context.Setup(x => x.CompanyFinancialInfos).ReturnsDbSet([
               new() {Id = 1}
            ]);

            col.Context.Setup(x => x.FinancialInfoItems).ReturnsDbSet([
             new FinancialInfoItem() {Id = 1, FinancialInfoId = 1, CorporateCustomerId = 1, FinancialInfoDefId = 1}
            ]);

            col.Context.Setup(x => x.FinancialInfos).ReturnsDbSet([
                new() {Id = 1, CurrencyId=1, FinancialYearInfoId = 1}
            ]);
            col.Context.Setup(x => x.FinancialYearInfos).ReturnsDbSet([
               new() {Id = 1, Deleted= false, Finalized = true,Viewed1y= true }
            ]);
            col.Context.Setup(x => x.Currencies).ReturnsDbSet([
               new() {Id = 1}
            ]);
            col.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            col.UnitOfWork.Setup(x => x.RiskInfoGroupRepo.GetRiskInfoWithAnswers(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<ERiskInfoGroup_category>(), It.IsAny<double>())).ReturnsAsync(
            [
                new RiskInfoGroupVm(){Id = 1, Title = "salam", Items = []}
            ]);

            var handler = new CalculationEngineRequestHandler(col.Context.Object, col.UnitOfWork.Object);

            // Act
            var result = await handler.Handle(new() { ProposalId = 1, Ver = 1, Year = 0 }, CancellationToken.None);

            // Assert
            Assert.Equal(expected, result.IsSuccess);
        }
    }
}
