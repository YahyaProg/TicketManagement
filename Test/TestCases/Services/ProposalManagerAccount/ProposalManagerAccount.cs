using Application.Services.ProposalManagerAccountService;
using Core.Enums;
using Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;

namespace Test.TestCases.Services.ProposalManagerAccount;

public class ProposalManagerAccount
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task SearchProposalManagerAccountRequest_Success1()
    {
        _unitOfWork.Setup(x => x.Context.ProposalManagerAccounts).ReturnsDbSet([]);
        _unitOfWork.Setup(x => x.Context.MebAccounts).ReturnsDbSet([]);

        var handler = new SearchProposalManagerAccountRequestHandler(_unitOfWork.Object);

        var request = new SearchProposalManagerAccountRequest
        { ProposalId = 0, Accperiod = EMebAccount_Accperiod.YeSale, Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task SearchProposalManagerAccountRequest_Success2()
    {
        _unitOfWork.Setup(x => x.Context.ProposalManagerAccounts).ReturnsDbSet([]);
        _unitOfWork.Setup(x => x.Context.MebAccounts).ReturnsDbSet([]);

        var handler = new SearchProposalManagerAccountRequestHandler(_unitOfWork.Object);

        var request = new SearchProposalManagerAccountRequest
        { ProposalId = 0, Accperiod = EMebAccount_Accperiod.SeMahe, Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task SearchProposalManagerAccountRequest_Success3()
    {
        _unitOfWork.Setup(x => x.Context.ProposalManagerAccounts).ReturnsDbSet([]);
        _unitOfWork.Setup(x => x.Context.MebAccounts).ReturnsDbSet([]);

        var handler = new SearchProposalManagerAccountRequestHandler(_unitOfWork.Object);

        var request = new SearchProposalManagerAccountRequest
        { ProposalId = 0, Accperiod = EMebAccount_Accperiod.ShishMahe, Page = 1, Size = 1 };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void AddProposalManagerAccountRequest_Success() =>
        Assert.NotNull(new AddProposalManagerAccountRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateProposalManagerAccountRequest_Success() =>
        Assert.NotNull(new UpdateProposalManagerAccountRequestHandler(_unitOfWork.Object));
}
