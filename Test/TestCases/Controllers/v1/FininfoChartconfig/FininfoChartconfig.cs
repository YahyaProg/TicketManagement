using Api.Controllers.v1;
using Application.Services.FininfoChartconfigconfigService;
using Application.Services.FininfoChartconfigService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.FininfoChartconfig;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.FininfoChartconfigconfig
{
    public class FininfoChartconfigconfigControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<FininfoChartconfigVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<FininfoChartconfigVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddFininfoChartconfigTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddFininfoChartconfigRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var FininfoChartconfigController = new FininfoChartconfigController(mediator.Object);

            var addCurrncyReq = new AddFininfoChartconfigRequest();

            var result = await FininfoChartconfigController.Add(addCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownFininfoChartconfigTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownFininfoChartconfigRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var FininfoChartconfigController = new FininfoChartconfigController(mediator.Object);

            var dropDownFininfoChartconfigReq = new DropDownFininfoChartconfigRequest();

            var result = await FininfoChartconfigController.DropDown(dropDownFininfoChartconfigReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetFininfoChartconfigTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetFininfoChartconfigRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var FininfoChartconfigController = new FininfoChartconfigController(mediator.Object);

            var getCurrncyReq = new GetFininfoChartconfigRequest();

            var result = await FininfoChartconfigController.Get(getCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchFininfoChartconfigTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchFininfoChartconfigRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var FininfoChartconfigController = new FininfoChartconfigController(mediator.Object);

            var searchCurrncyReq = new SearchFininfoChartconfigRequest();

            var result = await FininfoChartconfigController.Search(searchCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateFininfoChartconfigTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateFininfoChartconfigRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var FininfoChartconfigController = new FininfoChartconfigController(mediator.Object);

            var updateCurrncyReq = new UpdateFininfoChartconfigRequest();

            var result = await FininfoChartconfigController.Update(updateCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteFininfoChartconfigTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteFininfoChartconfigRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var FininfoChartconfigController = new FininfoChartconfigController(mediator.Object);

            var deleteCurrncyReq = new DeleteFininfoChartconfigRequest();

            var result = await FininfoChartconfigController.Delete(deleteCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
