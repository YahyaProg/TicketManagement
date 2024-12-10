using Application.Services.BaseService;
using Application.Services.StocksCollateralService;
using Core.Entities;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Helper;

namespace Test.TestCases.Services.StocksCollateralTest
{
    public class StocksCollateralUpdateTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task UpdateStockCollateral_Test(int type)
        {

            // Arrange
            var mediator = new Mock<IMediator>();
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            var handler = new UpdateStocksCollateralItemRequestHandler(collection.Context.Object, mediator.Object);

            var request = new UpdateStocksCollateralItemRequest()
            {
                Id = 1,
                BourseSymbolId = 1,
                Bazargani = "yes",
                BirthDate = DateTime.Now,
                CorpId = "safsfa",
                RelationType = type == 1 ? Core.Enums.EStocksCollateralItem_relationType.Direct : Core.Enums.EStocksCollateralItem_relationType.Indirect // 2 = direct
            };

            collection.Context.Setup(x => x.StocksCollateralItems).ReturnsDbSet([
                new StocksCollateralItem(){Id = type != 2 ? 1 : 0}
                ]);

            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            mediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(new Core.GenericResultModel.ApiResult<long>()
            {
                IsSuccess = type != 3 ? true : false,
                Data = 1
            });

            // Act
            var result = await handler.Handle(request, CancellationToken.None);


            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddStockCollateral_Test()
        {
            var mediator = new Mock<IMediator>();
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();
            var handler = new AddStocksCollateralItemRequestHandler(collection.Context.Object, mediator.Object);

            mediator.Setup(x => x.Send(It.IsAny<GetCustomerIdRequest>(), CancellationToken.None)).ReturnsAsync(new Core.GenericResultModel.ApiResult<long>()
            {
                IsSuccess = true,
                Data = 1
            });

            collection.Context.Setup(x => x.StocksCollateralItems).ReturnsDbSet([]);

            collection.Context.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);


            // act
            var result = await handler.Handle(new() { CorpId = "123", RelationType = Core.Enums.EStocksCollateralItem_relationType.Indirect }, CancellationToken.None);


            // assert

            Assert.True(result.IsSuccess);
        }
    }
}
