using Api.Controllers.v1;
using Application.Services.BranchService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.Branch;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.Branch
{
    public class BranchControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<BranchVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<BranchVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddBranchTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddBranchRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BranchController = new BranchsController(mediator.Object);
            var addCurrncyReq = new AddBranchRequest();

            var result = await BranchController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteBranchTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteBranchRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BranchController = new BranchsController(mediator.Object);
            var deleteCurrncyReq = new DeleteBranchRequest();

            var result = await BranchController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownBranchTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownBranchRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var BranchController = new BranchsController(mediator.Object);

            var dropDownBranchReq = new DropDownBranchRequest();

            var result = await BranchController.DropDown(dropDownBranchReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateBranchTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateBranchRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var BranchController = new BranchsController(mediator.Object);
            var updateCurrncyReq = new UpdateBranchRequest();

            var result = await BranchController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBranchTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetBranchRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var BranchController = new BranchsController(mediator.Object);
            var getCurrncyReq = new GetBranchRequest();

            var result = await BranchController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchBranchTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchBranchRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var BranchController = new BranchsController(mediator.Object);
            var searchCurrncyReq = new SearchBranchRequest();

            var result = await BranchController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
