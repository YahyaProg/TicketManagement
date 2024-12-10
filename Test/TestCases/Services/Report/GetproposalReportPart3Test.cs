using Application.Services.MebAccountService;
using Application.Services.Report;
using Application.Services.RiskInfoService;
using Application.Services.RiskRankHistService;
using Core.ViewModel.MebAccount;
using Core.ViewModel.RiskInfoPage;
using Core.ViewModel.RiskRankHistModel;
using MediatR;
using Moq;
using Moq.EntityFrameworkCore;
using TanvirArjel.EFCore.GenericRepository;
using Test.Helper;

namespace Test.TestCases.Services.Report
{
    public class GetproposalReportPart3Test
    {
        public readonly Mock<IMediator> _mediator = new();

        [Fact]
        public async Task GetproposalReportPart3_Test_Success()
        {
            // Arrange
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            collection.Context.Setup(x => x.ProposalSchemes).ReturnsDbSet([
                new Core.Entities.ProposalScheme(){Id = 1, CustomerId = 1, ProposalId = 1}
                ]);

            collection.Context.Setup(x => x.CustomerSchemes).ReturnsDbSet([
              new Core.Entities.CustomerScheme(){Id = 1, ProposalId = 1, CustomerId = 1, ProposalSchemeId = 1}
              ]);

            collection.Context.Setup(x => x.Bgmebs).ReturnsDbSet([
              new Core.Entities.Bgmeb(){Id = 1, ProposalId = 1, CustomerId = 1, ProposalSchemeId = 1, RemainingAmount = 234, OutStanding = 123}
              ]);

            collection.Context.Setup(x => x.ProposalManagerAccounts).ReturnsDbSet([
             new(){Id = 1}
             ]);

            collection.Context.Setup(x => x.ProposalManagerBgs).ReturnsDbSet([
             new(){Id = 1}
             ]);

            collection.Context.Setup(x => x.ProposalManagerLcs).ReturnsDbSet([
             new(){Id = 1}
             ]);
            collection.Context.Setup(x => x.ProposalManagerLoans).ReturnsDbSet([
             new(){Id = 1}
             ]);

            collection.Context.Setup(x => x.Lcmebs).ReturnsDbSet([
              new Core.Entities.Lcmeb(){Id = 1, ProposalId = 1, CustomerId = 1, ProposalSchemeId = 1, RemainingAmount = 23}
              ]);

            collection.Context.Setup(x => x.LoanMebs).ReturnsDbSet([
              new Core.Entities.LoanMeb(){Id = 1, ProposalId = 1, CustomerId = 1, ProposalSchemeId = 1, LocalLedgerBalance = 123}
              ]);



            _mediator.Setup(x => x.Send(It.IsAny<GetMebAccountRequest>(), CancellationToken.None)).ReturnsAsync(new Core.GenericResultModel.ApiResult<PaginatedList<MebAccountTurnoverVM>> ()
            {
                Data = new([], 2, 3, 1)
            });

            _mediator.Setup(x => x.Send(It.IsAny<GetRiskRankInfoRequest>(), CancellationToken.None)).ReturnsAsync(new Core.GenericResultModel.ApiResult<RiskRankInfoVM>()
            {
                Data = new()
            });


            IEnumerable<RiskInfoSubItemVm> subItems = [
                new RiskInfoSubItemVm(){ Id = 1, Title = "salam"}
                ];

            IEnumerable<RiskInfoItemVm> items = [
                new RiskInfoItemVm(){ Id =1, Title = "salam", SubItems = subItems  }
                ];
            IEnumerable<RiskInfoGroupVm> response = [
                 new RiskInfoGroupVm(){Id =1 , Items = items}
            ];

            _mediator.Setup(x => x.Send(It.IsAny<GetRiskInfoAnswerRequest>(), CancellationToken.None)).ReturnsAsync(new Core.GenericResultModel.ApiResult<IEnumerable<RiskInfoGroupVm>>()
            {
               Message = "success",
               Data = response
            });


            var handler = new GetProposalInfoReportPart3RequestHandler(collection.Context.Object, _mediator.Object);

            // Act
            var result = await handler.Handle(new GetProposalInfoReportPart3Request() { ProposalSchemeId = 1 }, CancellationToken.None);


            // Assert
            Assert.True(result.IsSuccess);

        }

    }
}
