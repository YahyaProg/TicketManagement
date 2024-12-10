using Application.Services.LoanMebService;
using Core.ViewModel.LoanMeb;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.LoanMebTest;

public class LoanMebRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task GetAllLoanMebRequest_Success()
    {
        var command = new GetAllLoanMebRequest { ProposalSchemeId = 0 };

        _unitOfWork.Setup(x => x.LoanMebRepo.GetAllLoanMeb(It.IsAny<GetAllLoanMebIM>()));

        var handler = new GetAllLoanMebRequestHandler(_unitOfWork.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateLoanMebRequest_Fail1()
    {
        var command = new UpdateLoanMebRequest { Id = 0 };

        _unitOfWork.Setup(x => x.Repository.GetByIdAsync<Core.Entities.LoanMeb>(It.IsAny<object>(), CancellationToken.None));

        var handler = new UpdateLoanMebRequestHandler(_unitOfWork.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateLoanMebRequest_Fail2()
    {
        var command = new UpdateLoanMebRequest { Id = 0 };

        _unitOfWork.Setup(x => x.Repository.GetByIdAsync<Core.Entities.LoanMeb>(It.IsAny<object>(), CancellationToken.None)).ReturnsAsync
            (new Core.Entities.LoanMeb());

        _unitOfWork.Setup(x => x.Repository.Update(It.IsAny<Core.Entities.LoanMeb>()));

        _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(0);

        var handler = new UpdateLoanMebRequestHandler(_unitOfWork.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateLoanMebRequest_Success()
    {
        var command = new UpdateLoanMebRequest { Id = 0 };

        _unitOfWork.Setup(x => x.Repository.GetByIdAsync<Core.Entities.LoanMeb>(It.IsAny<object>(), CancellationToken.None)).ReturnsAsync
            (new Core.Entities.LoanMeb
            {
                CurrencyId = 0
            });

        _unitOfWork.Setup(x => x.Repository.Update(It.IsAny<Core.Entities.LoanMeb>()));

        _unitOfWork.Setup(x => x.Repository.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

        var handler = new UpdateLoanMebRequestHandler(_unitOfWork.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void DeleteLoanMebRequest_Success() =>
        Assert.NotNull(new DeleteLoanMebRequestHandler(_unitOfWork.Object));
}
