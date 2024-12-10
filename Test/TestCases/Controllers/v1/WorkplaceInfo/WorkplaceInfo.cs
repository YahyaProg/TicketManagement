using Api.Controllers.v1;
using Application.Services.WorkplaceInfoService;
using Core.GenericResultModel;
using MediatR;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.WorkplaceInfo
{
    public class WorkplaceInfoControllerTesst
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<WorkplaceInfoVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddWorkplaceInfoTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddWorkplaceInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var WorkplaceInfoController = new WorkplaceInfoController(mediator.Object);

            var addWorkplaceInfoReq = new AddWorkplaceInfoRequest();

            var result = await WorkplaceInfoController.Add(addWorkplaceInfoReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteWorkplaceInfoTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteWorkplaceInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var WorkplaceInfoController = new WorkplaceInfoController(mediator.Object);

            var deleteWorkplaceInfoReq = new DeleteWorkplaceInfoRequest();

            var result = await WorkplaceInfoController.Delete(deleteWorkplaceInfoReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchWorkplaceInfoTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchWorkplaceInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var WorkplaceInfoController = new WorkplaceInfoController(mediator.Object);

            var searchWorkplaceInfoReq = new SearchWorkplaceInfoRequest();

            var result = await WorkplaceInfoController.Search(searchWorkplaceInfoReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateWorkplaceInfoTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateWorkplaceInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var WorkplaceInfoController = new WorkplaceInfoController(mediator.Object);

            var updateWorkplaceInfoReq = new UpdateWorkplaceInfoRequest();

            var result = await WorkplaceInfoController.Update(updateWorkplaceInfoReq);

            Assert.IsType<OkObjectResult>(result);
        }

    }
}
