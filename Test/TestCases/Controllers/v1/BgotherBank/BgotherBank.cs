using Api.Controllers.v1;
using Application.Services.BgotherBankService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.BgotherBank;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.BgotherBank
{
    public class BgotherBgotherBankControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<BgotherBankVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<BgotherBankVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddBgotherBankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddBgotherBankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BgotherBankController = new BgotherBankController(mediator.Object);
            var addCurrncyReq = new AddBgotherBankRequest();

            var result = await BgotherBankController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteBgotherBankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteBgotherBankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BgotherBankController = new BgotherBankController(mediator.Object);
            var deleteCurrncyReq = new DeleteBgotherBankRequest();

            var result = await BgotherBankController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
      
        [Fact]
        public async Task UpdateBgotherBankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateBgotherBankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BgotherBankController = new BgotherBankController(mediator.Object);
            var updateCurrncyReq = new UpdateBgotherBankRequest();

            var result = await BgotherBankController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBgotherBankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetBgotherBankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var BgotherBankController = new BgotherBankController(mediator.Object);
            var getCurrncyReq = new GetBgotherBankRequest();

            var result = await BgotherBankController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchBgotherBankTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchBgotherBankRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var BgotherBankController = new BgotherBankController(mediator.Object);
            var searchCurrncyReq = new SearchBgotherBankRequest();

            var result = await BgotherBankController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
