
using Application.Services.ProposalSchemeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.ProposalSchemeTest;


public class ProposalSchemeRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddProposalSchemeRequest_Success() =>
        Assert.NotNull(new AddProposalSchemeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SoftDeleteProposalSchemeRequest_Success() =>
        Assert.NotNull(new DeleteProposalSchemeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetProposalSchemeRequest_Success() =>
        Assert.NotNull(new GetProposalSchemeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateProposalSchemeRequest_Success() =>
        Assert.NotNull(new UpdateProposalSchemeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchProposalSchemeRequest_Success() =>
        Assert.NotNull(new SearchProposalSchemeRequestHandler(_unitOfWork.Object));
}