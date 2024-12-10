using Api.Controllers.v1;
using Application.Services.CreditSubTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.CreditSubType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CreditSubType
{
    public class CreditSubType
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<CreditSubTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<CreditSubTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> dropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddCreditSubTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddCreditSubTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var creditSubTypeController = new CreditSubTypeController(mediator.Object);
            var addCreditSubTypeReq = new AddCreditSubTypeRequest();

            var result = await creditSubTypeController.Add(addCreditSubTypeReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCreditSubTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteCreditSubTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var creditSubTypeController = new CreditSubTypeController(mediator.Object);
            var deleteCreditSubTypeReq = new DeleteCreditSubTypeRequest();

            var result = await creditSubTypeController.Delete(deleteCreditSubTypeReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCreditSubTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCreditSubTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var creditSubTypeController = new CreditSubTypeController(mediator.Object);
            var updateCreditSubTypeReq = new UpdateCreditSubTypeRequest();

            var result = await creditSubTypeController.Update(updateCreditSubTypeReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCreditSubTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCreditSubTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var creditSubTypeController = new CreditSubTypeController(mediator.Object);
            var getCreditSubTypeReq = new GetCreditSubTypeRequest();

            var result = await creditSubTypeController.Get(getCreditSubTypeReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchCreditSubTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchCreditSubTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var creditSubTypeController = new CreditSubTypeController(mediator.Object);
            var searchCreditSubTypeReq = new SearchCreditSubTypeRequest();

            var result = await creditSubTypeController.Search(searchCreditSubTypeReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DropDownCreditSubTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownSearchCreditSubTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(dropDownSuccessRes);

            var creditSubTypeController = new CreditSubTypeController(mediator.Object);
            var searchCreditSubTypeReq = new DropDownSearchCreditSubTypeRequest();

            var result = await creditSubTypeController.DropDownSearch(searchCreditSubTypeReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
