
using Application.Services.ProposalDescriptionService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.ProposalDescriptionTest;


public class ProposalDescriptionRequestTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddProposalDescriptionRequest_Success() =>
        Assert.NotNull(new AddProposalDescriptionRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DeleteProposalDescriptionRequest_Success() =>
        Assert.NotNull(new DeleteProposalDescriptionRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetByIdProposalDescriptionRequest_Success() =>
        Assert.NotNull(new GetProposalDescriptionRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateProposalDescriptionRequest_Success() =>
        Assert.NotNull(new UpdateProposalDescriptionRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchProposalDescriptionRequest_Success() =>
        Assert.NotNull(new SearchProposalDescriptionRequestHandler(_unitOfWork.Object));



}