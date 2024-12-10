using Application.Services.ManagerDebtService;
using Core.ViewModel.ManagerDebt;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.ManagerDebt;

public class ManagerDebtRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task GetAllIndividualManagerDebtRequest_Success()
    {
        var command = new GetAllIndividualManagerDebtRequest { ProposalId = 0 };

        _unitOfWork.Setup(x => x.ManagerDebtRepo.GetAllIndividualManagerDebt(It.IsAny<GetAllIndividualManagerDebtIM>()));

        var handler = new GetAllIndividualManagerDebtRequestHandler(_unitOfWork.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GetAllCorporateManagerDebtTotalRequest_Success()
    {
        var command = new GetAllCorporateManagerDebtTotalRequest { CustomersId = [0] };

        _unitOfWork.Setup(x => x.ManagerDebtRepo.GetAllCorporateManagerDebtTotal(It.IsAny<GetAllCorporateManagerDebtTotalIM>()));

        var handler = new GetAllCorporateManagerDebtTotalRequestHandler(_unitOfWork.Object);
        
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }
}
