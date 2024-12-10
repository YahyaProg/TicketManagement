using Api.Controllers.v1;
using Application.Services.CalculationIntervalService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.CalculationIntervalModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CalculationInterval
{
    public class CalculationIntervalTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<CalculationIntervalVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<CalculationIntervalVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddCalculationIntervalTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddCalculationIntervalRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CalculationIntervalController = new CalculationIntervalsController(mediator.Object);
            var addCurrncyReq = new AddCalculationIntervalRequest();

            var result = await CalculationIntervalController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCalculationIntervalTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteCalculationIntervalRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CalculationIntervalController = new CalculationIntervalsController(mediator.Object);
            var deleteCurrncyReq = new DeleteCalculationIntervalRequest();

            var result = await CalculationIntervalController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownCalculationIntervalTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownCalculationIntervalRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var CalculationIntervalController = new CalculationIntervalsController(mediator.Object);

            var dropDownCalculationIntervalReq = new DropDownCalculationIntervalRequest();

            var result = await CalculationIntervalController.DropDown(dropDownCalculationIntervalReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateCalculationIntervalTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCalculationIntervalRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CalculationIntervalController = new CalculationIntervalsController(mediator.Object);
            var updateCurrncyReq = new UpdateCalculationIntervalRequest();

            var result = await CalculationIntervalController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCalculationIntervalTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCalculationIntervalRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var CalculationIntervalController = new CalculationIntervalsController(mediator.Object);
            var getCurrncyReq = new GetCalculationIntervalRequest();

            var result = await CalculationIntervalController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchCalculationIntervalTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchCalculationIntervalRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var CalculationIntervalController = new CalculationIntervalsController(mediator.Object);
            var searchCurrncyReq = new SearchCalculationIntervalRequest();

            var result = await CalculationIntervalController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
