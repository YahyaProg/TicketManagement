using Api.Controllers.v1;
using Application.Services.CompQualQuestionService;
using Core.GenericResultModel;
using Core.ViewModel;
using Core.ViewModel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CompQualQuestion
{
    public class CompQualQuestionControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult<long> successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult successDeleteRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<CompQualQuestionVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<CompQualQuestionVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddOrUpdateCompQualQuestionTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddOrUpdateCompQualQuestionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CompQualQuestionController = new CompQualQuestionController(mediator.Object);
            var addCurrncyReq = new AddOrUpdateCompQualQuestionRequest();

            var result = await CompQualQuestionController.AddOrUpdate(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCompQualQuestionTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteCompQualQuestionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successDeleteRes);

            var CompQualQuestionController = new CompQualQuestionController(mediator.Object);
            var deleteCurrncyReq = new DeleteCompQualQuestionRequest();

            var result = await CompQualQuestionController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCompQualQuestionTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCompQualQuestionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var CompQualQuestionController = new CompQualQuestionController(mediator.Object);
            var getCurrncyReq = new GetCompQualQuestionRequest();

            var result = await CompQualQuestionController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchCompQualQuestionTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchCompQualQuestionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var CompQualQuestionController = new CompQualQuestionController(mediator.Object);
            var searchCurrncyReq = new SearchCompQualQuestionRequest();

            var result = await CompQualQuestionController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
