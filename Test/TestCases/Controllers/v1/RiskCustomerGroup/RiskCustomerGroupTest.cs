using Api.Controllers.v1;
using Application.Services.RiskCustomerGroupService;
using Core.GenericResultModel;
using Core.ViewModel.RiskCustomerGroup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.RiskCustomerGroup
{
    public class RiskCustomerGroupTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<RiskCustomerGroupVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<RiskCustomerGroupVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddRiskCustomerGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddRiskCustomerGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var RiskCustomerGroupController = new RiskCustomerGroupController(mediator.Object);
            var addRiskCustomerGroupReq = new AddRiskCustomerGroupRequest();

            var result = await RiskCustomerGroupController.Add(addRiskCustomerGroupReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteRiskCustomerGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteRiskCustomerGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var RiskCustomerGroupController = new RiskCustomerGroupController(mediator.Object);
            var deleteRiskCustomerGroupReq = new DeleteRiskCustomerGroupRequest();

            var result = await RiskCustomerGroupController.Delete(deleteRiskCustomerGroupReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateRiskCustomerGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateRiskCustomerGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var RiskCustomerGroupController = new RiskCustomerGroupController(mediator.Object);
            var updateRiskCustomerGroupReq = new UpdateRiskCustomerGroupRequest();

            var result = await RiskCustomerGroupController.Update(updateRiskCustomerGroupReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchRiskCustomerGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchRiskCustomerGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var RiskCustomerGroupController = new RiskCustomerGroupController(mediator.Object);
            var searchRiskCustomerGroupReq = new SearchRiskCustomerGroupRequest();

            var result = await RiskCustomerGroupController.Search(searchRiskCustomerGroupReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
