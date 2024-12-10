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
    public class GetOneFinancialFormulaRequestHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsApiResultWithFinancialFormula_WhenFinancialFormulaExists()
        {
            // Arrange

            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet(new List<Core.Entities.FinancialFormula>() { new()
            {
              Id =1,
              Approved = true,
              RiskCustomerGroupId=10,
            } });
            collection.Context.Setup(x => x.RiskCustomerGroups).ReturnsDbSet(new List<Core.Entities.RiskCustomerGroup>() { new()
            {
              Id =10,
              Title = "Title",
            } });
            var handler = new GetOneFinancialFormulaRequestHandler(collection.UnitOfWork.Object);
            var request = new GetOneFinancialFormulaRequest { Id = 1 };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.Code);
            Assert.NotNull(result.Data);

        }

        [Fact]
        public async Task Handle_ReturnsApiResultWithFinancialFormula_WhenFinancialFormulaNotExists()
        {
            // Arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet(new List<Core.Entities.FinancialFormula>() { new()
            {
              Id =1,
              Approved = true,
              RiskCustomerGroupId=10,
            } });
            collection.Context.Setup(x => x.RiskCustomerGroups).ReturnsDbSet(new List<Core.Entities.RiskCustomerGroup>() { new()
            {
              Id =10,
              Title = "Title",
            } });
            var handler = new GetOneFinancialFormulaRequestHandler(collection.UnitOfWork.Object);
            var request = new GetOneFinancialFormulaRequest { Id = 2 };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.Code);

        }

    }
}
