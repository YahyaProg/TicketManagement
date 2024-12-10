using Api.Controllers.v1;
using Application.Services.LicenceService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.Licence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.Licence
{
    public class LicenceControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<LicenceVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<LicenceVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddLicenceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddLicenceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var LicenceController = new LicenceController(mediator.Object);
            var addCurrncyReq = new AddLicenceRequest();

            var result = await LicenceController.Add(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteLicenceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteLicenceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var LicenceController = new LicenceController(mediator.Object);
            var deleteCurrncyReq = new DeleteLicenceRequest();

            var result = await LicenceController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownLicenceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownLicenceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var LicenceController = new LicenceController(mediator.Object);

            var dropDownLicenceReq = new DropDownLicenceRequest();

            var result = await LicenceController.DropDown(dropDownLicenceReq);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdateLicenceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateLicenceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var LicenceController = new LicenceController(mediator.Object);
            var updateCurrncyReq = new UpdateLicenceRequest();

            var result = await LicenceController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetLicenceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetLicenceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var LicenceController = new LicenceController(mediator.Object);
            var getCurrncyReq = new GetLicenceRequest();

            var result = await LicenceController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchLicenceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchLicenceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var LicenceController = new LicenceController(mediator.Object);
            var searchCurrncyReq = new SearchLicenceRequest();

            var result = await LicenceController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
