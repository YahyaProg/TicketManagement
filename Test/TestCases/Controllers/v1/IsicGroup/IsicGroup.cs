using Api.Controllers.v1;
using Application.Services.CurrencyService;
using Application.Services.IsicGroupService;
using Application.Services.IsicSubGroupService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.IsicGroup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.IsicGroup
{
    public class IsicGroup
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<IsicGroupVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<IsicGroupVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> dropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddIsicGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddIsicGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var isicGroupController = new IsicGroupController(mediator.Object);
            var addisicGroupReq = new AddIsicGroupRequest();

            var result = await isicGroupController.Add(addisicGroupReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DeleteIsicGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteIsicGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var isicGroupController = new IsicGroupController(mediator.Object);
            var deleteisicGroupReq = new DeleteIsicGroupRequest();

            var result = await isicGroupController.Delete(deleteisicGroupReq);

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task UpdateIsicGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny <UpdateIsicGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var isicGroupController = new IsicGroupController(mediator.Object);
            var updateisicGroupReq = new UpdateIsicGroupRequest();


            var result = await isicGroupController.Update(updateisicGroupReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetIsicGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetIsicGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var isicGroupController = new IsicGroupController(mediator.Object);
            var getIsicGroupReq = new GetIsicGroupRequest();

            var result = await isicGroupController.Get(getIsicGroupReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchIsicGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchIsicGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var isicGroupController = new IsicGroupController(mediator.Object);

            var searchIsicGroupReq = new SearchIsicGroupRequest();

            var result = await isicGroupController.Search(searchIsicGroupReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DropDownIsicGroupTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownSearchIsicGroupRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(dropDownSuccessRes);
            var isicGroupController = new IsicGroupController(mediator.Object);
            var dropDownIsicGroupReq = new DropDownSearchIsicGroupRequest();

            var result = await isicGroupController.DropDownSearch(dropDownIsicGroupReq);

            Assert.IsType<OkObjectResult>(result);

        }

    }
}
