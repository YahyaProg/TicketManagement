using Application.Services.InsuranceListTypeService;
using Core.GenericResultModel;
using Core.ViewModel.Base;
using Core.ViewModel.InsuranceListType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;
using Api.Controllers.v1;

namespace Test.TestCases.Controllers.v1.InsuranceListType
{
    public class InsuranceListType
    {
        public class InsuranceListTypeControllerTest
        {
            readonly Mock<IMediator> mediator = new();
            readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
            readonly ApiResult<InsuranceListTypeVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
            readonly ApiResult<PaginatedList<InsuranceListTypeVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
            readonly ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };

            [Fact]
            public async Task AddInsuranceListTypeTest()
            {
                mediator.Setup(x => x.Send(It.IsAny<AddInsuranceListTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

                var InsuranceListTypeController = new InsuranceListTypeController(mediator.Object);

                var addInsuranceListTypeReq = new AddInsuranceListTypeRequest();

                var result = await InsuranceListTypeController.Add(addInsuranceListTypeReq);

                Assert.IsType<OkObjectResult>(result);
            }

            [Fact]
            public async Task GetInsuranceListTypeTest()
            {
                mediator.Setup(x => x.Send(It.IsAny<GetInsuranceListTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

                var InsuranceListTypeController = new InsuranceListTypeController(mediator.Object);

                var getInsuranceListTypeReq = new GetInsuranceListTypeRequest();

                var result = await InsuranceListTypeController.Get(getInsuranceListTypeReq);

                Assert.IsType<OkObjectResult>(result);
            }

            [Fact]
            public async Task SearchInsuranceListTypeTest()
            {
                mediator.Setup(x => x.Send(It.IsAny<SearchInsuranceListTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

                var InsuranceListTypeController = new InsuranceListTypeController(mediator.Object);

                var searchInsuranceListTypeReq = new SearchInsuranceListTypeRequest();

                var result = await InsuranceListTypeController.Search(searchInsuranceListTypeReq);

                Assert.IsType<OkObjectResult>(result);
            }

            [Fact]
            public async Task DropDownInsuranceListTypeTest()
            {
                mediator.Setup(x => x.Send(It.IsAny<DropDownInsuranceListTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

                var InsuranceListTypeController = new InsuranceListTypeController(mediator.Object);

                var dropDownInsuranceListTypeReq = new DropDownInsuranceListTypeRequest();

                var result = await InsuranceListTypeController.DropDown(dropDownInsuranceListTypeReq);

                Assert.IsType<OkObjectResult>(result);
            }

            [Fact]
            public async Task UpdateInsuranceListTypeTest()
            {
                mediator.Setup(x => x.Send(It.IsAny<UpdateInsuranceListTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

                var InsuranceListTypeController = new InsuranceListTypeController(mediator.Object);

                var updateInsuranceListTypeReq = new UpdateInsuranceListTypeRequest();

                var result = await InsuranceListTypeController.Update(updateInsuranceListTypeReq);

                Assert.IsType<OkObjectResult>(result);
            }

            [Fact]
            public async Task DeleteInsuranceListTypeTest()
            {
                mediator.Setup(x => x.Send(It.IsAny<DeleteInsuranceListTypeRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

                var InsuranceListTypeController = new InsuranceListTypeController(mediator.Object);

                var deleteInsuranceListTypeReq = new DeleteInsuranceListTypeRequest();

                var result = await InsuranceListTypeController.Delete(deleteInsuranceListTypeReq);

                Assert.IsType<OkObjectResult>(result);
            }
        }
    }
}