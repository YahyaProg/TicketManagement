using Api.Controllers.v1;
using Application.Services.CreditManagementService;
using Core.GenericResultModel;
using Core.ViewModel.CreditManagement;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TanvirArjel.EFCore.GenericRepository;

namespace Test.TestCases.Controllers.v1.CreditManagement
{
    public class CreditManagementControllerTest
    {
        readonly Mock<IMediator> mediator = new();
        readonly ApiResult<PaginatedList<ActiveProposalSchemeAdvanceSearchVM>> successRes = new() { IsSuccess = true, Code = 0 };
        [Fact]
        public async Task ActiveProposalSchemeAdvanceSearchTest()
        {
            mediator.Setup(x => x.Send(It.IsAny<ActiveProposalSchemeAdvanceSearchRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(successRes);

            var CreditManagementController = new CreditManagementController(mediator.Object);
            var addCurrncyReq = new ActiveProposalSchemeAdvanceSearchRequest();

            var result = await CreditManagementController.ActiveProposalSchemeAdvanceSearch(addCurrncyReq);


            Assert.IsType<OkObjectResult>(result);
        }
    }
}
