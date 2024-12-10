using Api.Controllers.v1;
using Application.Services.EducationTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.EducationType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.EducationType
{
    public class EducationType
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<EducationTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<EducationTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<int?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddEducationTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddEducationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var EducationTypeController = new EducationTypeController(mediator.Object);
            var addCurrncyReq = new AddEducationTypeRequest();

            var result = await EducationTypeController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteEducationTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteEducationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var EducationTypeController = new EducationTypeController(mediator.Object);
            var deleteCurrncyReq = new DeleteEducationTypeRequest();

            var result = await EducationTypeController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownEducationTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownEducationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var EducationTypeController = new EducationTypeController(mediator.Object);

            var dropDownEducationTypeReq = new DropDownEducationTypeRequest();

            var result = await EducationTypeController.DropDown(dropDownEducationTypeReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateEducationTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateEducationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var EducationTypeController = new EducationTypeController(mediator.Object);
            var updateCurrncyReq = new UpdateEducationTypeRequest();

            var result = await EducationTypeController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetEducationTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetEducationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var EducationTypeController = new EducationTypeController(mediator.Object);
            var getCurrncyReq = new GetEducationTypeRequest();

            var result = await EducationTypeController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchEducationTypeTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchEducationTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var EducationTypeController = new EducationTypeController(mediator.Object);
            var searchCurrncyReq = new SearchEducationTypeRequest();

            var result = await EducationTypeController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
