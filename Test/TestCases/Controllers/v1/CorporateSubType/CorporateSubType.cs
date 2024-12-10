using Api.Controllers.v1;
using Application.Services.CorporateSubTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.CorporateSubType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CorporateSubType
{
    public class CorporateSubType
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<CorporateSubTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<CorporateSubTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> dropDownSuccessRes = new() { IsSuccess = true, Code = 0 };


        [Fact]
        public async Task AddCorporateSubTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddCorporateSubTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var corporateSubTypeController = new CorporateSubTypeController(mediator.Object);
            var addCurrncyReq = new AddCorporateSubTypeRequest();

            var result = await corporateSubTypeController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCorporateSubTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteCorporateSubTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CorporateSubTypeController = new CorporateSubTypeController(mediator.Object);
            var deleteCurrncyReq = new DeleteCorporateSubTypeRequest();

            var result = await CorporateSubTypeController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateCorporateSubTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateCorporateSubTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CorporateSubTypeController = new CorporateSubTypeController(mediator.Object);
            var updateCurrncyReq = new UpdateCorporateSubTypeRequest();

            var result = await CorporateSubTypeController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCorporateSubTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetCorporateSubTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var CorporateSubTypeController = new CorporateSubTypeController(mediator.Object);
            var getCurrncyReq = new GetCorporateSubTypeRequest();

            var result = await CorporateSubTypeController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchCorporateSubTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchCorporateSubTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var CorporateSubTypeController = new CorporateSubTypeController(mediator.Object);
            var searchCurrncyReq = new SearchCorporateSubTypeRequest();

            var result = await CorporateSubTypeController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DropDownCorporateSubTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownSearchCorporateSubTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(dropDownSuccessRes);

            var CorporateSubTypeController = new CorporateSubTypeController(mediator.Object);
            var searchCurrncyReq = new DropDownSearchCorporateSubTypeRequest();

            var result = await CorporateSubTypeController.DropDownSearch(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
