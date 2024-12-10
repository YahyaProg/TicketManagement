using Api.Controllers.v1;
using Application.Services.IsicSubGroupService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.IsicSubGroup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.IsicSubGroupTests
{
    public class IsicSubGroupTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<IsicSubGroupVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<IsicSubGroupVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> dropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddIsicGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddIsicSubGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var isicSubGroupController = new IsicSubGroupController(mediator.Object);
            var addisicSubGroupReq = new AddIsicSubGroupRequest();

            var result = await isicSubGroupController.Add(addisicSubGroupReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DeleteIsicGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteIsicSubGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var isicSubGroupController = new IsicSubGroupController(mediator.Object);
            var deleteisicSubGroupReq = new DeleteIsicSubGroupRequest();

            var result = await isicSubGroupController.Delete(deleteisicSubGroupReq);

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task UpdateIsicGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateIsicSubGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var isicSubGroupController = new IsicSubGroupController(mediator.Object);
            var updateisicSubGroupReq = new UpdateIsicSubGroupRequest();


            var result = await isicSubGroupController.Update(updateisicSubGroupReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetIsicGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetIsicSubGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var isicSubGroupController = new IsicSubGroupController(mediator.Object);
            var getIsicSubGroupReq = new GetIsicSubGroupRequest();

            var result = await isicSubGroupController.Get(getIsicSubGroupReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchIsicGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchIsicSubGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var isicSubGroupController = new IsicSubGroupController(mediator.Object);

            var searchIsicSubGroupReq = new SearchIsicSubGroupRequest();

            var result = await isicSubGroupController.Search(searchIsicSubGroupReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DropDownIsicSubGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownSearchIsicSubGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(dropDownSuccessRes);
            var isicSubGroupController = new IsicSubGroupController(mediator.Object);
            var dropDownIsicSubGroupReq = new DropDownSearchIsicSubGroupRequest();

            var result = await isicSubGroupController.DropDownSearch(dropDownIsicSubGroupReq);

            Assert.IsType<OkObjectResult>(result);

        }

    }
}
