using Api.Controllers.v1.CustomerRequests;
using Application.Services.CustomerRequestService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.CustomerRequestPage;
using Core.ViewModel.CustomerRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CustomerRequest
{
    public class CustomerRequestControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult<AddProposalAndCustomerReqRequestVM> successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<CustomerRequestCollateralVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult updateSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddCustomerRequestsTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddProposalAndCustomerReqRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CustomerRequestsController = new CustomerRequestsController(mediator.Object);

            var addCurrncyReq = new AddProposalAndCustomerReqRequest();

            var result = await CustomerRequestsController.AddCustomerNeedsDescription(addCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCustomerRequestsTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCustomerRequestCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var CustomerRequestsController = new CustomerRequestsController(mediator.Object);

            var getCurrncyReq = new GetCustomerRequestCollateralRequest();

            var result = await CustomerRequestsController.GetCustomerRequestCollateral(getCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }



        [Fact]
        public async Task UpdateCustomerRequestsTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCustomerRequestCollateralRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(updateSuccessRes);

            var CustomerRequestsController = new CustomerRequestsController(mediator.Object);

            var updateCurrncyReq = new UpdateCustomerRequestCollateralRequest();

            var result = await CustomerRequestsController.UpdateCustomerRequestCollateral(updateCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }


    }
}
