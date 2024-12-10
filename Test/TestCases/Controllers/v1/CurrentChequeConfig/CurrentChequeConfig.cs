using Api.Controllers.v1;
using Application.Services.CurrentChequeConfigService;
using Core.GenericResultModel;
using Core.ViewModel.CurrentChequeConfig;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CurrentChequeConfig
{
    public class CurrentChequeConfigControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<CurrentChequeConfigVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<CurrentChequeConfigVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddCurrentChequeConfigTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddCurrentChequeConfigRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CurrentChequeConfigController = new CurrentChequeConfigController(mediator.Object);
            var addCurrncyReq = new AddCurrentChequeConfigRequest();

            var result = await CurrentChequeConfigController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCurrentChequeConfigTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteCurrentChequeConfigRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CurrentChequeConfigController = new CurrentChequeConfigController(mediator.Object);
            var deleteCurrncyReq = new DeleteCurrentChequeConfigRequest();

            var result = await CurrentChequeConfigController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCurrentChequeConfigTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCurrentChequeConfigRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CurrentChequeConfigController = new CurrentChequeConfigController(mediator.Object);
            var updateCurrncyReq = new UpdateCurrentChequeConfigRequest();

            var result = await CurrentChequeConfigController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCurrentChequeConfigTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCurrentChequeConfigRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var CurrentChequeConfigController = new CurrentChequeConfigController(mediator.Object);
            var getCurrncyReq = new GetCurrentChequeConfigRequest();

            var result = await CurrentChequeConfigController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchCurrentChequeConfigTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchCurrentChequeConfigRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var CurrentChequeConfigController = new CurrentChequeConfigController(mediator.Object);
            var searchCurrncyReq = new SearchCurrentChequeConfigRequest();

            var result = await CurrentChequeConfigController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
