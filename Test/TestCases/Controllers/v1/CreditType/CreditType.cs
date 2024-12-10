using Api.Controllers.v1;
using Application.Services.CreditTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.CreditType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CreditType
{
    public class CreditType
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<CreditTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> dropDownSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<CreditTypeVM>> searchSuccessRes = new () { IsSuccess = true, Code = 0 };
        [Fact]
        public async Task AddCreditTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddCreditTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var creditTypeController = new CreditTypeController(mediator.Object);
            var addCreditTypeReq = new AddCreditTypeRequest();

            var result = await creditTypeController.Add(addCreditTypeReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCreditTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteCreditTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var creditTypeController = new CreditTypeController(mediator.Object);
            var deleteCreditTypeReq = new DeleteCreditTypeRequest();

            var result = await creditTypeController.Delete(deleteCreditTypeReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCreditTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCreditTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var creditTypeController = new CreditTypeController(mediator.Object);
            var updateCreditTypeReq = new UpdateCreditTypeRequest();

            var result = await creditTypeController.Update(updateCreditTypeReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCreditTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCreditTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var creditTypeController = new CreditTypeController(mediator.Object);
            var getCreditTypeReq = new GetCreditTypeRequest();

            var result = await creditTypeController.Get(getCreditTypeReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchCreditTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchCreditTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var creditTypeController = new CreditTypeController(mediator.Object);
            var searchCreditTypeReq = new SearchCreditTypeRequest();

            var result = await creditTypeController.Search(searchCreditTypeReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DropDownCreditTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownSearchCreditTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(dropDownSuccessRes);

            var creditTypeController = new CreditTypeController(mediator.Object);
            var searchCreditTypeReq = new DropDownSearchCreditTypeRequest();

            var result = await creditTypeController.DropDownSearch(searchCreditTypeReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
