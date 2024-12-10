using Api.Controllers.v1;
using Application.Services.FininfoChartService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.FininfoChart;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.FininfoChart
{
    public class FininfoChartControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<FininfoChartVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<FininfoChartVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddFininfoChartTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddFininfoChartRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var FininfoChartController = new FininfoChartController(mediator.Object);

            var addCurrncyReq = new AddFininfoChartRequest();

            var result = await FininfoChartController.Add(addCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownFininfoChartTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownFininfoChartRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var FininfoChartController = new FininfoChartController(mediator.Object);

            var dropDownFininfoChartReq = new DropDownFininfoChartRequest();

            var result = await FininfoChartController.DropDown(dropDownFininfoChartReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetFininfoChartTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetFininfoChartRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var FininfoChartController = new FininfoChartController(mediator.Object);

            var getCurrncyReq = new GetFininfoChartRequest();

            var result = await FininfoChartController.Get(getCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchFininfoChartTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchFininfoChartRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var FininfoChartController = new FininfoChartController(mediator.Object);

            var searchCurrncyReq = new SearchFininfoChartRequest();

            var result = await FininfoChartController.Search(searchCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateFininfoChartTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateFininfoChartRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var FininfoChartController = new FininfoChartController(mediator.Object);

            var updateCurrncyReq = new UpdateFininfoChartRequest();

            var result = await FininfoChartController.Update(updateCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteFininfoChartTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteFininfoChartRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var FininfoChartController = new FininfoChartController(mediator.Object);

            var deleteCurrncyReq = new DeleteFininfoChartRequest();

            var result = await FininfoChartController.Delete(deleteCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
