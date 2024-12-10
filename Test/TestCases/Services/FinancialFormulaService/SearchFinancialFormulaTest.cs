using Application.Services.FinancialFormulaService;
using Core.Entities;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.FinancialFormulaService
{
    public class SearchFinancialFormulaTest
    {

        [Fact]
        public async Task SearchFinancialFormula_Test()
        {
            // Arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet([
                new FinancialFormula(){Id = 1, ApproverId = 1, CreatorId = 1, RiskCustomerGroupId = 1, Title = "amir"}
                ]);

            collection.Context.Setup(x => x.BankStaffs).ReturnsDbSet([
                new BankStaff(){Id = 1 , OrganizationId = 1, UserId = "123"}
                ]);

            collection.Context.Setup(x => x.RiskCustomerGroups).ReturnsDbSet([
              new RiskCustomerGroup(){Id = 1}
              ]);


            var request = new SearchFinancialFormulaRequest()
            {
                Size = 1,
                Page = 0,
                Title = "amir",
            };

            var handler = new SearchlFinancialFormulaRequestHandler(collection.UnitOfWork.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
