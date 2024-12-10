using Api.Controllers.v1;
using Application.Services.CustomerSchemeService;
using Application.Services.IsCompletedService;
using Core.GenericResultModel;
using Core.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1
{
    public class IsCompletedControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<IsCompletedVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<IsCompletedVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddIsCompletedTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddIsCompletedRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var isCompletedController = new IsCompletedController(mediator.Object);
            var addisCompletedReq = new AddIsCompletedRequest();

            var result = await isCompletedController.Add(addisCompletedReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteIsCompletedTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteIsCompletedRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var isCompletedController = new IsCompletedController(mediator.Object);
            var deleteIsCompletedReq = new DeleteIsCompletedRequest();

            var result = await isCompletedController.Delete(deleteIsCompletedReq);


            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task GetIsCompletedTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetIsCompletedRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var isCompletedController = new IsCompletedController(mediator.Object);
            var getIsCompletedReq = new GetIsCompletedRequest();

            var result = await isCompletedController.Get(getIsCompletedReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task IsCompletedCompleteTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddIsCompletedRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);
            var isCompletedController = new IsCompletedController(mediator.Object);

            var result = await isCompletedController.Complete(It.IsAny<long>());

            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task SearchIsCompletedTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchIsCompletedRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var isCompletedController = new IsCompletedController(mediator.Object);
            var searchIsCompletedReq = new SearchIsCompletedRequest();

            var result = await isCompletedController.Search(searchIsCompletedReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateCustomerSchemeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateIsCompletedRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var customerSchemeController = new IsCompletedController(mediator.Object);
            var searchCustomerSchemeReq = new UpdateIsCompletedRequest();

            var result = await customerSchemeController.Update(searchCustomerSchemeReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
