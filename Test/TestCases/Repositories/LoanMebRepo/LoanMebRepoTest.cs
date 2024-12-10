using Core.Entities;
using Core.ViewModel.LoanMeb;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Repositories.LoanMebRepo;

public class LoanMebRepoTest
{
    private readonly Mock<DBContext> _contextMoq = new();

    [Fact]
    public async Task GetAllLoanMeb()
    {
        _contextMoq.Setup(x => x.LoanMebs).ReturnsDbSet(
            new List<LoanMeb>
            {
                new()
                {
                    CustomerId = 0,
                    LoanTypeId = 0,
                    CurrencyId = 0,
                    LoanStatusId = 0
                }
            });
        _contextMoq.Setup(x => x.ProposalManagerLoans).ReturnsDbSet([new() { Id = 1 }]);
        _contextMoq.Setup(x => x.ProposalSchemes).ReturnsDbSet(new List<ProposalScheme> { new() { Id = 0, CustomerId = 0 } });
        _contextMoq.Setup(x => x.LoanTypes).ReturnsDbSet(new List<LoanType> { new() { Id = 0 } });
        _contextMoq.Setup(x => x.Currencies).ReturnsDbSet(new List<Currency> { new() { Id = 0 } });
        _contextMoq.Setup(x => x.LoanStatuses).ReturnsDbSet(new List<LoanStatus> { new() { Id = 0 } });

        var getAllLoanMebRepo = new Infrastructure.Repositories.LoanMebRepository.LoanMebRepo(_contextMoq.Object);

        var result = await getAllLoanMebRepo.GetAllLoanMeb(new GetAllLoanMebIM { ProposalSchemeId = 0, Page = 1, Size = 1 });

        Assert.NotNull(result);
    }
}
