using Core.ViewModel.WhiteListChequeConfig;
using Moq.EntityFrameworkCore;
using static Test.Helper.MoqHelper;

namespace Test.TestCases.Repositories.WhiteListChequeConfigRepo;

public class WhiteListChequeConfigRepoTest
{
    [Fact]
    public async Task Get()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.Context.Setup(x => x.WhiteListChequeConfigs).ReturnsDbSet([new() { ChequeCollateralId = 1, CustomerId = 1, Id = 1 }]);
        moq.Context.Setup(x => x.CurrentChequeConfigs).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.Customers).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet([new() { Id = 1, BirthDate = DateTime.Now }]);

        var WhiteListChequeConfigRepo = new Infrastructure.Repositories.WhiteListChequeConfigRepository.WhiteListChequeConfigRepo(moq.Context.Object);

        var result = await WhiteListChequeConfigRepo.Get(1);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByChequeCollateralID()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.Context.Setup(x => x.WhiteListChequeConfigs).ReturnsDbSet([new() { ChequeCollateralId = 1 }]);

        var WhiteListChequeConfigRepo = new Infrastructure.Repositories.WhiteListChequeConfigRepository.WhiteListChequeConfigRepo(moq.Context.Object);

        var result = await WhiteListChequeConfigRepo.getByChequeCollateralID(1);

        Assert.NotEqual(0, result);
    }

    [Fact]
    public async Task GetById()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.Context.Setup(x => x.WhiteListChequeConfigs).ReturnsDbSet([new() { Id = 1 }]);

        var WhiteListChequeConfigRepo = new Infrastructure.Repositories.WhiteListChequeConfigRepository.WhiteListChequeConfigRepo(moq.Context.Object);

        var result = await WhiteListChequeConfigRepo.GetById(1);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Serach()
    {
        var moq = GetUnitOfWorkMoqCollection();

        moq.Context.Setup(x => x.WhiteListChequeConfigs).ReturnsDbSet([new() { ChequeCollateralId = 1, CustomerId = 1, Id = 1 }]);
        moq.Context.Setup(x => x.CurrentChequeConfigs).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.CorporateCustomers).ReturnsDbSet([new() { Id = 1 }]);
        moq.Context.Setup(x => x.IndividualCustomers).ReturnsDbSet([new() { Id = 1 }]);

        var WhiteListChequeConfigRepo = new Infrastructure.Repositories.WhiteListChequeConfigRepository.WhiteListChequeConfigRepo(moq.Context.Object);

        var result = await WhiteListChequeConfigRepo.Serach(new WhiteListChequeConfigIM { Page = 1, Size = 1 });

        Assert.NotEmpty(result.Items);
    }
}
