using Api.Controllers.v1;
using Application.Services.TradingCompanyActivityService;
using Core.GenericResultModel;
using Core.ViewModel.TradingCompanyActivity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.TradingCompanyActivity
{
    public class TradingCompanyActivityControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<TradingCompanyActivityVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<TradingCompanyActivityVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddTradingCompanyActivityTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddTradingCompanyActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var TradingCompanyActivityController = new TradingCompanyActivityController(mediator.Object);
            var addCurrncyReq = new AddTradingCompanyActivityRequest();

            var result = await TradingCompanyActivityController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteTradingCompanyActivityTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteTradingCompanyActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var TradingCompanyActivityController = new TradingCompanyActivityController(mediator.Object);
            var deleteCurrncyReq = new DeleteTradingCompanyActivityRequest();

            var result = await TradingCompanyActivityController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateTradingCompanyActivityTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateTradingCompanyActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var TradingCompanyActivityController = new TradingCompanyActivityController(mediator.Object);
            var updateCurrncyReq = new UpdateTradingCompanyActivityRequest();

            var result = await TradingCompanyActivityController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetTradingCompanyActivityTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetTradingCompanyActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var TradingCompanyActivityController = new TradingCompanyActivityController(mediator.Object);
            var getCurrncyReq = new GetTradingCompanyActivityRequest();

            var result = await TradingCompanyActivityController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchTradingCompanyActivityTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchTradingCompanyActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var TradingCompanyActivityController = new TradingCompanyActivityController(mediator.Object);
            var searchCurrncyReq = new SearchTradingCompanyActivityRequest();

            var result = await TradingCompanyActivityController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
