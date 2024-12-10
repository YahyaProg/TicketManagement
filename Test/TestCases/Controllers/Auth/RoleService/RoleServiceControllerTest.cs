using Application.Services.Auth.RoleServService;
using Core.GenericResultModel;
using Core.ViewModel.Auth.AuthRoleService;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Api.Controllers.auth;

using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.Auth.RoleService
{
    public class RoleServiceControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<AuthRoleServiceVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<AuthRoleServiceVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        
       

        [Fact]
        public async Task DeleteRoleServiceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteRoleServiceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var RoleServiceController = new RoleServiceController(mediator.Object);
            var deleteCurrncyReq = new DeleteRoleServiceRequest();

            var result = await RoleServiceController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
     
        [Fact]
        public async Task GetRoleServiceTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetRoleServiceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var RoleServiceController = new RoleServiceController(mediator.Object);
            var getCurrncyReq = new GetRoleServiceRequest();

            var result = await RoleServiceController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        // TODO
        //[Fact]
        //public async Task SearchRoleServiceTest()
        //{
        //    mediator.Setup(x => x.Send(It.IsAny<SearchRoleServiceRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

        //    var RoleServiceController = new RoleServiceController(mediator.Object);
        //    var searchCurrncyReq = new SearchRoleServiceRequest();

        //    var result = await RoleServiceController.Search(searchCurrncyReq);


        //    Assert.IsType<OkObjectResult>(result);
        //}
    }
}
