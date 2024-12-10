using Api.Controllers.v1;
using Application.Services.LcSettleTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.LcSettleType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.LcSettleType
{
    public class LcSettleTypeControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<LcSettleTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<LcSettleTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddLcSettleTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddLcSettleTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var LcSettleTypeController = new LcSettleTypeController(mediator.Object);

            var addLcSettleTypeReq = new AddLcSettleTypeRequest();

            var result = await LcSettleTypeController.Add(addLcSettleTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetLcSettleTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetLcSettleTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var LcSettleTypeController = new LcSettleTypeController(mediator.Object);

            var getLcSettleTypeReq = new GetLcSettleTypeRequest();

            var result = await LcSettleTypeController.Get(getLcSettleTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchLcSettleTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchLcSettleTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var LcSettleTypeController = new LcSettleTypeController(mediator.Object);

            var searchLcSettleTypeReq = new SearchLcSettleTypeRequest();

            var result = await LcSettleTypeController.Search(searchLcSettleTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DropDownLcSettleTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownLcSettleTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var LcSettleTypeController = new LcSettleTypeController(mediator.Object);

            var dropDownLcSettleTypeReq = new DropDownLcSettleTypeRequest();

            var result = await LcSettleTypeController.DropDown(dropDownLcSettleTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateLcSettleTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateLcSettleTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var LcSettleTypeController = new LcSettleTypeController(mediator.Object);

            var updateLcSettleTypeReq = new UpdateLcSettleTypeRequest();

            var result = await LcSettleTypeController.Update(updateLcSettleTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteLcSettleTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteLcSettleTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var LcSettleTypeController = new LcSettleTypeController(mediator.Object);

            var deleteLcSettleTypeReq = new DeleteLcSettleTypeRequest();

            var result = await LcSettleTypeController.Delete(deleteLcSettleTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}

