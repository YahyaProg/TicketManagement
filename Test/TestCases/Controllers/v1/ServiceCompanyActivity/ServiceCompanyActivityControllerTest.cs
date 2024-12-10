using Api.Controllers.v1;
using Application.Services.ServiceCompanyActivity;
using Core.GenericResultModel;
using Core.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.ServiceCompanyActivity
{
    public class ServiceCompanyActivityControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<ServiceCompanyActivityVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<ServiceCompanyActivityVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };

        [Fact]
        public async Task AddServiceCompanyActivityTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddServiceCompanyActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var serviceCompanyActivityController = new ServiceCompanyActivityController(mediator.Object);
            var addServiceCompanyActivityReq = new AddServiceCompanyActivityRequest();

            var result = await serviceCompanyActivityController.Add(addServiceCompanyActivityReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteServiceCompanyActivityTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteServiceCompanyActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var serviceCompanyActivityController = new ServiceCompanyActivityController(mediator.Object);
            var deleteServiceCompanyActivityReq = new DeleteServiceCompanyActivityRequest();

            var result = await serviceCompanyActivityController.Delete(deleteServiceCompanyActivityReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateServiceCompanyActivityTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateServiceCompanyActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var serviceCompanyActivityController = new ServiceCompanyActivityController(mediator.Object);
            var updateServiceCompanyActivityReq = new UpdateServiceCompanyActivityRequest();

            var result = await serviceCompanyActivityController.Update(updateServiceCompanyActivityReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetServiceCompanyActivityTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetServiceCompanyActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var serviceCompanyActivityController = new ServiceCompanyActivityController(mediator.Object);
            var getServiceCompanyActivityReq = new GetServiceCompanyActivityRequest();

            var result = await serviceCompanyActivityController.Get(getServiceCompanyActivityReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchServiceCompanyActivityTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchServiceCompanyActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var serviceCompanyActivityController = new ServiceCompanyActivityController(mediator.Object);
            var searchServiceCompanyActivityReq = new SearchServiceCompanyActivityRequest();

            var result = await serviceCompanyActivityController.Search(searchServiceCompanyActivityReq);


            Assert.IsType<OkObjectResult>(result);
        }

        //[Fact]
        //public async Task AdvanceSearchServiceCompanyActivityTest()
        //{
        //    mediator.Setup(x => x.Send(It.IsAny<AdvancedSearchServiceCompanyActivityRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        //    var CommentController = new ServiceCompanyActivityController(mediator.Object);
        //    var searchServiceCompanyActivityReq = new AdvancedSearchServiceCompanyActivityRequest();

        //    var result = await CommentController.AdvancedSearch(searchServiceCompanyActivityReq);


        //    Assert.IsType<OkObjectResult>(result);
        //}
    }
}
