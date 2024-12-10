using Api.Controllers.v1;
using Application.Services.IntervalTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.IntervalType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.IntervalType
{
    public class IntervalTypeControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<IntervalTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<IntervalTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddIntervalTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddIntervalTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var IntervalTypeController = new IntervalTypeController(mediator.Object);
            var addCurrncyReq = new AddIntervalTypeRequest();

            var result = await IntervalTypeController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteIntervalTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteIntervalTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var IntervalTypeController = new IntervalTypeController(mediator.Object);
            var deleteCurrncyReq = new DeleteIntervalTypeRequest();

            var result = await IntervalTypeController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownIntervalTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownIntervalTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var IntervalTypeController = new IntervalTypeController(mediator.Object);

            var dropDownIntervalTypeReq = new DropDownIntervalTypeRequest();

            var result = await IntervalTypeController.DropDown(dropDownIntervalTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateIntervalTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateIntervalTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var IntervalTypeController = new IntervalTypeController(mediator.Object);
            var updateCurrncyReq = new UpdateIntervalTypeRequest();

            var result = await IntervalTypeController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetIntervalTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetIntervalTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var IntervalTypeController = new IntervalTypeController(mediator.Object);
            var getCurrncyReq = new GetIntervalTypeRequest();

            var result = await IntervalTypeController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchIntervalTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchIntervalTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var IntervalTypeController = new IntervalTypeController(mediator.Object);
            var searchCurrncyReq = new SearchIntervalTypeRequest();

            var result = await IntervalTypeController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
