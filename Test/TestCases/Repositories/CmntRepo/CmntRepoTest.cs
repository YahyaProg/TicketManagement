using Core.ViewModel;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Repositories.CmntRepo;

public class CmntRepoTest
{
    [Fact]
    public async Task AdvancedSearch()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.Context.Setup(x => x.Cmnts).ReturnsDbSet([new() { UserId = "1" }]);
        moq.Context.Setup(x => x.BankStaffs).ReturnsDbSet([new() { UserId = "1" }]);

        var advancedSearch = new Infrastructure.Repositories.CmntRepository.CmntRepo(moq.Context.Object);

        var result = await advancedSearch.AdvancedSearch(new CmntIM { Page = 1, Size = 1 });

        Assert.NotEmpty(result.Items);
    }
}
