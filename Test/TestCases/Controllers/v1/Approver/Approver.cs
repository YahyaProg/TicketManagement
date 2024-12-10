using Api.Controllers.v1;
using Application.Services.ApproverService;
using Core.GenericResultModel;
using Core.ViewModel.Approver;
using Core.ViewModel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.Approver
{
    public class ApproverControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<ApproverVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<ApproverVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };
        [Fact]
        public async Task AddApproverTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddApproverRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var approverController = new ApproverController(mediator.Object);
            var addCurrncyReq = new AddApproverRequest();

            var result = await approverController.Add(addCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteApproverTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteApproverRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ApproverController = new ApproverController(mediator.Object);
            var deleteCurrncyReq = new DeleteApproverRequest();

            var result = await ApproverController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownApproverTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownApproverRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var ApproverController = new ApproverController(mediator.Object);

            var dropDownApproverReq = new DropDownApproverRequest();

            var result = await ApproverController.DropDown(dropDownApproverReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateApproverTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateApproverRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ApproverController = new ApproverController(mediator.Object);
            var updateCurrncyReq = new UpdateApproverRequest();

            var result = await ApproverController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetApproverTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetApproverRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var ApproverController = new ApproverController(mediator.Object);
            var getCurrncyReq = new GetApproverRequest();

            var result = await ApproverController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchApproverTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchApproverRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var ApproverController = new ApproverController(mediator.Object);
            var searchCurrncyReq = new SearchApproverRequest();

            var result = await ApproverController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
