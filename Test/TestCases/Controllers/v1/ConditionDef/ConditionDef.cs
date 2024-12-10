using Api.Controllers.v1;
using Application.Services.ConditionDefService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.ConditionDef;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.ConditionDef
{
    public class ConditionDef
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<ConditionDefVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<ConditionDefVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddConditionDefTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddConditionDefRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ConditionDefController = new ConditionDefController(mediator.Object);
            var addCurrncyReq = new AddConditionDefRequest();

            var result = await ConditionDefController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteConditionDefTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteConditionDefRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ConditionDefController = new ConditionDefController(mediator.Object);
            var deleteCurrncyReq = new DeleteConditionDefRequest();

            var result = await ConditionDefController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownConditionDefTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownConditionDefRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var ConditionDefController = new ConditionDefController(mediator.Object);

            var dropDownConditionDefReq = new DropDownConditionDefRequest();

            var result = await ConditionDefController.DropDown(dropDownConditionDefReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateConditionDefTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateConditionDefRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var ConditionDefController = new ConditionDefController(mediator.Object);
            var updateCurrncyReq = new UpdateConditionDefRequest();

            var result = await ConditionDefController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetConditionDefTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetConditionDefRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var ConditionDefController = new ConditionDefController(mediator.Object);
            var getCurrncyReq = new GetConditionDefRequest();

            var result = await ConditionDefController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchConditionDefTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchConditionDefRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var ConditionDefController = new ConditionDefController(mediator.Object);
            var searchCurrncyReq = new SearchConditionDefRequest();

            var result = await ConditionDefController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
