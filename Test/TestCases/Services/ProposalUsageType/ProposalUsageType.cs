using Application.Services.ProposalUsageTypeService;
using Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestCases.Services.ProposalUsageType;
public class ProposalUsageTypeServiceTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public void AddProposalUsageTypeRequest_Success() =>
        Assert.NotNull(new AddProposalUsageTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SoftDeleteProposalUsageTypeRequest_Success() =>
        Assert.NotNull(new DeleteProposalUsageTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void GetProposalUsageTypeRequest_Success() =>
        Assert.NotNull(new GetProposalUsageTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void UpdateProposalUsageTypeRequest_Success() =>
        Assert.NotNull(new UpdateProposalUsageTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void SearchProposalUsageTypeRequest_Success() =>
        Assert.NotNull(new SearchProposalUsageTypeRequestHandler(_unitOfWork.Object));

    [Fact]
    public void DropDownProposalUsageTypeRequest_Success() =>
        Assert.NotNull(new DropDownProposalUsageTypeRequestHandler(_unitOfWork.Object));
}
