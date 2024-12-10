using Application.Services.ProposalService;
using Core.Entities;
using Core.Enums;
using Core.Helpers;
using Moq;
using Moq.EntityFrameworkCore;
using Test.Helper;

namespace Test.TestCases.Services.ProposalTest
{
    public class ChangeProposalStatusRequestTest
    {
        private readonly Mock<IUserHelper> helper;
        public ChangeProposalStatusRequestTest()
        {
            helper = new();
            helper.Setup(x => x.GetUserFromToken()).Returns(new Core.ViewModel.UserDto { Id = "1" });
        }

        [Fact]
        public async Task ChangeProposalStatusRequest_Test_Success()
        {
            var collection = MoqHelper.GetUnitOfWorkMoqCollection();

            var request = new ChangeProposalStatusRequest() { 
                ProposalId = 1,Status=EProposal_status.sent
            };

            collection.Context.Setup(x => x.Proposals).ReturnsDbSet([
                new Proposal(){Id = 1, CustomerId = 1,Status=EProposal_status.todo}
                ]);


            collection.Context.Setup(x => x.RiskRankHists).ReturnsDbSet([
                new RiskRankHist(){Id = 1, FinalScore = 100,ProposalId=1}
            ]);

            collection.Context.Setup(x => x.ProposalActionLogs).ReturnsDbSet([
                new ProposalActionLog(){Id = 1, ProposalId=1}
            ]);

            collection.Context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var handler = new ChangeProposalStatusRequestHandler(collection.Context.Object, helper.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);


            // Assert
            Assert.True(result.IsSuccess);


        }

    }
}
