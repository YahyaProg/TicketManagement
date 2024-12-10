using Api.Controllers.v1;
using Application.Services.LoanTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.LoanType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.LoanType
{
    public class LoanTypeControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<LoanTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<LoanTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddLoanTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddLoanTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var LoanTypeController = new LoanTypeController(mediator.Object);

            var addLoanTypeReq = new AddLoanTypeRequest();

            var result = await LoanTypeController.Add(addLoanTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetLoanTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetLoanTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var LoanTypeController = new LoanTypeController(mediator.Object);

            var getLoanTypeReq = new GetLoanTypeRequest();

            var result = await LoanTypeController.Get(getLoanTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchLoanTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchLoanTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var LoanTypeController = new LoanTypeController(mediator.Object);

            var searchLoanTypeReq = new SearchLoanTypeRequest();

            var result = await LoanTypeController.Search(searchLoanTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DropDownLoanTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownLoanTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var LoanTypeController = new LoanTypeController(mediator.Object);

            var dropDownLoanTypeReq = new DropDownLoanTypeRequest();

            var result = await LoanTypeController.DropDown(dropDownLoanTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateLoanTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateLoanTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var LoanTypeController = new LoanTypeController(mediator.Object);

            var updateLoanTypeReq = new UpdateLoanTypeRequest();

            var result = await LoanTypeController.Update(updateLoanTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteLoanTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteLoanTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var LoanTypeController = new LoanTypeController(mediator.Object);

            var deleteLoanTypeReq = new DeleteLoanTypeRequest();

            var result = await LoanTypeController.Delete(deleteLoanTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
