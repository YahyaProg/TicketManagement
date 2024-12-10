using Api.Controllers.v1;
using Application.Services.BgcollateralService;
using Core.GenericResultModel;
using Core.ViewModel.Bgcollateral;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.Bgcollateral
{
    public class BgcollateralControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<BgcollateralVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<BgcollateralVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddBgcollateralTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddBgcollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BgcollateralController = new BgcollateralController(mediator.Object);
            var addCurrncyReq = new AddBgcollateralRequest();

            var result = await BgcollateralController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteBgcollateralTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteBgcollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BgcollateralController = new BgcollateralController(mediator.Object);
            var deleteCurrncyReq = new DeleteBgcollateralRequest();

            var result = await BgcollateralController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateBgcollateralTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateBgcollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BgcollateralController = new BgcollateralController(mediator.Object);
            var updateCurrncyReq = new UpdateBgcollateralRequest();

            var result = await BgcollateralController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBgcollateralTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetBgcollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var BgcollateralController = new BgcollateralController(mediator.Object);
            var getCurrncyReq = new GetBgcollateralRequest();

            var result = await BgcollateralController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchBgcollateralTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchBgcollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var BgcollateralController = new BgcollateralController(mediator.Object);
            var searchCurrncyReq = new SearchBgcollateralRequest();

            var result = await BgcollateralController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
