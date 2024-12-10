using Application.Services.Auth.RoleService;
using Core.GenericResultModel;
using Core.ViewModel.Auth.Role;
using Core.ViewModel.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;
using Api.Controllers.auth;

namespace Test.TestCases.Controllers.Auth.Role
{
    public class RoleControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult successRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<RoleVM> getSuccessRes = new() { IsSuccess = true, Code = 0 };
        readonly ApiResult<PaginatedList<RoleVM>> searchSuccessRes = new() { IsSuccess = true, Code = 0 };
        ApiResult<PaginatedList<DropDownResponseVM<long?>>> DropDownSuccessRes = new() { IsSuccess = true, Code = 0 };
        [Fact]
        public async Task AddRoleTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<AddRoleRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var RoleController = new RoleController(mediator.Object);
            var addCurrncyReq = new AddRoleRequest();

            var result = await RoleController.Add(addCurrncyReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteRoleTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DeleteRoleRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var RoleController = new RoleController(mediator.Object);
            var deleteCurrncyReq = new DeleteRoleRequest();

            var result = await RoleController.Delete(deleteCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DropDownRoleTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<DropDownRoleRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(DropDownSuccessRes);

            var RoleController = new RoleController(mediator.Object);

            var dropDownRoleReq = new DropDownRoleRequest();

            var result = await RoleController.DropDown(dropDownRoleReq);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateRoleTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<UpdateRoleRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var RoleController = new RoleController(mediator.Object);
            var updateCurrncyReq = new UpdateRoleRequest();

            var result = await RoleController.Update(updateCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetRoleTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<GetRoleRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getSuccessRes);

            var RoleController = new RoleController(mediator.Object);
            var getCurrncyReq = new GetRoleRequest();

            var result = await RoleController.Get(getCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SearchRoleTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<SearchRoleRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(searchSuccessRes);

            var RoleController = new RoleController(mediator.Object);
            var searchCurrncyReq = new SearchRoleRequest();

            var result = await RoleController.Search(searchCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
