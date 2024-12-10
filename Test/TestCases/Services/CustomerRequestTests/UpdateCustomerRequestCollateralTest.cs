using Application.Services.CustomerRequestService;
using Core.Entities;
using Core.GenericResultModel;
using Infrastructure;
using Moq;
using System.Linq.Expressions;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.CustomerRequestTests
{
    public class UpdateCustomerRequestCollateralTest
    {
        private readonly UpdateCustomerRequestCollateralRequest request = new()
        {
            CollateralDesc = "",
            Pishnahad = DateTime.Now,
            ProposalSchemeId = 1,
            Request = ""
        };

        [Fact]
        public async Task ProposalNotFound()
        {
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet(new List<Core.Entities.ProposalScheme>() { new() { Id = 0 } });

            var res = await Test(collection.UnitOfWork.Object);

            Assert.NotNull(res);
            Assert.False(res.IsSuccess);
            Assert.Equal(404, res.Code);
        }

        [Fact]
        public async Task CustomerRequestNotFound()
        {
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet(new List<Core.Entities.ProposalScheme>() { new() { Id = 1 } });

            var res = await Test(collection.UnitOfWork.Object);

            Assert.NotNull(res);
            Assert.False(res.IsSuccess);
            Assert.Equal(404, res.Code);
        }

        [Fact]
        public async Task Update_Failed()
        {
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet(new List<Core.Entities.ProposalScheme>() { new() { Id = 1, CustomerRequest = new() } });
            collection.Context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

            var res = await Test(collection.UnitOfWork.Object);

            Assert.NotNull(res);
            Assert.False(res.IsSuccess);
            Assert.Equal(400, res.Code);
        }
        
        [Fact]
        public async Task Update_Success()
        {
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet(new List<Core.Entities.ProposalScheme>() { new() { Id = 1, CustomerRequest = new() } });
            collection.Context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(2);

            var res = await Test(collection.UnitOfWork.Object);

            Assert.NotNull(res);
            Assert.True(res.IsSuccess);
        }

        private async Task<ApiResult> Test(IUnitOfWork unitOfWork)
        {
            var handler = new UpdateCustomerRequestCollateralRequestHandler(unitOfWork);

            var res = await handler.Handle(request, CancellationToken.None);

            return res;
        }
    }
}
