using Api.Controllers.v1;
using Application.Services.CreditPeriodService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.CreditPeriod;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CreditPeriod
{
    public class CreditPeriodControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<CreditPeriodVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<CreditPeriodVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddCreditPeriodTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddCreditPeriodRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var creditPeriodController = new CreditPeriodController(mediator.Object);
            var addCurrncyReq = new AddCreditPeriodRequest();

            var result = await creditPeriodController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCreditPeriodTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteCreditPeriodRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CreditPeriodController = new CreditPeriodController(mediator.Object);
            var deleteCurrncyReq = new DeleteCreditPeriodRequest();

            var result = await CreditPeriodController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownCreditPeriodTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownCreditPeriodRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var CreditPeriodController = new CreditPeriodController(mediator.Object);

            var dropDownCreditPeriodReq = new DropDownCreditPeriodRequest();

            var result = await CreditPeriodController.DropDown(dropDownCreditPeriodReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateCreditPeriodTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCreditPeriodRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CreditPeriodController = new CreditPeriodController(mediator.Object);
            var updateCurrncyReq = new UpdateCreditPeriodRequest();

            var result = await CreditPeriodController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCreditPeriodTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCreditPeriodRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var CreditPeriodController = new CreditPeriodController(mediator.Object);
            var getCurrncyReq = new GetCreditPeriodRequest();

            var result = await CreditPeriodController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchCreditPeriodTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchCreditPeriodRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var CreditPeriodController = new CreditPeriodController(mediator.Object);
            var searchCurrncyReq = new SearchCreditPeriodRequest();

            var result = await CreditPeriodController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}

