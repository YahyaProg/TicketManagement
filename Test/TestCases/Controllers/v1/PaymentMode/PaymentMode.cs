using Api.Controllers.v1;
using Application.Services.PaymentModeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.PaymentMode;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.PaymentMode
{
    public class PaymentModeControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaymentModeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<PaymentModeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddPaymentModeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddPaymentModeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var PaymentModeController = new PaymentModeController(mediator.Object);
            var addCurrncyReq = new AddPaymentModeRequest();

            var result = await PaymentModeController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeletePaymentModeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeletePaymentModeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var PaymentModeController = new PaymentModeController(mediator.Object);
            var deleteCurrncyReq = new DeletePaymentModeRequest();

            var result = await PaymentModeController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownPaymentModeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownPaymentModeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var PaymentModeController = new PaymentModeController(mediator.Object);

            var dropDownPaymentModeReq = new DropDownPaymentModeRequest();

            var result = await PaymentModeController.DropDown(dropDownPaymentModeReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdatePaymentModeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdatePaymentModeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var PaymentModeController = new PaymentModeController(mediator.Object);
            var updateCurrncyReq = new UpdatePaymentModeRequest();

            var result = await PaymentModeController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetPaymentModeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetPaymentModeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var PaymentModeController = new PaymentModeController(mediator.Object);
            var getCurrncyReq = new GetPaymentModeRequest();

            var result = await PaymentModeController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchPaymentModeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchPaymentModeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var PaymentModeController = new PaymentModeController(mediator.Object);
            var searchCurrncyReq = new SearchPaymentModeRequest();

            var result = await PaymentModeController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
