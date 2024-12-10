using Core.ViewModel.BlackListChequeConfig;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Repositories.BlackListChequeConfigRepo;

public class BlackListChequeConfigRepoTest
{
    [Fact]
    public async Task Get()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.Context.Setup(x => x.BlackListChequeConfigs).ReturnsDbSet([new() { ChequeCollateralId = 1, CustomerId = 1, Id = 1 }]);
        moq.Context.Setup(x => x.CurrentChequeConfigs).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet([new() { Id = 1, BirthDate = DateTime.Now }]);

        var blackListChequeConfigRepo = new Infrastructure.Repositories.BlackListChequeConfigRepositor.BlackListChequeConfigRepo(moq.Context.Object);

        var result = await blackListChequeConfigRepo.Get(1);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByChequeCollateralID()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.Context.Setup(x => x.BlackListChequeConfigs).ReturnsDbSet([new() { ChequeCollateralId = 1 }]);

        var blackListChequeConfigRepo = new Infrastructure.Repositories.BlackListChequeConfigRepositor.BlackListChequeConfigRepo(moq.Context.Object);

        var result = await blackListChequeConfigRepo.getByChequeCollateralID(1);

        Assert.NotEqual(0, result);
    }

    [Fact]
    public async Task GetById()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.Context.Setup(x => x.BlackListChequeConfigs).ReturnsDbSet([new() { Id = 1 }]);

        var blackListChequeConfigRepo = new Infrastructure.Repositories.BlackListChequeConfigRepositor.BlackListChequeConfigRepo(moq.Context.Object);

        var result = await blackListChequeConfigRepo.GetById(1);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Serach()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.Context.Setup(x => x.BlackListChequeConfigs).ReturnsDbSet([new() { ChequeCollateralId = 1, CustomerId = 1, Id = 1 }]);
        moq.Context.Setup(x => x.CurrentChequeConfigs).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet([new() { Id = 1 }]);

        var blackListChequeConfigRepo = new Infrastructure.Repositories.BlackListChequeConfigRepositor.BlackListChequeConfigRepo(moq.Context.Object);

        var result = await blackListChequeConfigRepo.Serach(new BlackListChequeConfigIM { Page = 1, Size = 1 });

        Assert.NotEmpty(result.Items);
    }
}
