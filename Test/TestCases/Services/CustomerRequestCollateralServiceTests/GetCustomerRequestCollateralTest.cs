using Application.Services.CustomerRequestService;
using Core.GenericResultModel;
using Core.ViewModel.CustomerRequestPage;
using Infrastructure;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.CustomerRequestCollateralServiceTests
{
    public class GetCustomerRequestCollateralTest
    {
        private readonly GetCustomerRequestCollateralRequest request = new() { ProposalSchemeId = 1 };

        [Fact]
        public async Task ProposalScheme_NotFound()
        {
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet(new List<Core.Entities.ProposalScheme>());

            var res = await Test(collection.UnitOfWork.Object);

            Assert.False(res.IsSuccess);
            Assert.Equal(404, res.Code);
        }

        [Fact]
        public async Task CustomerRequest_NotFound()
        {
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet(new List<Core.Entities.ProposalScheme>() { new() { Id = request.ProposalSchemeId } });

            var res = await Test(collection.UnitOfWork.Object);

            Assert.False(res.IsSuccess);
            Assert.Equal(404, res.Code);
        }

        [Fact]
        public async Task Success()
        {
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet(
                new List<Core.Entities.ProposalScheme>()
                {
                    new()
                    {
                        Id = request.ProposalSchemeId,
                        CustomerRequest = new()
                    }
                });

            var res = await Test(collection.UnitOfWork.Object);

            Assert.True(res.IsSuccess);
            Assert.NotNull(res.Data);
        }

        private async Task<ApiResult<CustomerRequestCollateralVM>> Test(IUnitOfWork unitOfWork)
        {
            var handler = new GetCustomerRequestCollateralRequestHandler(unitOfWork);

            var res = await handler.Handle(request, CancellationToken.None);

            return res;
        }
    }
}
