using Api.Controllers.v1;
using Application.Services.RiskCustomerGroupItemService;
using Core.GenericResultModel;
using Core.ViewModel.RiskCustomerGroupItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.RiskCustomerGroupItem
{
    public class RiskCustomerGroupItemTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<RiskCustomerGroupItemVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<RiskCustomerGroupItemVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddRiskCustomerGroupItemTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddRiskCustomerGroupItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var RiskCustomerGroupItemController = new RiskCustomerGroupItemController(mediator.Object);
            var addRiskCustomerGroupItemReq = new AddRiskCustomerGroupItemRequest();

            var result = await RiskCustomerGroupItemController.Add(addRiskCustomerGroupItemReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteRiskCustomerGroupItemTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteRiskCustomerGroupItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var RiskCustomerGroupItemController = new RiskCustomerGroupItemController(mediator.Object);
            var deleteRiskCustomerGroupItemReq = new DeleteRiskCustomerGroupItemRequest();

            var result = await RiskCustomerGroupItemController.Delete(deleteRiskCustomerGroupItemReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateRiskCustomerGroupItemTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateRiskCustomerGroupItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var RiskCustomerGroupItemController = new RiskCustomerGroupItemController(mediator.Object);
            var updateRiskCustomerGroupItemReq = new UpdateRiskCustomerGroupItemRequest();

            var result = await RiskCustomerGroupItemController.Update(updateRiskCustomerGroupItemReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchRiskCustomerGroupItemTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchRiskCustomerGroupItemRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var RiskCustomerGroupItemController = new RiskCustomerGroupItemController(mediator.Object);
            var searchRiskCustomerGroupItemReq = new SearchRiskCustomerGroupItemRequest();

            var result = await RiskCustomerGroupItemController.Search(searchRiskCustomerGroupItemReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
