using Application.Services.FinancialFormulaService;
using Core.Entities;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Helper;

namespace Test.TestCases.Services.FinancialFormulaService
{
    public class UpdateTitleFinancialFormulaRequestHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsApiResultWithSuccess_WhenFinancialFormulaWithTitleAlreadyNotExists()
        {
            // Arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet(new List<Core.Entities.FinancialFormula>() { new()
            {
              Id =1,
              Approved = true,
              Title ="OldTitle",
              RiskCustomerGroupId=10,
            } });
            var handler = new UpdateTitleFinancialFormulaRequestHandler(collection.UnitOfWork.Object);
            var request = new UpdateTitleFinancialFormulaRequest { oldTitle = "OldTitle", newTitle = "NewTitle" };
            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);


            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ReturnsApiResultWithFalse_WhenFinancialFormulaWithTitleAlreadyNotExists()
        {
            // Arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet(new List<Core.Entities.FinancialFormula>() { new()
            {
              Id =1,
              Approved = true,
              Title ="OldTitle",
              RiskCustomerGroupId=10,
            } });
            var handler = new UpdateTitleFinancialFormulaRequestHandler(collection.UnitOfWork.Object);
            var request = new UpdateTitleFinancialFormulaRequest { oldTitle = "OldTitle", newTitle = "NewTitle" };
            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(-1);


            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ReturnsApiResultWithFalse_WhenFinancialFormulaWithTitleAlreadyExists()
        {
            // Arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet(new List<Core.Entities.FinancialFormula>() { new()
            {
              Id =1,
              Approved = true,
              Title ="NewTitle",
              RiskCustomerGroupId=10,
            } });
            var handler = new UpdateTitleFinancialFormulaRequestHandler(collection.UnitOfWork.Object);
            var request = new UpdateTitleFinancialFormulaRequest { oldTitle = "OldTitle", newTitle = "NewTitle" };
            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(-1);


            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }
    }
}
