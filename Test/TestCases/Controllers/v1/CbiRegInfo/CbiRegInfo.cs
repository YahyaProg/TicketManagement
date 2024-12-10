using Api.Controllers.v1;
using Application.Services.CbiRegInfoService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.CbiRegInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CbiRegInfo
{
    public class CbiRegInfo
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<CbiRegInfoVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<CbiRegInfoSearchVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> dropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddCbiRegInfoTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddCbiRegInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CbiRegInfoController = new CbiRegInfoController(mediator.Object);
            var addCbiRegInfoReq = new AddCbiRegInfoRequest();

            var result = await CbiRegInfoController.Add(addCbiRegInfoReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DeleteCbiRegInfoTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteCbiRegInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CbiRegInfoController = new CbiRegInfoController(mediator.Object);
            var deleteCbiRegInfoReq = new DeleteCbiRegInfoRequest();

            var result = await CbiRegInfoController.Delete(deleteCbiRegInfoReq);

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task UpdateCbiRegInfoTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCbiRegInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CbiRegInfoController = new CbiRegInfoController(mediator.Object);
            var updateCbiRegInfoReq = new UpdateCbiRegInfoRequest();


            var result = await CbiRegInfoController.Update(updateCbiRegInfoReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCbiRegInfoTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCbiRegInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var CbiRegInfoController = new CbiRegInfoController(mediator.Object);
            var getCbiRegInfoReq = new GetCbiRegInfoRequest();

            var result = await CbiRegInfoController.Get(getCbiRegInfoReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchCbiRegInfoTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AdvanceSearchCbiRegInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var CbiRegInfoController = new CbiRegInfoController(mediator.Object);

            var searchCbiRegInfoReq = new AdvanceSearchCbiRegInfoRequest();

            var result = await CbiRegInfoController.Search(searchCbiRegInfoReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DropDownCbiRegInfoTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownSearchCbiRegInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(dropDownSuccessRes);
            var CbiRegInfoController = new CbiRegInfoController(mediator.Object);
            var dropDownCbiRegInfoReq = new DropDownSearchCbiRegInfoRequest();

            var result = await CbiRegInfoController.DropDownSearch(dropDownCbiRegInfoReq);

            Assert.IsType<OkObjectResult>(result);

        }
    }
}
