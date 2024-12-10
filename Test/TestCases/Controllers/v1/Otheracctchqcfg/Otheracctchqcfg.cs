using Api.Controllers.v1;
using Application.Services.OtheracctchqcfgService;
using Core.GenericResultModel;
using Core.ViewModel.Otheracctchqcfg;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.Otheracctchqcfg
{
    public class OtheracctchqcfgControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<OtheracctchqcfgVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<OtheracctchqcfgVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddOtheracctchqcfgTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddOtheracctchqcfgRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var OtheracctchqcfgController = new OtheracctchqcfgController(mediator.Object);
            var addCurrncyReq = new AddOtheracctchqcfgRequest();

            var result = await OtheracctchqcfgController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteOtheracctchqcfgTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteOtheracctchqcfgRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var OtheracctchqcfgController = new OtheracctchqcfgController(mediator.Object);
            var deleteCurrncyReq = new DeleteOtheracctchqcfgRequest();

            var result = await OtheracctchqcfgController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateOtheracctchqcfgTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateOtheracctchqcfgRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var OtheracctchqcfgController = new OtheracctchqcfgController(mediator.Object);
            var updateCurrncyReq = new UpdateOtheracctchqcfgRequest();

            var result = await OtheracctchqcfgController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetOtheracctchqcfgTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetOtheracctchqcfgRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var OtheracctchqcfgController = new OtheracctchqcfgController(mediator.Object);
            var getCurrncyReq = new GetOtheracctchqcfgRequest();

            var result = await OtheracctchqcfgController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchOtheracctchqcfgTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchOtheracctchqcfgRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var OtheracctchqcfgController = new OtheracctchqcfgController(mediator.Object);
            var searchCurrncyReq = new SearchOtheracctchqcfgRequest();

            var result = await OtheracctchqcfgController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
