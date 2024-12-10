using Api.Controllers.v1;
using Application.Services.BrokersActivityService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.BrokersActivity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.BrokersActivity
{
    public class BrokersActivityControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<BrokersActivityVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<BrokersActivityVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddBrokersActivityTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddBrokersActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BrokersActivityController = new BrokersActivityController(mediator.Object);

            var addCurrncyReq = new AddBrokersActivityRequest();

            var result = await BrokersActivityController.Add(addCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownBrokersActivityTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownBrokersActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var BrokersActivityController = new BrokersActivityController(mediator.Object);

            var dropDownBrokersActivityReq = new DropDownBrokersActivityRequest();

            var result = await BrokersActivityController.DropDown(dropDownBrokersActivityReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetBrokersActivityTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetBrokersActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var BrokersActivityController = new BrokersActivityController(mediator.Object);

            var getCurrncyReq = new GetBrokersActivityRequest();

            var result = await BrokersActivityController.Get(getCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchBrokersActivityTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchBrokersActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var BrokersActivityController = new BrokersActivityController(mediator.Object);

            var searchCurrncyReq = new SearchBrokersActivityRequest();

            var result = await BrokersActivityController.Search(searchCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateBrokersActivityTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateBrokersActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BrokersActivityController = new BrokersActivityController(mediator.Object);

            var updateCurrncyReq = new UpdateBrokersActivityRequest();

            var result = await BrokersActivityController.Update(updateCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteBrokersActivityTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteBrokersActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BrokersActivityController = new BrokersActivityController(mediator.Object);

            var deleteCurrncyReq = new DeleteBrokersActivityRequest();

            var result = await BrokersActivityController.Delete(deleteCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
