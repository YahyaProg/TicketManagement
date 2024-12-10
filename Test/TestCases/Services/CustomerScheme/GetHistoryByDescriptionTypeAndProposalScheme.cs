using Application.Services.CustomerSchemeService;
using Infrastructure;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.CustomerSchemeTest
{
    public class GetHistoryByDescriptionTypeAndProposalScheme
    {
        private readonly Mock<DBContext> context = new();

        [Fact]
        public async void GetHistoryByDescriptionTypeAndProposalSchemeRequest_Add_Success()
        {
            var request = new GetHistoryByDescriptionTypeAndProposalSchemeRequest()
            {
                DescriptionType = Core.Enums.ERegisterHistory.MajorChangesAndHistory,
                ProposalSchemeId = 15,
            };
            List<Core.Entities.CustomerScheme> customerSchemes =
                [
                     new()
                    {
                        Id = 1,
                        ActivityDescriptions = "2درخواست وام",
                        CompanySubject = "تولیدی",
                        Moaref = " معرف 2",
                        ProposalId = 2  ,
                        ProposalSchemeId=15
                    }
                ];
            var mock = customerSchemes.BuildMock();
            _ = context.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);
            context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(3);

            var handler = new GetHistoryByDescriptionTypeAndProposalSchemeRequestHandler(context.Object);
            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetHistoryByDescriptionTypeAndProposalSchemeRequest_Add_SuccessCompanySubject()
        {
            var request = new GetHistoryByDescriptionTypeAndProposalSchemeRequest()
            {
                DescriptionType = Core.Enums.ERegisterHistory.CompanySubject,
                ProposalSchemeId = 15,
            };
            List<Core.Entities.CustomerScheme> customerSchemes =
                [
                     new()
                    {
                        Id = 1,
                        ActivityDescriptions = "2درخواست وام",
                        CompanySubject = "تولیدی",
                        Moaref = " معرف 2",
                        ProposalId = 2  ,
                        ProposalSchemeId=15
                    }
                ];
            var mock = customerSchemes.BuildMock();
            _ = context.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);
            context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(3);
            var handler = new GetHistoryByDescriptionTypeAndProposalSchemeRequestHandler(context.Object);
            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);
        }
            
        [Fact]
        public async void GetHistoryByDescriptionTypeAndProposalSchemeRequest_Add_SuccessActivityDescriptions()
        {
            var request = new GetHistoryByDescriptionTypeAndProposalSchemeRequest()
            {
                DescriptionType = Core.Enums.ERegisterHistory.CompanySubject,
                ProposalSchemeId = 15,
            };
            List<Core.Entities.CustomerScheme> customerSchemes =
                [
                     new()
                    {
                        Id = 1,
                        ActivityDescriptions = "2درخواست وام",
                        CompanySubject = "تولیدی",
                        Moaref = " معرف 2",
                        ProposalId = 2  ,
                        ProposalSchemeId=15
                    }
                ];
            var mock = customerSchemes.BuildMock();
            _ = context.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);
            context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(3);
            var handler = new GetHistoryByDescriptionTypeAndProposalSchemeRequestHandler(context.Object);
            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);
        }

         [Fact]
        public async void GetHistoryByDescriptionTypeAndProposalSchemeRequest_Add_SuccessStructuralDiagram()
        {
            var request = new GetHistoryByDescriptionTypeAndProposalSchemeRequest()
            {
                DescriptionType = Core.Enums.ERegisterHistory.CompanySubject,
                ProposalSchemeId = 15,
            };
            List<Core.Entities.CustomerScheme> customerSchemes =
                [
                     new()
                    {
                        Id = 1,
                        ActivityDescriptions = "2درخواست وام",
                        CompanySubject = "تولیدی",
                        Moaref = " معرف 2",
                        ProposalId = 2  ,
                        ProposalSchemeId=15
                    }
                ];
            var mock = customerSchemes.BuildMock();
            _ = context.Setup(x => x.CustomerSchemes).ReturnsDbSet(customerSchemes);
            context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(3);
            var handler = new GetHistoryByDescriptionTypeAndProposalSchemeRequestHandler(context.Object);
            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);
        }


    }
}
