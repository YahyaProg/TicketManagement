using Api.Controllers.v1;
using Application.Services.CollateralOprChecklistService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.CollateralOprChecklist;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CollateralOprChecklist
{
    public class CollateralOprChecklistControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<CollateralOprChecklistVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<CollateralOprChecklistVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddCollateralOprChecklistTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddCollateralOprChecklistRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CollateralOprChecklistController = new CollateralOprChecklistController(mediator.Object);

            var addCurrncyReq = new AddCollateralOprChecklistRequest();

            var result = await CollateralOprChecklistController.Add(addCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownCollateralOprChecklistTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownCollateralOprChecklistRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var CollateralOprChecklistController = new CollateralOprChecklistController(mediator.Object);

            var dropDownCollateralOprChecklistReq = new DropDownCollateralOprChecklistRequest();

            var result = await CollateralOprChecklistController.DropDown(dropDownCollateralOprChecklistReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetCollateralOprChecklistTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCollateralOprChecklistRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var CollateralOprChecklistController = new CollateralOprChecklistController(mediator.Object);

            var getCurrncyReq = new GetCollateralOprChecklistRequest();

            var result = await CollateralOprChecklistController.Get(getCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchCollateralOprChecklistTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchCollateralOprChecklistRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var CollateralOprChecklistController = new CollateralOprChecklistController(mediator.Object);

            var searchCurrncyReq = new SearchCollateralOprChecklistRequest();

            var result = await CollateralOprChecklistController.Search(searchCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCollateralOprChecklistTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCollateralOprChecklistRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CollateralOprChecklistController = new CollateralOprChecklistController(mediator.Object);

            var updateCurrncyReq = new UpdateCollateralOprChecklistRequest();

            var result = await CollateralOprChecklistController.Update(updateCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCollateralOprChecklistTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteCollateralOprChecklistRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CollateralOprChecklistController = new CollateralOprChecklistController(mediator.Object);

            var deleteCurrncyReq = new DeleteCollateralOprChecklistRequest();

            var result = await CollateralOprChecklistController.Delete(deleteCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
