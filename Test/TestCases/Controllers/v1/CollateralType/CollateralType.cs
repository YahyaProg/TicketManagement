using Api.Controllers.v1;
using Application.Services.CollateralTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.CollateralType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CollateralType
{
    public class CollateralTypeControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<CollateralTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<CollateralTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddCollateralTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddCollateralTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CollateralTypeController = new CollateralTypeController(mediator.Object);

            var addCollateralTypeReq = new AddCollateralTypeRequest();

            var result = await CollateralTypeController.Add(addCollateralTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCollateralTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCollateralTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var CollateralTypeController = new CollateralTypeController(mediator.Object);

            var getCollateralTypeReq = new GetCollateralTypeRequest();

            var result = await CollateralTypeController.Get(getCollateralTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchCollateralTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchCollateralTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var CollateralTypeController = new CollateralTypeController(mediator.Object);

            var searchCollateralTypeReq = new SearchCollateralTypeRequest();

            var result = await CollateralTypeController.Search(searchCollateralTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DropDownCollateralTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownCollateralTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var CollateralTypeController = new CollateralTypeController(mediator.Object);

            var dropDownCollateralTypeReq = new DropDownCollateralTypeRequest();

            var result = await CollateralTypeController.DropDown(dropDownCollateralTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCollateralTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCollateralTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CollateralTypeController = new CollateralTypeController(mediator.Object);

            var updateCollateralTypeReq = new UpdateCollateralTypeRequest();

            var result = await CollateralTypeController.Update(updateCollateralTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCollateralTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteCollateralTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CollateralTypeController = new CollateralTypeController(mediator.Object);

            var deleteCollateralTypeReq = new DeleteCollateralTypeRequest();

            var result = await CollateralTypeController.Delete(deleteCollateralTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
