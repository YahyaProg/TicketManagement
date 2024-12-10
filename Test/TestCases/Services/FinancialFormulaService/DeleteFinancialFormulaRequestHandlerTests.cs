using Application.Services.FinancialFormulaService;
using Core.Entities;
using Core.GenericResultModel;
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
    public class DeleteFinancialFormulaRequestHandlerTests
    {


        [Fact]
        public async Task Handle_DeleteFinancialFormulaRequest_ReturnsApiResult()
        {
            // Arrange
            var request = new DeleteFinancialFormulaRequest { Id = 1 };

            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            DeleteFinancialFormulaRequestHandler _handler = new DeleteFinancialFormulaRequestHandler(collection.UnitOfWork.Object);

            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet(new List<Core.Entities.FinancialFormula>() { new()
            {
              Id =1,
              Title = "Test",
              Approved = true,
              Last = false,
            },
                new()
                {
                    Id=2,
                    Title= "Test",
                    Approved= true,
                    Last= true
                }
            });

            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);


            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ApiResult>(result);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_DeleteFinancialFormulaRequest_ReturnsFalseSaveChangesApiResult()
        {
            // Arrange
            var request = new DeleteFinancialFormulaRequest { Id = 1 };

            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            DeleteFinancialFormulaRequestHandler _handler = new DeleteFinancialFormulaRequestHandler(collection.UnitOfWork.Object);

            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet(new List<Core.Entities.FinancialFormula>() { new()
            {
              Id =1,
              Title = "Test",
              Approved = true,
              Last = false,
            },
                new()
                {
                    Id=2,
                    Title= "Test",
                    Approved= true,
                    Last= true
                }
            });

            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(-1);


            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ApiResult>(result);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_DeleteFinancialFormulaRequest_ReturnsNotApprovedApiResult()
        {
            // Arrange
            var request = new DeleteFinancialFormulaRequest { Id = 1 };

            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            DeleteFinancialFormulaRequestHandler _handler = new DeleteFinancialFormulaRequestHandler(collection.UnitOfWork.Object);

            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet(new List<Core.Entities.FinancialFormula>() { new()
            {
              Id =1,
              Title = "Test",
              Approved = false,
              Last = false,
            },
                new()
                {
                    Id=2,
                    Title= "Test",
                    Approved= true,
                    Last= true
                }
            });

            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);


            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ApiResult>(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_DeleteFinancialFormulaRequest_ReturnsNotFoundApiResult()
        {
            // Arrange
            var request = new DeleteFinancialFormulaRequest { Id = 10 };

            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            DeleteFinancialFormulaRequestHandler _handler = new DeleteFinancialFormulaRequestHandler(collection.UnitOfWork.Object);

            collection.Context.Setup(x => x.FinancialFormulas).ReturnsDbSet(new List<Core.Entities.FinancialFormula>() { new()
            {
              Id =1,
              Title = "Test",
              Approved = false,
              Last = false,
            },
                new()
                {
                    Id=2,
                    Title= "Test",
                    Approved= true,
                    Last= true
                }
            });

            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);


            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ApiResult>(result);
            Assert.False(result.IsSuccess);
        }
    }
}
