using Application.Services.ProposalTypeService;
using Infrastructure;
using Moq;

namespace Test.TestCases.Services.ProposalTypeSevice
{
    public class ProposalTypeRequestTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        [Fact]
        public void AddProposalTypeRequest_Success() =>
            Assert.NotNull(new AddProposalTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SoftDeleteProposalTypeRequest_Success() =>
            Assert.NotNull(new DeleteProposalTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void GetProposalTypeRequest_Success() =>
            Assert.NotNull(new GetProposalTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void UpdateProposalTypeRequest_Success() =>
            Assert.NotNull(new UpdateProposalTypeRequestHandler(_unitOfWork.Object));

        [Fact]
        public void SearchProposalTypeRequest_Success() =>
        Assert.NotNull(new SearchProposalTypeRequestHandler(_unitOfWork.Object));
    }
}
