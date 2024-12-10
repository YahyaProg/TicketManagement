using Core.Entities;
using Core.ViewModel.ManagerDebt;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Repositories.ManagerDebtRepo;

public class ManagerDebtRepoTest
{
    private readonly Mock<DBContext> _contextMoq = new();

    [Fact]
    public async Task GetAllIndividualManagerDebt()
    {
        _contextMoq.Setup(x => x.ManagerDebts).ReturnsDbSet(
            new List<ManagerDebt>
            {
                new()
                {
                    ManagerId = 0,
                    CurrencyId = 0,
                    ProposalId = 0,
                    CustomerId = 0,
                    LastEtelam = true
                }
            });
        _contextMoq.Setup(x => x.Managers).ReturnsDbSet(new List<Manager> { new() { Id = 0, PersonId = 0, Deleted = false } });
        _contextMoq.Setup(x => x.IndividualCustomers).ReturnsDbSet(new List<IndividualCustomer> { new() { Id = 0 } });
        _contextMoq.Setup(x => x.Currencies).ReturnsDbSet(new List<Currency> { new() { Id = 0 } });

        var managerDebtRepo = new Infrastructure.Repositories.ManagerDebtRepository.ManagerDebtRepo(_contextMoq.Object);

        var result = await managerDebtRepo.GetAllIndividualManagerDebt(
            new GetAllIndividualManagerDebtIM
            {
                ProposalId = 0,
                CustomerId = 0,
                Page = 1,
                Size = 1
            });

        Assert.NotEmpty(result.Items);
    }

    [Fact]
    public async Task GetAllIndividualManagerDebtTotal()
    {
        _contextMoq.Setup(x => x.ManagerDebtsTotal).ReturnsDbSet(
            new List<ManagerDebtTotal> { new() { CustomerId = 0, LastEstelam = true } });

        var managerDebtRepo = new Infrastructure.Repositories.ManagerDebtRepository.ManagerDebtRepo(_contextMoq.Object);

        var result = await managerDebtRepo.GetAllCorporateManagerDebtTotal(
            new GetAllCorporateManagerDebtTotalIM
            {
                CustomersId = new List<long> { 0 },
                Page = 1,
                Size = 1
            });

        Assert.NotEmpty(result.Items);
    }
}
